using R3;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Selivura.DemoClicker.UI
{
    public class InventoryViewModel : MonoBehaviour
    {
        [Inject] private InventoryService _inventoryService;
        protected CompositeDisposable disposable = new();

        public Subject<List<Item>> OnInventoryUpdated = new();
        private void Awake()
        {
            _inventoryService.OnInventoryChanged.Subscribe(_ => OnInventoryChanged()).AddTo(disposable);
        }
        private void OnInventoryChanged()
        {
            OnInventoryUpdated.OnNext(_inventoryService.GetItems());
        }
        private void OnDestroy()
        {
            disposable.Dispose();
        }
    }
}

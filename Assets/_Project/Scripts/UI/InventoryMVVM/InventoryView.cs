using R3;
using System.Collections.Generic;
using UnityEngine;

namespace Selivura.DemoClicker.UI
{
    public abstract class InventoryView : MonoBehaviour
    {
        [SerializeField] protected InventoryViewModel viewModel;

        protected CompositeDisposable disposable = new();
        private void Awake()
        {
            viewModel.OnInventoryUpdated.Subscribe(OnInventoryUpdated);
        }
        protected virtual void OnInventoryUpdated(List<Item> items)
        {

        }
        private void OnDestroy()
        {
            disposable.Dispose();
        }
    }
}

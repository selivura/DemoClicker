using R3;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;
using Zenject;

namespace Selivura.DemoClicker.UI
{
    public class InventoryItemDisplay : MonoBehaviour
    {
        [SerializeField] private ItemViewModel _itemViewModel;
        [SerializeField] private Item _itemToSearch;

        [Inject] private InventoryService _inventoryService;

        protected CompositeDisposable disposable = new();
        private void Awake()
        {
            SetItemToSearch(_itemToSearch);
            _inventoryService.OnItemAdded.Subscribe(OnItemAdded).AddTo(disposable);
        }
        private void SetItemToSearch(Item item)
        {
            _itemToSearch = item;
            var newItem = _inventoryService.FindItemByID(item.ID);

            if (newItem != null)
            {
                _itemViewModel.SetDisplayItem(newItem);
            }
            else
            {
                _itemViewModel.UpdateItem(_itemToSearch.Icon, 0, _itemToSearch.Quality);
            }
        }
        private void OnItemAdded(Item item)
        {
            if (item.ID == _itemToSearch.ID)
            {
                _itemViewModel.SetDisplayItem(item);
            }
        }
        private void OnDestroy()
        {
            disposable.Dispose();
        }
    }
}

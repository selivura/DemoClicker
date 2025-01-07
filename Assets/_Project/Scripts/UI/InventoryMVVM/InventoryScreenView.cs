using R3;
using Selivura.DemoClicker.UI;
using System.Collections.Generic;
using UnityEngine;

namespace Selivura.DemoClicker
{
    public class InventoryScreenView : InventoryView
    {
        [SerializeField] private ItemViewModel _itemViewPrefab;
        [SerializeField] private ItemViewModel _fullDisplayViewModel;
        [SerializeField] private Transform _container;
        private List<ItemViewModel> _spawned = new();

        private CompositeDisposable _spawnedDisposable = new();
        
        protected override void OnInventoryUpdated(List<Item> items)
        {
            foreach (var spawnedVM in _spawned)
            {
                Destroy(spawnedVM.gameObject);
            }
            _spawned.Clear();
            foreach (var item in items)
            {
                var spawnedSlot = Instantiate(_itemViewPrefab, _container);
                spawnedSlot.SetDisplayItem(item);
                spawnedSlot.OnSlotSelected.Subscribe(OnItemSlotSelected).AddTo(_spawnedDisposable);
                _spawned.Add(spawnedSlot);
            }
        }
        private void OnItemSlotSelected(Item item)
        {
            _fullDisplayViewModel.SetDisplayItem(item);
        }
        private void OnDestroy()
        {
            _spawnedDisposable.Dispose();
        }
    }
}

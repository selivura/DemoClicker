using Selivura.DemoClicker.UI;
using System.Collections.Generic;
using UnityEngine;

namespace Selivura.DemoClicker
{
    public class InventoryScreenView : InventoryView
    {
        [SerializeField] private ItemViewModel _itemViewPrefab;
        [SerializeField] private Transform _container;
        private List<ItemViewModel> _spawned = new();
        
        protected override void OnInventoryUpdated(List<Item> items)
        {
            foreach (var spawnedVM in _spawned)
            {
                Destroy(spawnedVM.gameObject);
            }
            _spawned.Clear();
            foreach (var item in items)
            {
                var spawned = Instantiate(_itemViewPrefab, _container);
                spawned.SetDisplayItem(item);
                _spawned.Add(spawned);
            }
        }
    }
}

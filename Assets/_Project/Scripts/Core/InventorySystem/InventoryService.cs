using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Selivura.DemoClicker
{
    public class InventoryService : MonoBehaviour
    {
        [Inject]
        private PlayerStatsService _statsController;

        public List<Item> Items => _items;

        private readonly List<Item> _items = new();

        public void GiveItem(Item itemPrefab, int amount)
        {
            Item existingItem = _items.Find(delegate (Item compareItem) { return itemPrefab.ID == compareItem.ID; });
            if(existingItem != null)
            {
                existingItem.IncreaseStack(amount);
                return;
            }

            Item newItem = Instantiate(itemPrefab, transform);
            _items.Add(newItem);
            newItem.IncreaseStack(amount);
        }
        public void GiveItem(ItemDrop itemDrop)
        {
            GiveItem(itemDrop.Item, itemDrop.Amount);
        }
        public Item FindItemByID(string id)
        {
            return _items.Find(delegate (Item item) { return item.ID == id; });
        }
    }
}

using R3;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Selivura.DemoClicker
{
    public class InventoryService : MonoBehaviour
    {
        [Inject]
        private PlayerStatsService _statsController;

        private readonly List<Item> _items = new();

        public Subject<Item> OnItemAdded = new();
        public Subject<Unit> OnInventoryChanged = new();
        public void GiveItem(Item itemPrefab, int amount)
        {
            Item item = _items.Find(delegate (Item compareItem) { return itemPrefab.ID == compareItem.ID; });
            if(item != null)
            {
                item.IncreaseStack(amount);
            }
            else
            {
                item = Instantiate(itemPrefab, transform);
                _items.Add(item);
                item.IncreaseStack(amount);
            }
            OnItemAdded.OnNext(item);
            OnInventoryChanged.OnNext(Unit.Default);
        }
        public void GiveItem(ItemDrop itemDrop)
        {
            GiveItem(itemDrop.Item, itemDrop.Amount);
        }
        public Item FindItemByID(string id)
        {
            return _items.Find(delegate (Item item) { return item.ID == id; });
        }
        public List<Item> GetItems()
        {
            return _items;  
        }
    }
}

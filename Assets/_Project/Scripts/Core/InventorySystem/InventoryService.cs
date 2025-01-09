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
        public Subject<Item> OnItemRemoved = new();
        public Subject<Unit> OnInventoryChanged = new();

#if UNITY_EDITOR
        [SerializeField] private Item _giveItem;
        [SerializeField] private int _giveAmount;
        private void FixedUpdate()
        {
            if(_giveItem)
            {
                GiveItem(_giveItem, _giveAmount);
                _giveItem = null;
            }
        }
#endif
        public bool CanAfford(ItemPrice itemPrice, int amount = 1)
        {
            if (!HasItem(itemPrice.Item, out Item found))
                return false;
            if (found.Stack < itemPrice.Price * amount)
                return false;
            return true;
        }
        public bool GiveItem(Item itemPrefab, int amount)
        {
            if(amount < 0)
            {
                return RemoveItem(itemPrefab, -amount);
            }
            Item item = _items.Find(delegate (Item compareItem) { return itemPrefab.ID == compareItem.ID; });
            if(item != null)
            {
                item.ChangeStack(amount);
            }
            else
            {
                item = Instantiate(itemPrefab, transform);
                _items.Add(item);
                item.ChangeStack(amount);
            }
            OnItemAdded.OnNext(item);
            OnInventoryChanged.OnNext(Unit.Default);
            return true;
        }
        public bool HasItem(Item item)
        {
            return HasItem(item, out _);
        }
        public bool HasItem(Item item, out Item found)
        {
            found = FindItemByID(item.ID);
            if (found != null)
            {
                return found.Stack > 0;
            }
            return false;
        }
        public bool RemoveItem(Item item, int amount = 1)
        {
            if(amount < 0)
            {
                return GiveItem(item, -amount);
            }
            var found = FindItemByID(item.ID);
            if (found == null)
            {
                return false;
            }
            if(found.Stack > amount)
            {
                found.ChangeStack(-amount);
            }
            else
            {
                Destroy(found.gameObject);
                _items.Remove(found);
                OnItemRemoved.OnNext(found);
            }
            OnInventoryChanged.OnNext(Unit.Default);
            return true;
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

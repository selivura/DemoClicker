using R3;
using Selivura.DemoClicker.Persistence;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Zenject;

namespace Selivura.DemoClicker
{
    public class InventoryService : MonoBehaviour, ISaveable<SavedInventory>
    {
        [SerializeField] GameItemsList _gameItemList;
        [Inject] DiContainer _diContainer;
        [Inject] ShopService _shopService;

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
            if (!FindItem(itemPrice.Item, out Item found))
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
                item = _diContainer.InstantiatePrefab(itemPrefab, transform).GetComponent<Item>();
                _items.Add(item);
                item.ChangeStack(amount);
            }
            OnItemAdded.OnNext(item);
            OnInventoryChanged.OnNext(Unit.Default);
            return true;
        }
        public bool HasItem(Item item)
        {
            return FindItem(item, out _);
        }
        public bool FindItem(Item item, out Item found)
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

        public SavedInventory CaptureState()
        {
            SavedInventory savedInventory = new SavedInventory(new());
            foreach (var item in _items)
            {
                savedInventory.Items.Add(new SavedItem(item.ID, item.Stack));
            }
            return savedInventory;
        }

        public void RestoreState(SavedInventory state)
        {
            //for (int i = 0; i < _items.Count; i++)
            //{
            //    var item = _items[i];
            //    RemoveItem(item, item.Stack);
            //}

            foreach (var savedItem in state.Items)
            {
                GiveItem(_gameItemList.Items.Find((foundItem) => { return foundItem.ID == savedItem.ID; }), savedItem.Stack);
            }
        }
        
        public bool CanBuyMissingInShop(ItemPrice priceToPay, int amountToBuy, out int missingAmount, out int requiredCurrency, out ShopLot shopLot)
        {
            missingAmount = priceToPay.Price * amountToBuy;
            requiredCurrency = 0;

            if (!_shopService.FindItemForSale(priceToPay.Item, out shopLot))
                return false;

            if (!FindItem(shopLot.Price.Item, out Item funds))
                return false;

            if (FindItem(priceToPay.Item, out Item missingItem))
                missingAmount = priceToPay.Price * amountToBuy - missingItem.Stack;

            requiredCurrency = Mathf.CeilToInt(missingAmount / (float)shopLot.AmountForSale) * shopLot.Price.Price;

            if (funds.Stack < requiredCurrency)
                return false;
            return true;
        }
    }
    [System.Serializable]
    public class SavedInventory
    {
        public readonly List<SavedItem> Items;

        public SavedInventory(List<SavedItem> items)
        {
            Items = items;
        }
    }

    [System.Serializable]
    public class SavedItem
    {
        public readonly string ID;
        public readonly int Stack;

        public SavedItem(string iD, int stack)
        {
            ID = iD;
            Stack = stack;
        }
    }
}

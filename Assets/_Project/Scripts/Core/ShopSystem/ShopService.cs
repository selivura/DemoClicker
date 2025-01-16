using R3;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Selivura.DemoClicker
{
    public class ShopService : MonoBehaviour
    {
        [Inject] InventoryService _inventoryService;
        public List<ShopLot> ItemsForSale => _itemsForSale;
        [SerializeField] private List<ShopLot> _itemsForSale = new();
        public Subject<List<ShopLot>> OnItemsChanged { get; private set; } = new();

        private void Start()
        {
            OnItemsChanged.OnNext(ItemsForSale);
        }

        public void Buy(ShopLot lot)
        {
            var foundLot = _itemsForSale.Find((compLot) => { return (compLot.ItemForSale == lot.ItemForSale) && (compLot.Price.Price == lot.Price.Price) && (compLot.Price.Item == lot.Price.Item); });
            if (_inventoryService.CanAfford(foundLot.Price))
            {
                _inventoryService.GiveItem(lot.ItemForSale, lot.AmountForSale);
                _inventoryService.RemoveItem(lot.Price.Item, lot.Price.Price);
            }
        }
        public bool FindItemForSale(Item item, out ShopLot lot)
        {
            lot = _itemsForSale.Find((searchLot) =>
            {
                return searchLot.ItemForSale == item;
            });
            return lot != null;
        }
    }
    [System.Serializable]
    public class ShopLot
    {
        public Item ItemForSale;
        public int AmountForSale;
        public ItemPrice Price;

        private ShopLot() { }
        public ShopLot(Item item, int amount,ItemPrice price) 
        {
            ItemForSale = item;
            AmountForSale = amount;
            Price = price; 
        }
    }
}

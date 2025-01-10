using UnityEngine;

namespace Selivura.DemoClicker
{
    [System.Serializable]
    public class ItemPrice
    {
        public Item Item => _item;
        public int Price => _price;

        [SerializeField] private Item _item;
        [SerializeField] private int _price;

        public ItemPrice()
        {

        }
        public ItemPrice(Item item, int price)
        {
            _item = item;
            _price = price;
        }
    }
}

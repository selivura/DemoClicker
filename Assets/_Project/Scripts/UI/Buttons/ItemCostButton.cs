using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Selivura.DemoClicker.UI
{
    public class ItemCostButton : CustomButton
    {
        [SerializeField] private Item _item;
        [SerializeField] private int _price;

        [SerializeField] private Image _itemImage;
        [SerializeField] private TMP_Text _priceText;
        private void Start()
        {
            SetItemAndCost(_item, _price);
        }
        public void SetItemAndCost(ItemPrice price)
        {
            SetItemAndCost(price.Item, price.Price);
        }
        public void SetItemAndCost(Item item, int cost)
        {
            _item = item;
            _price = cost;
            _itemImage.sprite = item.Icon;
            _priceText.text = "x" + cost;
        }
    }
}

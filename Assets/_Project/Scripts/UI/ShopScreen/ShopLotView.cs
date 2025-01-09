using R3;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Selivura.DemoClicker.UI
{
    public class ShopLotView : MonoBehaviour
    {
        private ShopLot _shopLot;
        [SerializeField] Image _itemIcon;
        [SerializeField] Image _priceIcon;
        [SerializeField] TMP_Text _priceText;

        public Subject<ShopLot> OnSelected = new();
        public void SetLot(ShopLot shopLot)
        {
            _shopLot = shopLot;
            _itemIcon.sprite = shopLot.ItemForSale.Icon;
            _priceIcon.sprite = shopLot.Price.Item.Icon;
            _priceText.text = shopLot.Price.Price.ToString();
        }
        public void Select()
        {
            OnSelected.OnNext(_shopLot);
        }
    }
}

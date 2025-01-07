using R3;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Selivura.DemoClicker.UI
{
    public class FullItemDisplay : ItemView
    {
        [SerializeField] protected TMP_Text nameText;
        [SerializeField] protected TMP_Text descText;
        [SerializeField] protected TMP_Text stackText;
        [SerializeField] protected TMP_Text priceText;
        [SerializeField] protected Image icon;
        [SerializeField] protected Image qualityDisplay;

        [Tooltip("Pallete that will be used to set color of qualityDisplay. Must have colors for all qualities.")]
        [SerializeField] protected UIColorPalette colorPalette;

        [Tooltip("Sprites that will be used to set sprite of qualityDisplay. Must have sprites for all qualities.")]
        [SerializeField] protected List<Sprite> qualitySprites = new();

        private void Start()
        {
            viewModel.ItemName.Subscribe(OnItemNameUpdated).AddTo(disposable);
            viewModel.ItemDescription.Subscribe(OnItemDescUpdated).AddTo(disposable);

            viewModel.ItemStack.Subscribe(OnItemAmountUpdated).AddTo(disposable);
            viewModel.ItemIcon.Subscribe(OnItemIconUpdated).AddTo(disposable);
            viewModel.ItemQuality.Subscribe(OnItemQualityUpdated).AddTo(disposable);
            viewModel.ItemPrice.Subscribe(OnItemPriceUpdated).AddTo(disposable);
        }
        private void OnItemDescUpdated(string name)
        {
            descText.text = name;
        }
        private void OnItemNameUpdated(string name)
        {
            nameText.text = name;
        }
        private void OnItemAmountUpdated(int stack)
        {
            if (!stackText)
                return;
            stackText.text = "x" + stack.ToString();
        }
        private void OnItemPriceUpdated(int price)
        {
            if (!priceText)
                return;
            priceText.text = price.ToString();
        }
        private void OnItemIconUpdated(Sprite icon)
        {
            this.icon.sprite = icon;
        }
        private void OnItemQualityUpdated(ItemQuality quality)
        {
            qualityDisplay.color = colorPalette.Colors[(int)quality];
            qualityDisplay.sprite = qualitySprites[(int)quality];  
        }
    }
}

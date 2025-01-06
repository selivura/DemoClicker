using R3;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Selivura.DemoClicker.UI
{
    public class ItemInventoryView : ItemView
    {
        [SerializeField] protected Image icon;
        [SerializeField] protected Image qualityDisplay;
        [SerializeField] protected TMP_Text stackText;

        [Tooltip("Pallete that will be used to set color of qualityDisplay. Must have colors for all qualities.")]
        [SerializeField] protected UIColorPalette colorPalette;

        private void Start()
        {
            viewModel.ItemStack.Subscribe(OnItemAmountUpdated).AddTo(disposable);
            viewModel.ItemIcon.Subscribe(OnItemIconUpdated).AddTo(disposable);
            viewModel.ItemQuality.Subscribe(OnItemQualityUpdated).AddTo(disposable);
        }

        private void OnItemAmountUpdated(int stack)
        {
            stackText.text = stack.ToString();
        }
        private void OnItemIconUpdated(Sprite icon)
        {
            this.icon.sprite = icon;
        }
        private void OnItemQualityUpdated(ItemQuality quality)
        {
            qualityDisplay.color = colorPalette.Colors[(int)quality];
        }
    }
}

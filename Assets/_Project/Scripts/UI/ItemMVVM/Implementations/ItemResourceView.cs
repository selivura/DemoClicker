using R3;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Selivura.DemoClicker.UI
{
    public class ItemResourceView : ItemView
    {
        [SerializeField] protected Image icon;
        [SerializeField] protected TMP_Text stackText;

        private void Start()
        {
            viewModel.ItemStack.Subscribe(OnItemAmountUpdated).AddTo(disposable);
            viewModel.ItemIcon.Subscribe(OnItemIconUpdated).AddTo(disposable);
        }

        private void OnItemAmountUpdated(int stack)
        {
            stackText.text = stack.ToString();
        }
        private void OnItemIconUpdated(Sprite icon)
        {
            this.icon.sprite = icon;
        }
    }
}

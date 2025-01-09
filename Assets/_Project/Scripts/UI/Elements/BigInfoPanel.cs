using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

namespace Selivura.DemoClicker.UI
{
    public class BigInfoPanel : MonoBehaviour
    {
        [SerializeField] private Image _frame;
        [SerializeField] private Image _icon;
        [SerializeField] private Image _quality;
        [SerializeField] private TMP_Text _nameText;
        [SerializeField] private TMP_Text _amountText;
        [SerializeField] private TMP_Text _descText;

        [SerializeField] private UITheme _uiTheme;

        public Image Frame => _frame;
        public Image Icon => _icon;
        public Image Quality => _quality;
        public TMP_Text NameText => _nameText;
        public TMP_Text AmountText => _amountText;
        public TMP_Text DescText => _descText;

        public void ClearPanel()
        {
            NameText.text = "???";
            DescText.text = "???";
            AmountText.text = "x???";

            Icon.sprite = _uiTheme.PlaceholderIcon;
            Quality.sprite = _uiTheme.PlaceholderIcon;
            Quality.color = _uiTheme.QualityPalette.Colors[0];
        }

        public void SetQuality(ItemQuality quality)
        {
            Quality.sprite = _uiTheme.QualityIcons[(int)quality];
            Quality.color = _uiTheme.QualityPalette.Colors[(int)quality];
        }
    }
}

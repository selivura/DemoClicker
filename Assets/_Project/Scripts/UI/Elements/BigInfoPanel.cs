using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Selivura.DemoClicker.UI
{
    public class BigInfoPanel : MonoBehaviour
    {
        [SerializeField] private Image _frame;
        [SerializeField] private Image _icon;
        [SerializeField] private Image _splashArt;
        [SerializeField] private Image _quality;
        [SerializeField] private TMP_Text _nameText;
        [SerializeField] private TMP_Text _amountText;
        [SerializeField] private TMP_Text _descText;

        [SerializeField] private UITheme _uiTheme;

        public Image Frame => _frame;
        public Image SplashArt => _splashArt;
        public Image Icon => _icon;
        public Image Quality => _quality;
        public TMP_Text NameText => _nameText;
        public TMP_Text AmountText => _amountText;
        public TMP_Text DescText => _descText;

        public void ClearPanel()
        {
            _nameText.text = "???";
           _descText.text = "???";
            _amountText.text = "x???";

            _splashArt.sprite = _uiTheme.PlaceholderSprite;
            _icon.sprite = _uiTheme.PlaceholderIcon;

            _quality.sprite = _uiTheme.PlaceholderIcon;
            _quality.color = _uiTheme.QualityPalette.Colors[0];
        }

        public void SetQuality(ItemQuality quality)
        {
            _quality.sprite = _uiTheme.QualityIcons[(int)quality];
            _quality.color = _uiTheme.QualityPalette.Colors[(int)quality];
        }
    }
}

using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Selivura.DemoClicker.UI
{
    public class TextWithIcon : MonoBehaviour
    {
        [SerializeField] UITheme _uiTheme;
        public Image IconImage => _iconImage;
        [SerializeField] private Image _iconImage;
        public TMP_Text Text => _tmp;
        [SerializeField] private TMP_Text _tmp;

        public void Clear()
        {
            _iconImage.sprite = _uiTheme.PlaceholderIcon;
            _tmp.text = "???";
        }
    }
}

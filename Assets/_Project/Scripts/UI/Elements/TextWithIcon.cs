using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Selivura.DemoClicker.UI
{
    public class TextWithIcon : MonoBehaviour
    {
        public Image Image => _iconImage;
        [SerializeField] private Image _iconImage;
        public TMP_Text Text => _tmp;
        [SerializeField] private TMP_Text _tmp;
    }
}

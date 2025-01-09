using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Selivura.DemoClicker.UI
{
    public class TextWithIconButton : CustomButton
    {
        public TextWithIcon TextWithIcon => _textWithIcon;
        [SerializeField] private TextWithIcon _textWithIcon;
    }
}

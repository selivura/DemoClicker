using UnityEngine;

namespace Selivura.DemoClicker.UI
{
    public class TextAndIconButton : CustomButton
    {
        public TextWithIcon TextWithIcon => _textWithIcon;
        [SerializeField] TextWithIcon _textWithIcon;
    }
}

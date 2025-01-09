using UnityEngine;
using UnityEngine.UI;

namespace Selivura.DemoClicker.UI
{
    public class Frame : MonoBehaviour
    {
        [SerializeField] private CustomButton _button;
        [SerializeField] private Image _frameImage;
        [SerializeField] private Image _iconImage;
        public Image FrameImage => _frameImage;
        public Image IconImage => _iconImage;
        public CustomButton Button => _button;
    }
}

using UnityEngine;
using UnityEngine.UI;

namespace Selivura.DemoClicker.UI
{
    public class BannerSelectButton : CustomButton
    {
        public Image BannerIcon => _bannerIcon;
        [SerializeField] private Image _bannerIcon;
    }
}

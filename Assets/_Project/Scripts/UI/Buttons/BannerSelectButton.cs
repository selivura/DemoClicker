using UnityEngine;
using UnityEngine.UI;

namespace Selivura.DemoClicker.UI
{
    public class BannerSelectButton : CustomButton
    {
        [SerializeField] private Image _bannerIcon;
        private GachaBannerHolder _bannerHolder;

        public void SetBanner(GachaBannerHolder banner)
        {
            _bannerHolder = banner;
            _bannerIcon.sprite = _bannerHolder.Banner.Icon;
        }
    }
}

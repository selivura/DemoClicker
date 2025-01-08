using R3;
using UnityEngine;

namespace Selivura.DemoClicker.UI
{
    public class GachaViewModel : MonoBehaviour
    {
        GachaBannerHolder _currentGachaInstance;
        public Subject<GachaBannerHolder> OnGachaInstanceChanged = new();
        public void SetBannerHolder(GachaBannerHolder gachaInstance)
        {
            _currentGachaInstance = gachaInstance;
            OnGachaInstanceChanged.OnNext(_currentGachaInstance);
        }
    }
}

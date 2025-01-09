using R3;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Selivura.DemoClicker.UI
{
    public class GachaViewModel : MonoBehaviour
    {
        [Inject] GachaService _gachaService;
        GachaBannerHolder _currentGachaInstance;

        public Subject<GachaBannerHolder> OnGachaInstanceChanged = new();
        public Subject<List<GachaBannerHolder>> OnBannersUpdated = new();

        private CompositeDisposable _disposable = new();
        private void Start()
        {
            SelectBannerHolder(0);
            OnBannersUpdated.OnNext(_gachaService.CurrentHolders);
        }
        public void SelectBannerHolder(int index)
        {
            _currentGachaInstance = _gachaService.CurrentHolders[index];
            OnGachaInstanceChanged.OnNext(_currentGachaInstance);

            _gachaService.OnBannersUpdated.Subscribe(OnBannersUpdated.OnNext).AddTo(_disposable);
        }
        private void OnDestroy()
        {
            _disposable.Dispose();
        }
    }
}

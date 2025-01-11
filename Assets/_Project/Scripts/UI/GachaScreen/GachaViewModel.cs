using R3;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Selivura.DemoClicker.UI
{
    public class GachaViewModel : MonoBehaviour
    {
        [Inject] GachaService _gachaService;
        [Inject] InventoryService _inventoryService;

        GachaBannerHolder _currentBannerHolder;

        public Subject<GachaBannerHolder> OnBannerHolderChanged = new();
        public Subject<List<GachaBannerHolder>> OnBannersUpdated = new();
        public Subject<Unit> OnInventoryUpdated = new();

        private CompositeDisposable _disposable = new();
        private void Start()
        {
            _inventoryService.OnInventoryChanged.Subscribe(_ => OnInventoryUpdated.OnNext(Unit.Default)).AddTo(_disposable);
            OnBannersUpdated.OnNext(_gachaService.CurrentHolders);
            SelectBannerHolder(0);
        }
        public void SelectBannerHolder(int index)
        {
            _currentBannerHolder = _gachaService.CurrentHolders[index];
            OnBannerHolderChanged.OnNext(_currentBannerHolder);

            _gachaService.OnBannersUpdated.Subscribe(OnBannersUpdated.OnNext).AddTo(_disposable);
        }
        private void OnDestroy()
        {
            _disposable.Dispose();
        }
    }
}

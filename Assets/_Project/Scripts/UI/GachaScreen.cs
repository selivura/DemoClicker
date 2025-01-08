using R3;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Selivura.DemoClicker.UI
{
    public class GachaScreen : MonoBehaviour
    {
        [Inject] GachaService _gachaService;
        [SerializeField] GachaViewModel _viewModel;
        [SerializeField] BannerSelectButton _bannerSelectButtonPrefab;
        [SerializeField] private Transform _buttonsContainer;

        private List<BannerSelectButton> _spawnedButtons = new();

        private CompositeDisposable _disposable = new();
        private void Start()
        {
            _gachaService.OnBannersUpdated.Subscribe(OnBannersUpdated).AddTo(_disposable);
            ShowBanner(_gachaService.CurrentHolders[0]);
            OnBannersUpdated(_gachaService.CurrentHolders);
        }
        private void OnBannersUpdated(List<GachaBannerHolder> banners)
        {
            foreach (var button in _spawnedButtons)
            {
                button.OnButtonClick.RemoveAllListeners();
                Destroy(button.gameObject);
            }
            _spawnedButtons.Clear();
            foreach (var banner in banners)
            {
                var spawned = Instantiate(_bannerSelectButtonPrefab, _buttonsContainer);
                spawned.SetBanner(banner);
                spawned.OnButtonClick.AddListener(() => ShowBanner(banner));
            }
        }
        private void ShowBanner(GachaBannerHolder holder)
        {
            _viewModel.SetBannerHolder(holder);
        }
        private void OnDestroy()
        {
            _disposable.Dispose();
        }
    }
}

using R3;
using Selivura.DemoClicker.UI;
using System.Collections.Generic;
using UnityEngine;

namespace Selivura.DemoClicker
{
    public class GachaView : MonoBehaviour
    {
        private GachaBannerHolder _currentHolder;

        [SerializeField] GachaViewModel _viewModel;
        [SerializeField] Transform _bannerFrame;
        [SerializeField] private TextWithIconButton _x1Button;
        [SerializeField] private TextWithIconButton _x10Button;

        private GameObject _spawnedGachaBanner;
        private CompositeDisposable _disposable = new();

        [SerializeField] BannerSelectButton _bannerSelectButtonPrefab;
        [SerializeField] private Transform _buttonsContainer;
        private void OnBannersUpdated(List<GachaBannerHolder> banners)
        {
            for (int i = 0; i < _buttonsContainer.childCount; i++)
            {
                Destroy(_buttonsContainer.GetChild(i).gameObject);
            }
            for (int i = 0; i < banners.Count; i++)
            {
                var button = Instantiate(_bannerSelectButtonPrefab, _buttonsContainer);
                
                button.BannerIcon.sprite = banners[i].Banner.Icon;

                //if I remove index and use i instaed it will always set to banners.Count for some reason
                int index = i;
                button.OnButtonClick.AddListener(() => _viewModel.SelectBannerHolder(index)); 
            }
        }
        private void Awake()
        {
            _viewModel.OnGachaInstanceChanged.Subscribe(OnBannerChanged).AddTo(_disposable);
            _viewModel.OnBannersUpdated.Subscribe(OnBannersUpdated).AddTo(_disposable);
            _x1Button.OnButtonClick.AsObservable().Subscribe(_ => OnX1ButtonClicked()).AddTo(_disposable);
            _x10Button.OnButtonClick.AsObservable().Subscribe(_ => OnX10ButtonClicked()).AddTo(_disposable);
        }
        private void OnX1ButtonClicked()
        {
            _currentHolder.Pull();
        }
        private void OnX10ButtonClicked()
        {
            _currentHolder.Pull(10);
        }
        private void OnBannerChanged(GachaBannerHolder holder)
        {
            Destroy(_spawnedGachaBanner);
            _currentHolder = holder;
            _spawnedGachaBanner = Instantiate(_currentHolder.Banner.BannerGraphicsPrefab, _bannerFrame);

            SetupButton(_x1Button, 1); 
            SetupButton(_x10Button, 10);
        }
        private void SetupButton(TextWithIconButton button, int pullAmount)
        {
            button.TextWithIcon.Image.sprite = _currentHolder.Banner.Key.Item.Icon;
            button.TextWithIcon.Text.text = "x" + _currentHolder.Banner.Key.Price * pullAmount;
        }
        private void OnDestroy()
        {
            _disposable.Dispose();
        }
    }
}
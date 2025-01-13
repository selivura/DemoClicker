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

        [SerializeField] private BannerDropResultsWindow _resultsWindow;

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

                //if I remove index and use i instaed, it will always set to banners.Count for some reason
                int index = i;
                button.OnButtonClick.AddListener(() => _viewModel.SelectBannerHolder(index)); 
            }
        }
        private void Awake()
        {
            _viewModel.OnBannerHolderChanged.Subscribe(OnBannerChanged).AddTo(_disposable);
            _viewModel.OnBannersUpdated.Subscribe(OnBannersUpdated).AddTo(_disposable);
            _viewModel.OnInventoryUpdated.Subscribe(_ => UpdateButtons()).AddTo(_disposable);

            _x1Button.OnButtonClick.AsObservable().Subscribe(_ => OnX1ButtonClicked()).AddTo(_disposable);
            _x10Button.OnButtonClick.AsObservable().Subscribe(_ => OnX10ButtonClicked()).AddTo(_disposable);
        }
        private void UpdateButtons()
        {
            UpdateButton(_x1Button, 1);
            UpdateButton(_x10Button, 10);
        }
        private void OnX1ButtonClicked()
        {
            _resultsWindow.SetDrop(_currentHolder.Pull());
            _resultsWindow.ShowWindow(); 
            UpdateButton(_x1Button, 1);
        }
        private void OnX10ButtonClicked()
        {
            _resultsWindow.SetDrop(_currentHolder.Pull(10));
            _resultsWindow.ShowWindow();
            UpdateButton(_x10Button, 10);
        }
        private void OnBannerChanged(GachaBannerHolder holder)
        {
            Destroy(_spawnedGachaBanner);
            _currentHolder = holder;
            _spawnedGachaBanner = Instantiate(_currentHolder.Banner.BannerGraphicsPrefab, _bannerFrame);

            UpdateButtons();
        }
        private void UpdateButton(TextWithIconButton button, int pullAmount)
        {
            button.TextWithIcon.IconImage.sprite = _currentHolder.Banner.Key.Item.Icon;
            button.TextWithIcon.Text.text = "x" + _currentHolder.Banner.Key.Price * pullAmount;
            button.ButtonComponent.interactable = _currentHolder.CanPull(pullAmount);
        }
        private void OnDestroy()
        {
            _disposable.Dispose();
        }
    }
}

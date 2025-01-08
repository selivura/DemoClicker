using R3;
using Selivura.DemoClicker.UI;
using UnityEngine;

namespace Selivura.DemoClicker
{
    public class GachaView : MonoBehaviour
    {
        [SerializeField] GachaViewModel _viewModel;
        [SerializeField] Transform _bannerFrame;
        private GachaBannerHolder _currentHolder;
        [SerializeField] private ItemCostButton _x1Button;
        [SerializeField] private ItemCostButton _x10Button;

        private GameObject _spawnedGachaBanner;
        private CompositeDisposable _disposable = new();

        private void Start()
        {
            _viewModel.OnGachaInstanceChanged.Subscribe(OnGachaChanged).AddTo(_disposable);
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
        private void OnGachaChanged(GachaBannerHolder holder)
        {
            Destroy(_spawnedGachaBanner);
            _currentHolder = holder;
            _spawnedGachaBanner = Instantiate(_currentHolder.Banner.BannerGraphicsPrefab, _bannerFrame);
            _x1Button.SetItemAndCost(holder.Banner.Key);
            _x10Button.SetItemAndCost(holder.Banner.Key.Item, holder.Banner.Key.Price * 10);
        }
        private void OnDestroy()
        {
            _disposable.Dispose();
        }
    }
}

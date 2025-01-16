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
        [Inject] ShopService _shopService;

        GachaBannerHolder _currentBannerHolder;

        public Subject<GachaBannerHolder> OnBannerHolderChanged = new();
        public Subject<List<GachaBannerHolder>> OnBannersUpdated = new();
        public Subject<List<GachaDrop>> OnPulled = new();

        public Subject<int> OnCanBuyMissingItems = new();
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
        public void Pull(int amount)
        {
            if (!_inventoryService.CanAfford(_currentBannerHolder.Banner.Key, amount))
            {
                if (_inventoryService.CanBuyMissingInShop(_currentBannerHolder.Banner.Key, amount, out _, out _, out _))
                {
                    OnCanBuyMissingItems.OnNext(amount);
                }
                return;
            }
            OnPulled.OnNext(_currentBannerHolder.Pull(amount));
        }
        public bool CanPull(int amount)
        {
            if (_inventoryService.CanAfford(_currentBannerHolder.Banner.Key, amount))
                return true;
            if (_inventoryService.CanBuyMissingInShop(_currentBannerHolder.Banner.Key, amount, out _, out _, out _))
                return true;
            return false;
        }
        public bool GetMissingItemsInfo(int amountToBuy, out int missingAmount, out int requiredCurrency, out ShopLot shopLot, out Item misingItem)
        {
            misingItem = _currentBannerHolder.Banner.Key.Item;
            return _inventoryService.CanBuyMissingInShop(_currentBannerHolder.Banner.Key, amountToBuy, out missingAmount, out requiredCurrency, out shopLot);
        }
        public void BuyMissingAndPull(int pullAmount)
        {
            if (!_inventoryService.CanBuyMissingInShop(_currentBannerHolder.Banner.Key, pullAmount, out int missingAmount, out _, out ShopLot shopLot))
                return;
            for (int i = 0; i < missingAmount; i++)
            {
                _shopService.Buy(shopLot);
            }
            OnPulled.OnNext(_currentBannerHolder.Pull(pullAmount));
        }
    }
}

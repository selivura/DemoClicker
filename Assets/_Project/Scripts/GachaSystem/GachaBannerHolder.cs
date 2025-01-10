using R3;
using UnityEngine;

namespace Selivura.DemoClicker
{
    public class GachaBannerHolder
    {
        public BannerPullData BannerPullData => _currentPullData;
        BannerPullData _currentPullData = new();

        public GachaBanner Banner => _banner;
        GachaBanner _banner;

        InventoryService _inventoryService;

        public Subject<Unit> OnPull = new();

        public GachaBannerHolder(BannerPullData pullData, GachaBanner banner, InventoryService inventoryService)
        {
            _currentPullData = pullData;
            _banner = banner;
            _inventoryService = inventoryService;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key">Index of ItemPrice in banner scriptable object</param>
        /// <param name="amount">amount of pulls to check</param>
        /// <returns></returns>
        public bool CanPull(int amount)
        {
            ItemPrice itemPrice = _banner.Key;
            if (!_inventoryService.CanAfford(itemPrice,amount))
                return false;
            return true;
        }
        public void Pull(int amount = 1)
        {
            if(!CanPull(amount)) return;

            for (int i = 0; i < amount; i++)
            {
                GachaDrop drop = _banner.Pull(_currentPullData);
                _inventoryService.GiveItem(drop);
                _inventoryService.RemoveItem(_banner.Key.Item, _banner.Key.Price);
            }
            OnPull.OnNext(Unit.Default);
        }
    }
}

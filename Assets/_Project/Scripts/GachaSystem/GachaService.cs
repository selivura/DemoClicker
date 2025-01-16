using R3;
using Selivura.DemoClicker.Persistence;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Selivura.DemoClicker
{
    public class GachaService : MonoBehaviour, ISaveable<List<BannerSaveData>>
    {
        [Inject] InventoryService _inventoryService;

        [SerializeField] List<GachaBanner> _starterBanners= new();

        public List<GachaBanner> CurrentBanners { get; private set; } = new();

        public List<GachaBannerHolder> CurrentHolders { get; private set; } = new();

        public Subject<List<GachaBannerHolder>> OnBannersUpdated = new();

        public Subject<GachaBannerHolder> OnBannerAdded = new();

        private List<BannerSaveData> _saveData = new();

        CompositeDisposable _disposable = new();
        private void Awake()
        {
            foreach (var banner in _starterBanners)
            {
                AddBanner(banner);
            }
        }
        public void AddBanner(GachaBanner banner)
        {
            CurrentBanners.Add(banner);
            var savedBannerData = FindBannerSaveByID(banner.ID);

            GachaBannerHolder holder = new(new(), banner, _inventoryService);
            if(savedBannerData != null)
            {
                if (savedBannerData.BannerID == banner.ID)
                {
                    holder = new(savedBannerData.PullData, banner, _inventoryService);
                }
            }

            CurrentHolders.Add(holder);
            OnBannersUpdated.OnNext(CurrentHolders);
        }
        private void OnDestroy()
        {
            _disposable.Dispose();
        }

        public List<BannerSaveData> CaptureState()
        {
            _saveData.Clear();
            foreach (var holders in CurrentHolders)
            {
                _saveData.Add(new(holders.Banner.ID, holders.BannerPullData));
            }
            return _saveData;
        }

        public void RestoreState(List<BannerSaveData> state)
        {
            _saveData.Clear();
            foreach (var item in state)
            {
                _saveData.Add(item);
            }
        }
        public BannerSaveData FindBannerSaveByID(string ID)
        {
            return _saveData.Find((data) => { return data.BannerID == ID; });
        }
    }
}

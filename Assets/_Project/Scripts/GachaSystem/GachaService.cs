using R3;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Selivura.DemoClicker
{
    public class GachaService : MonoBehaviour
    {
        [Inject] InventoryService _inventoryService;

        public List<GachaBanner> CurrentBanners => _currentBanners;
        [SerializeField] private List<GachaBanner> _currentBanners = new();

        public List<GachaBannerHolder> CurrentHolders { get; private set; } = new();

        public Subject<List<GachaBannerHolder>> OnBannersUpdated = new();

        [Tooltip("Editor only check to call OnBannersUpdated")]
        [SerializeField] private bool _updateBannersNow = false;

        private void Update()
        {
            if(_updateBannersNow)
            {
                _updateBannersNow = false;
                UpdateBannerHolders();
            }
        }

        private void Awake()
        {
            UpdateBannerHolders();
        }
        public void AddBanner(GachaBanner banner)
        {
            _currentBanners.Add(banner);
            UpdateBannerHolders();
        }
        private void UpdateBannerHolders()
        {
            CurrentHolders.Clear();
            foreach (var banner in _currentBanners)
            {
                CurrentHolders.Add(new GachaBannerHolder(new(), banner, _inventoryService));
            }
            OnBannersUpdated.OnNext(CurrentHolders);
        }

    }
}

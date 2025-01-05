using R3;
using SelivuraLib.Utility;
using UnityEngine;
using Zenject;

namespace Selivura.DemoClicker
{
    public class OnClickEffectApplier : MonoBehaviour
    {
        [Inject] private ClickerService _clicker;
        [Inject] private LevelingService _levelingService;
        [Inject] private PlayerStatsService _playerStatsService;
        [Inject] private InventoryService _inventoryService;
        [Inject] private GameRulesConfig _gameRulesConfig;
        private CompositeDisposable _disposable = new();
        private void Start()
        {
            _clicker.OnClick.Subscribe(_ => OnClick()).AddTo(_disposable);
        }
        private void OnClick()
        {
            int xpPerClick = (int)_playerStatsService.Stats[StatType.ExpPerClick].Value;
            int coinPerClick = (int)_playerStatsService.Stats[StatType.CoinAmount].Value;
            float coinChance = _playerStatsService.Stats[StatType.CoinChance].Value;
            _levelingService.AddExperience(xpPerClick);
            if (Utilities.SimpleRoll(coinChance))
            {
                _inventoryService.GiveItem(_gameRulesConfig.CoinPrefab, coinPerClick);
            }
        }
        private void OnDestroy()
        {
            _disposable.Dispose();
        }
    }
}

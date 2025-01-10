using R3;
using UnityEngine;
using Zenject;

namespace Selivura.DemoClicker
{
    public class ClickerService : MonoBehaviour
    {
        [Inject] private PlayerStatsService _playerStatsService;
        [Inject] private InventoryService _inventoryService;
        [Inject] private GameRulesConfig _gameRulesConfig;
        public readonly Subject<Unit> OnClick = new();
        public void Click()
        {
            int coinPerClick = (int)_playerStatsService.Stats[StatType.CoinPerClick].Value;
            _inventoryService.GiveItem(_gameRulesConfig.CoinPrefab, coinPerClick);
            OnClick.OnNext(Unit.Default);
        }
    }
}


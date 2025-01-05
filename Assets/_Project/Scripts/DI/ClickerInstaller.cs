using UnityEngine;
using Zenject;
namespace Selivura.DemoClicker
{
    public class ClickerInstaller : MonoInstaller
    {
        [SerializeField] private GameRulesConfig _gameRules;
        [SerializeField] private ClickerService _clickerService;
        [SerializeField] private InventoryService _inventoryService;
        [SerializeField] private PlayerStatsService _playerStatsService;
        [SerializeField] private LevelingService _levelingService;

        public override void InstallBindings()
        {
            Container.Bind<GameRulesConfig>().FromInstance(_gameRules);
            Container.Bind<ClickerService>().FromInstance(_clickerService);
            Container.Bind<InventoryService>().FromInstance(_inventoryService);
            Container.Bind<PlayerStatsService>().FromInstance(_playerStatsService);
            Container.Bind<LevelingService>().FromInstance(_levelingService);
        }
    }

}
using Selivura.DemoClicker.Persistence;
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
        [SerializeField] private DataSavingService _dataSavingService;
        [SerializeField] private GachaService _gachaService;
        [SerializeField] private ShopService _shopService;

        public override void InstallBindings()
        {
            Container.Bind<GameRulesConfig>().FromInstance(_gameRules);
            Container.Bind<ClickerService>().FromInstance(_clickerService);
            Container.Bind<InventoryService>().FromInstance(_inventoryService);
            Container.Bind<PlayerStatsService>().FromInstance(_playerStatsService);
            Container.Bind<DataSavingService>().FromInstance(_dataSavingService);
            Container.Bind<GachaService>().FromInstance(_gachaService);
            Container.Bind<ShopService>().FromInstance(_shopService);
            //Container.Bind<DiContainer>().FromInstance(Container);
        }
    }

}
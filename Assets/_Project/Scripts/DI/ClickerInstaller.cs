using UnityEngine;
using Zenject;
namespace Selivura.DemoClicker
{
    public class ClickerInstaller : MonoInstaller
    {
        [SerializeField] private ClickerService _clickerService;

        public override void InstallBindings()
        {
            Container.Bind<ClickerService>().FromInstance(_clickerService);
        }
    }

}
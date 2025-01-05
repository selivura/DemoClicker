using UnityEngine;
using Zenject;

namespace Selivura.DemoClicker
{
    public class ClickButton : ButtonBase
    {
        [Inject]
        ClickerService _clickerService;

        protected override void OnClick()
        {
            _clickerService.Click();
        }
    }
}

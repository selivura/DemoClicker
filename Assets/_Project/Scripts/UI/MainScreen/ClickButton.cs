using Zenject;

namespace Selivura.DemoClicker.UI
{
    public class ClickButton : CustomButton
    {
        [Inject]
        ClickerService _clickerService;

        protected override void OnClick()
        {
            _clickerService.Click();
        }
    }
}

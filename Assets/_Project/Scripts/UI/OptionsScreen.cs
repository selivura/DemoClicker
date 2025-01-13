using R3;
using Selivura.DemoClicker.Persistence;
using Selivura.DemoClicker.UI;
using UnityEngine;
using Zenject;

namespace Selivura.DemoClicker
{
    public class OptionsScreen : MonoBehaviour
    {
        [SerializeField] ConfirmationWindow _confirmationWindow;
        [Inject] DataSavingService _dataSavingService;

        public void DeleteButton()
        {
            _confirmationWindow.SetDefaultText();
            _confirmationWindow.SetTextMessage("DELETE YOUR SAVE? PERMANENTLY.");
            _confirmationWindow.OnConfirm.Subscribe(_ => ConfirmDelete());
            _confirmationWindow.ShowWindow();
            
        }
        public void QuitButton()
        {
            _confirmationWindow.SetDefaultText();
            _confirmationWindow.SetTextMessage("Are you sure you want to quit?");
            _confirmationWindow.OnConfirm.Subscribe(_ => ConfirmQuit());
            _confirmationWindow.ShowWindow();
        }
        private void ConfirmDelete()
        {
            _dataSavingService.DeleteGame();
        }
        private void ConfirmQuit()
        {
            Application.Quit();
        }
    }
}

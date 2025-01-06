using System.Collections.Generic;
using UnityEngine;

namespace Selivura.DemoClicker.UI
{
    public class ScreenSwitcher : MonoBehaviour
    {
        [SerializeField] private List<UIScreen> _uiScreens = new();
        [SerializeField] private int _currentScreen = 0;
        private void Start()
        {
            foreach (var screen in _uiScreens)
            {
                screen.gameObject.SetActive(false);
            }
            _uiScreens[_currentScreen].gameObject.SetActive(true);
        }
        public void SwitchScreen(int screenId)
        {
            _uiScreens[_currentScreen].gameObject.SetActive(false);
            _currentScreen = screenId;
            _uiScreens[_currentScreen].gameObject.SetActive(true);
        }
    }
}

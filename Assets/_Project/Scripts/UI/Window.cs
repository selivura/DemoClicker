using UnityEngine;
using UnityEngine.Events;

namespace Selivura.DemoClicker.UI
{
    public class Window : MonoBehaviour
    {
        [SerializeField] protected UITheme uiTheme;
        public UnityEvent OnShow;
        public UnityEvent OnHide;

        public void ShowWindow()
        {
            OnShow?.Invoke();
        }
        public void HideWindow()
        {
            OnHide?.Invoke();
        }
    }
}

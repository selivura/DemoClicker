using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Selivura.DemoClicker.UI
{
    [RequireComponent(typeof(Button))]
    public class CustomButton : MonoBehaviour
    {
        public Button ButtonComponent { get; private set; }
        public UnityEvent OnButtonClick;
        private void Awake()
        {
            ButtonComponent = GetComponent<Button>();   
            ButtonComponent.onClick.AddListener(Click);
        }
        private void Click()
        {
            OnClick();
            OnButtonClick?.Invoke();
        }
        protected virtual void OnClick()
        {

        }
    }
}

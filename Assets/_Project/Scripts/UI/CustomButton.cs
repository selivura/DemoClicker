using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Selivura.DemoClicker.UI
{
    [RequireComponent(typeof(Button))]
    public abstract class CustomButton : MonoBehaviour
    {
        private Button _button;
        public UnityEvent OnButtonClick;
        private void Awake()
        {
            _button = GetComponent<Button>();   
            _button.onClick.AddListener(Click);
        }
        public void Click()
        {
            OnClick();
            OnButtonClick?.Invoke();
        }
        protected virtual void OnClick()
        {

        }
    }
}

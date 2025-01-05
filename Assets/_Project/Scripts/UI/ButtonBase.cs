using UnityEngine;
using UnityEngine.UI;

namespace Selivura.DemoClicker
{
    [RequireComponent(typeof(Button))]
    public abstract class ButtonBase : MonoBehaviour
    {
        private Button _button;
        private void Awake()
        {
            _button = GetComponent<Button>();   
            _button.onClick.AddListener(OnClick);
        }
        public void Click()
        {
            OnClick();
        }
        protected abstract void OnClick();
    }
}

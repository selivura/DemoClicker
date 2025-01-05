using UnityEngine;

namespace Selivura.DemoClicker
{
    public abstract class ButtonBase : MonoBehaviour
    {
        public void Click()
        {
            OnClick();
        }
        protected abstract void OnClick();
    }
}

using R3;
using UnityEngine;

namespace Selivura.DemoClicker
{
    public class ClickerService : MonoBehaviour
    {
        public readonly Subject<Unit> OnClick = new();
        public void Click()
        {
            OnClick.OnNext(Unit.Default);
            Debug.Log("CLickj");
        }
    }
}


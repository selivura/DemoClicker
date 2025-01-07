using R3;
using UnityEngine;

namespace Selivura.DemoClicker.UI
{
    public abstract class ItemView : MonoBehaviour
    {
        [SerializeField] protected ItemViewModel viewModel;

        protected CompositeDisposable disposable = new();

        private void OnDestroy()
        {
            disposable.Dispose();
        }
        public void SelectSlot()
        {
            viewModel.SelectSlot();
        }
    }
}

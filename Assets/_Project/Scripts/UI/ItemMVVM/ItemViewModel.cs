using R3;
using UnityEngine;

namespace Selivura.DemoClicker.UI
{
    public class ItemViewModel : MonoBehaviour
    {
        public ReactiveProperty<Sprite> ItemIcon = new();
        public ReactiveProperty<int> ItemStack = new();
        public ReactiveProperty<ItemQuality> ItemQuality = new();

        protected Item displayItem;
        protected CompositeDisposable disposable = new();

        protected void UpdateItem(Item item)
        {
            ItemStack.Value = item.Stack;
            ItemIcon.Value = item.Icon;
            ItemQuality.Value = item.Quality;
        }
        public void UpdateItem(Sprite icon, int stack, ItemQuality quality)
        {
            ItemStack.Value = stack;
            ItemIcon.Value = icon;
            ItemQuality.Value = quality;
        }
        public void SetDisplayItem(Item item)
        {
            displayItem = item;
            displayItem.OnStateChanged.Subscribe(UpdateItem).AddTo(disposable);
            UpdateItem(item);
        }
        private void OnDestroy()
        {
            disposable.Dispose();
        }
    }
}

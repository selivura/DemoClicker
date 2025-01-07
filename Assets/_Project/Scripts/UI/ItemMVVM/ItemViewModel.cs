using R3;
using UnityEngine;

namespace Selivura.DemoClicker.UI
{
    public class ItemViewModel : MonoBehaviour
    {
        public ReactiveProperty<Sprite> ItemIcon = new();
        public ReactiveProperty<Sprite> ItemSprite = new();
        public ReactiveProperty<string> ItemName = new();
        public ReactiveProperty<string> ItemDescription = new();
        public ReactiveProperty<int> ItemStack = new();
        public ReactiveProperty<int> ItemPrice = new();
        public ReactiveProperty<ItemQuality> ItemQuality = new();

        protected Item displayItem;
        protected CompositeDisposable disposable = new();

        public Subject<Item> OnSlotSelected = new Subject<Item>();

        protected void UpdateItem(Item item)
        {
            UpdateItem(item.Icon, item.Sprite, item.Stack, item.Quality, item.Name, item.Description, item.Price);
        }
        public void UpdateItem(Sprite icon, Sprite sprite, int stack, ItemQuality quality, string itemName, string itemDesc, int price)
        {
            ItemStack.Value = stack;
            ItemPrice.Value = price;
            ItemIcon.Value = icon;
            ItemQuality.Value = quality;
            ItemSprite.Value = sprite;
            ItemName.Value = itemName;
            ItemDescription.Value = itemDesc;
        }
        public void SetDisplayItem(Item item)
        {
            displayItem = item;
            displayItem.OnStateChanged.Subscribe(UpdateItem).AddTo(disposable);
            UpdateItem(item);
        }
        public void SelectSlot()
        {
            OnSlotSelected.OnNext(displayItem);
        }
        private void OnDestroy()
        {
            disposable.Dispose();
        }
    }
}

using UnityEngine;

namespace Selivura.DemoClicker
{
    public class GachaDrop : ItemDrop
    {
        public ItemQuality DropQuality => _quality;
        [SerializeField] private ItemQuality _quality;
        public GachaDrop()
        {

        }
        public GachaDrop(ItemQuality quality, Item item, int amount) : base(item, amount)
        {
            _quality = quality;
            this.item = item;
            this.amount = amount;
        }
    }
}

using UnityEngine;

namespace Selivura.DemoClicker
{
    [CreateAssetMenu(menuName = "LootTables/Items")]
    public class ItemLootTable : LootTable<ItemDrop>
    {
        
    }
    [System.Serializable]
    public class ItemDrop
    {
        public  Item Item => item;
        [SerializeField] protected Item item;
        public  int Amount => amount;
        [SerializeField] protected int amount;
        public ItemDrop()
        {

        }
        public ItemDrop(Item item, int amount)
        {
            this.item = item;
            this.amount = amount;
        }
    }
}

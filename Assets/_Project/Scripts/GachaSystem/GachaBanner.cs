using AYellowpaper.SerializedCollections;
using System.Collections.Generic;
using UnityEngine;

namespace Selivura.DemoClicker
{
    [CreateAssetMenu]
    public class GachaBanner : ScriptableObject
    {
        [Header("Basic info")]

        public Sprite Icon => _icon;
        [SerializeField] private Sprite _icon;

        public GameObject BannerGraphicsPrefab => _bannerGraphicsPrefab;
        [SerializeField] private GameObject _bannerGraphicsPrefab;

        public string Name => _name;
        [SerializeField] private string _name;

        public string Description => _description;
        [SerializeField][TextArea] private string _description; 
        public string ID => _ID;

        [SerializeField][TextArea] private string _ID = "gacha_banner_default";

        public ItemPrice Key => _key;
        [SerializeField] ItemPrice _key;

        [Header("Pity")]

        public ItemQuality SoftPityResetMinimalQuality => _softPityResetMinimalQuality;
        [SerializeField] private ItemQuality _softPityResetMinimalQuality = ItemQuality.S;

        public float SoftPityPerPull => _softPityPerPull;
        [SerializeField] private float _softPityPerPull = 0.1f;

        public Dictionary<ItemQuality, int> HardPityRequirements => _hardPityRequirements;
        [SerializedDictionary("Tier", "Guranteed amount of pulls")]
        [SerializeField] SerializedDictionary<ItemQuality, int> _hardPityRequirements = new();

        [Header("Drops")]

        public LootTable<ItemQuality> TierChances => _tierChances;
        [SerializeField] LootTable<ItemQuality> _tierChances;

        public Dictionary<ItemQuality, LootTable<ItemDrop>> BannerDrops => _bannerDrops;
        [SerializedDictionary("Tier", "LootTable")]
        [SerializeField] SerializedDictionary<ItemQuality, LootTable<ItemDrop>> _bannerDrops = new();

        public GachaDrop Pull(BannerPullData data)
        {
            data.Pulls++;

            if(data.HardPity == null)
            {
                data.CreateHardPity(this);
            }

            data.IncreaseHardPity();

            ItemQuality dropTier = _tierChances.GetDrop(default, data.SoftPity);

            ItemQuality minimumQuality = ItemQuality.B;

            foreach (var keyValuePair in _hardPityRequirements)
            {
                int current = data.HardPity[keyValuePair.Key];
                if (keyValuePair.Value <= current)
                {
                    minimumQuality = keyValuePair.Key;
                }
            }

            if (dropTier < minimumQuality)
            {
                dropTier = minimumQuality;
            }

            data.ResetHardPity(dropTier);

            ItemDrop itemDrop = _bannerDrops[dropTier].GetDrop();
            GachaDrop gachaDrop = new GachaDrop(dropTier, itemDrop.Item, itemDrop.Amount);

            if ((int)gachaDrop.DropQuality < (int)_softPityResetMinimalQuality)
            {
                data.SoftPity += _softPityPerPull;
            }
            else
            {
                data.SoftPity = 0;
            }


            Debug.Log($"You pulled [{dropTier}] {gachaDrop.Item.Name} x {gachaDrop.Amount}");
            Debug.Log($"---BANNER STATS---");
            Debug.Log($"Soft pity: {data.SoftPity}");
            foreach (var item in data.HardPity)
            {
                Debug.Log($"[{item.Key}] pity: {item.Value}");
            }
            return gachaDrop;
        }
    }
    [System.Serializable]
    public class ItemPrice
    {
        public Item Item => _item;
        public int Price => _price;

        [SerializeField] private Item _item;
        [SerializeField] private int _price;

        public ItemPrice()
        {

        }
        public ItemPrice(Item item, int price)
        {
            _item = item;
            _price = price;
        }
    }
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
    [System.Serializable]
    public class BannerPullData
    {
        public int Pulls = 0;
        public float SoftPity = 0.0f;

        public Dictionary<ItemQuality, int> HardPity => _hardPity;

        Dictionary<ItemQuality, int> _hardPity;
        public Dictionary<ItemQuality, int> CreateHardPity(GachaBanner banner)
        {
            _hardPity = new();
            foreach (var item in banner.HardPityRequirements)
            {
                _hardPity.Add(item.Key, 0);
            }
            return _hardPity;
        }
        public void IncreaseHardPity()
        {
            Dictionary<ItemQuality, int> modified = new();
            foreach (var item in _hardPity)
            {
                modified.Add(item.Key, _hardPity[item.Key] + 1);
            }
            _hardPity = modified;
        }
        public void ResetHardPity(ItemQuality droppedQuality)
        {
            if (!_hardPity.ContainsKey(droppedQuality))
                return;
            for (int i = 0; i <= (int)droppedQuality; i++)
            {
                if (_hardPity.ContainsKey((ItemQuality)i))
                {
                    _hardPity[(ItemQuality)i] = 0;
                }
            }
        }
    }
}

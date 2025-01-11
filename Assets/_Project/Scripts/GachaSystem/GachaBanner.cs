using AYellowpaper.SerializedCollections;
using Newtonsoft.Json;
using System;
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

        [SerializeField] private string _ID;

        public ItemPrice Key => _key;
        [SerializeField] ItemPrice _key;

        [Header("Pity")]

        public ItemQuality SoftPityResetMinimalQuality => _softPityResetMinimalQuality;
        [SerializeField] private ItemQuality _softPityResetMinimalQuality = ItemQuality.S;

        public float SoftPityPerPull => _softPityPerPull;
        [SerializeField] private float _softPityPerPull = 0.1f;

        public List<HardPity> HardPityRequirements => _hardPityRequirements;
        [SerializeField] List<HardPity> _hardPityRequirements = new();

        [Header("Drops")]

        public LootTable<ItemQuality> TierChances => _tierChances;
        [SerializeField] LootTable<ItemQuality> _tierChances;

        public Dictionary<ItemQuality, LootTable<ItemDrop>> BannerDrops => _bannerDrops;
        [SerializedDictionary("Tier", "LootTable")]
        [SerializeField] SerializedDictionary<ItemQuality, LootTable<ItemDrop>> _bannerDrops = new();

        public GachaDrop Pull(BannerPullData data)
        {
            data.Pulls++;

            if(data.CurrentHardPity == null || data.CurrentHardPity.Count == 0)
            {
                data.CreateHardPity(this);
            }

            data.IncreaseHardPity();

            ItemQuality dropTier = _tierChances.GetDrop(1, data.SoftPity);

            ItemQuality minimumQuality = ItemQuality.B;

            foreach (var requirement in _hardPityRequirements)
            {
                int currentAmount = HardPity.FindHardPityByQuality(data.CurrentHardPity, requirement.Quality).AmountOfPulls;
                if (requirement.AmountOfPulls <= currentAmount)
                {
                    minimumQuality = requirement.Quality;
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
            foreach (var item in data.CurrentHardPity)
            {
                Debug.Log($"[{item.Quality}] Hard pity: {item.AmountOfPulls}");
            }
            return gachaDrop;
        }

        [ContextMenu("Generate ID")]
        private void GenerateID()
        {
            _ID = Guid.NewGuid().ToString();
        }
        private void Awake()
        {
            GenerateID();
        }
    }
    [Serializable]
    public class BannerSaveData
    {
        public readonly string BannerID;
        public readonly BannerPullData PullData;

        public BannerSaveData(string bannerID, BannerPullData pullData)
        {
            BannerID = bannerID;
            PullData = pullData;
        }
    }
    [Serializable]
    public class HardPity
    {
        public ItemQuality Quality;

        public int AmountOfPulls;

        public HardPity()
        {

        }
        public HardPity(ItemQuality quality, int amountOfPulls)
        {
            Quality = quality;
            AmountOfPulls = amountOfPulls;
        }

        public static HardPity FindHardPityByQuality(List<HardPity> list, ItemQuality droppedQuality)
        {
            return list.Find((hardPity) => { return hardPity.Quality == droppedQuality; });
        }
        public static bool HasHardPity(List<HardPity> list, ItemQuality droppedQuality)
        {
            return FindHardPityByQuality(list, droppedQuality) != null;
        }
    }
    [Serializable]
    public class BannerPullData
    {
        public int Pulls = 0;
        public float SoftPity = 0.0f;

        public List<HardPity> CurrentHardPity => _currentHardPity;

        [SerializeField] List<HardPity> _currentHardPity = new();
        public List<HardPity> CreateHardPity(GachaBanner banner)
        {
            _currentHardPity = new();
            foreach (var item in banner.HardPityRequirements)
            {
                _currentHardPity.Add(new(item.Quality, 0));
            }
            return _currentHardPity;
        }
        public void IncreaseHardPity()
        {
            foreach (var item in _currentHardPity)
            {
                item.AmountOfPulls++;
            }
        }
        public void ResetHardPity(ItemQuality droppedQuality)
        {
            if (!HardPity.HasHardPity(_currentHardPity, droppedQuality))
                return;
            for (int i = 0; i <= (int)droppedQuality; i++)
            {
                if (HardPity.HasHardPity(_currentHardPity, (ItemQuality)i))
                {
                    HardPity.FindHardPityByQuality(_currentHardPity, (ItemQuality)i).AmountOfPulls = 0;
                }
            }
        }
    }
}

using R3;
using System;
using UnityEngine;

namespace Selivura.DemoClicker
{
    public enum ItemQuality
    {
        B,
        A,
        S,
        SS,
        SSS,
    }
    public class Item : MonoBehaviour
    {
        [Header("Basic info")]

        public Sprite Icon => _icon;
        [SerializeField] private Sprite _icon;
        public Sprite SplashArt => _splashArt;
        [SerializeField] private Sprite _splashArt;
        public string Name => _name; 
        [SerializeField] private string _name;

        public string Description => _description;
        
        [SerializeField] [TextArea] private string _description;
        public string ID => _ID;

        [Tooltip("DO NOT CHANGE IT OR SAVE FILES WONT FIND THIS ITEM")]
        [SerializeField] private string _ID;

        public ItemQuality Quality => _quality;
        [SerializeField] private ItemQuality _quality;

        public int Stack => stack;
        protected int stack = 0;

        public Subject<Item> OnStateChanged = new();

        private void Start()
        {
            OnInitialize();
        }
        public void SetIcon(Sprite icon)
        {
            _icon = icon;
            OnStateChanged.OnNext(this);
        }
        protected virtual void OnInitialize()
        {

        }
        public void ChangeStack(int amount)
        {
            stack += amount;
            OnStackChanged(amount);
            OnStateChanged.OnNext(this);
        }
        protected virtual void OnStackChanged(int amount) { }

        public static string QualityToString(ItemQuality quality)
        {
            switch (quality)
            {
                case ItemQuality.B:
                    return "B";
                case ItemQuality.A:
                    return "A";
                case ItemQuality.S:
                    return "S";
                case ItemQuality.SS:
                    return "SS";
                case ItemQuality.SSS:
                    return "SSS";
                default:
                    return "?";
            }
        }
        [ContextMenu("Genreate ID")]
        private void GenerateId()
        {
            _ID = Guid.NewGuid().ToString();
        }
        private void Reset()
        {
            GenerateId();
        }
    }
}

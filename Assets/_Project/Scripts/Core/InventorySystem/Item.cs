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
        public string Name => _name; 
        [SerializeField] private string _name;

        public string Description => _description;
        
        [SerializeField] [TextArea] private string _description;
        public string ID => _ID;

        [SerializeField][TextArea] private string _ID = "item_id";

        public ItemQuality Quality => _quality;
        [SerializeField] private ItemQuality _quality;

        public int Stack => _stack;
        protected int _stack = 0;

        private void Start()
        {
            OnInitialize();
        }
        protected virtual void OnInitialize()
        {

        }
        public void IncreaseStack(int amount)
        {
            _stack += amount;
        }
        protected virtual void OnStackIncreased(int amount) { }

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
    }
}

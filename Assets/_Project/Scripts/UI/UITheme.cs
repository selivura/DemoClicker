using UnityEngine;

namespace Selivura.DemoClicker.UI
{
    [CreateAssetMenu(fileName = "UITheme", menuName = "UI/UITheme")]
    public class UITheme : ScriptableObject
    {
        [SerializeField] private Sprite _placeholderIcon;
        [SerializeField] private Sprite _placeholderSprite;
        [Header("Item Quality")]
        [SerializeField] private UIColorPalette _qualityPalette;
        [SerializeField] private Sprite[] _qualityIcons;

        public Sprite PlaceholderSprite => _placeholderSprite;
        public Sprite PlaceholderIcon => _placeholderIcon;
        public UIColorPalette QualityPalette => _qualityPalette; 
        public Sprite[] QualityIcons => _qualityIcons;
    }
}

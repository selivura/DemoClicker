using UnityEngine;

namespace Selivura.DemoClicker
{
    [CreateAssetMenu(menuName = "UI/Color palette")]
    public class UIColorPalette : ScriptableObject
    {
        public Color[] Colors => _colors;
        [SerializeField] Color[] _colors;
    }
}

using TMPro;
using UnityEngine;

namespace Selivura.DemoClicker.UI
{
    public class ItemFrame : Frame
    {
        [SerializeField] private TMP_Text _text;

        public TMP_Text Text => _text;
    }
}

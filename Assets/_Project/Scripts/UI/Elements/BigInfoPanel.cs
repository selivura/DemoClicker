using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Selivura.DemoClicker.UI
{
    public class BigInfoPanel : MonoBehaviour
    {
        [SerializeField] private Image _frame;
        [SerializeField] private Image _icon;
        [SerializeField] private Image _quality;
        [SerializeField] private TMP_Text _nameText;
        [SerializeField] private TMP_Text _amountText;
        [SerializeField] private TMP_Text _descText;

        public Image Frame  => _frame; 
        public Image Icon => _icon; 
        public Image Quality => _quality;
        public TMP_Text NameText => _nameText;
        public TMP_Text AmountText => _amountText;
        public TMP_Text DescText => _descText;
    }
}

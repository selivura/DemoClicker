using System.Collections.Generic;
using UnityEngine;

namespace Selivura.DemoClicker.UI
{
    public class BannerDropResultsWindow : Window
    {
        [SerializeField] ItemFrame[] _itemFrames;
        private List<GachaDrop> _gachaDrop;

        public void SetDrop(List<GachaDrop> dropResult)
        {
            _gachaDrop = dropResult;
            for (int i = 0; i < _itemFrames.Length; i++)
            {
                var frame = _itemFrames[i];
                if (i < _gachaDrop.Count)
                {
                    frame.gameObject.SetActive(true);
                    frame.IconImage.sprite = _gachaDrop[i].Item.Icon;
                    frame.FrameImage.color = uiTheme.QualityPalette.Colors[(int)_gachaDrop[i].DropQuality];
                    frame.Text.text = "x" + _gachaDrop[i].Amount;
                }
                else
                {
                    frame.gameObject.SetActive(false);
                }
            }
        }
    }
}

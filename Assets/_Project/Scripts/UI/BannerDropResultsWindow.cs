using DG.Tweening;
using Selivura.DemoClicker.UI;
using System.Collections.Generic;
using UnityEngine;

namespace Selivura.DemoClicker
{
    public class BannerDropResultsWindow : MonoBehaviour
    {
        [SerializeField] ItemFrame[] _itemFrames;
        [SerializeField] UITheme _uiTheme;

        public void ShowWindow(List<GachaDrop> dropResult)
        {
            gameObject.SetActive(true);
            transform.DOScale(1, 0.25f).OnComplete(() => { });
            for (int i = 0; i < _itemFrames.Length; i++)
            {
                var frame = _itemFrames[i];
                if (i < dropResult.Count)
                {
                    frame.gameObject.SetActive(true);
                    frame.IconImage.sprite = dropResult[i].Item.Icon;
                    frame.FrameImage.color = _uiTheme.QualityPalette.Colors[(int)dropResult[i].DropQuality];
                    frame.Text.text = "x" + dropResult[i].Amount;
                }
                else
                {
                    frame.gameObject.SetActive(false);
                }
            }
        }
        public void HideWindow()
        {
            transform.DOScale(0, 0.25f).OnComplete(() => gameObject.SetActive(false));
        }
    }
}

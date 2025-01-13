using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Selivura.DemoClicker.UI
{
    public class WindowAnimations : MonoBehaviour
    {
        [SerializeField] Image _backgroundImage;
        [SerializeField] Transform _windowContainer;

        public void DoShowAnim()
        {
            gameObject.SetActive(true);
            _backgroundImage.DOFade(1, 0.25f).OnComplete(() => { });
            _windowContainer.DOScale(1, 0.25f);
        }
        public void DoHideAnim()
        {
            _backgroundImage.DOFade(0, 0.25f).OnComplete(() => gameObject.SetActive(false));
            _windowContainer.DOScale(0, 0.25f);
        }
        private void OnDestroy()
        {
            _backgroundImage.DOKill();
            _windowContainer.DOKill();
        }
    }
}

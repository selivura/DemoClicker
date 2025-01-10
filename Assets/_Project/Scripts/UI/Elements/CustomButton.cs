using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using DG.Tweening;

namespace Selivura.DemoClicker.UI
{
    [RequireComponent(typeof(Button))]
    public class CustomButton : MonoBehaviour
    {
        [Header("Tweening")]
        [SerializeField] private float _punchScale = 0.25f;
        [SerializeField] private float _punchDuration = 0.1f;
        [SerializeField] private int _punchVibrato = 5;
        [SerializeField] private float _punchElasticity = 1;
        public Button ButtonComponent { get; private set; }
        public UnityEvent OnButtonClick;
        private void Awake()
        {
            ButtonComponent = GetComponent<Button>();   
            ButtonComponent.onClick.AddListener(Click);
        }
        private void Click()
        {
            transform.DOKill(true);
            transform.DOPunchScale(transform.localScale * _punchScale, _punchDuration, _punchVibrato, _punchElasticity);
            OnClick();
            OnButtonClick?.Invoke();
        }
        protected virtual void OnClick()
        {

        }
    }
}

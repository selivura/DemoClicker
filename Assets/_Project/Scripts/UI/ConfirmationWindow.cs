using R3;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Selivura.DemoClicker.UI
{
    public class ConfirmationWindow : Window
    {
        [SerializeField] private string _defaultMessage = "Are you sure?";
        [SerializeField] private string _defaultConfirm = "Hell yeah";
        [SerializeField] private string _defaultCancel = "Hell nah";

        [SerializeField] private Button _confirmButton;
        [SerializeField] private Button _cancelButton;
        [SerializeField] private TMP_Text _windowText;
        [SerializeField] private TMP_Text _confirmText;
        [SerializeField] private TMP_Text _cancelText;
        public Subject<Unit> OnConfirm { get; private set; } = new();
        public Subject<Unit> OnCancel { get; private set; } = new();

        private void Awake()
        {
            _confirmButton.onClick.AddListener(Confirm);
            _cancelButton.onClick.AddListener(Cancel);
        }
        public void SetDefaultText()
        {
            SetTextMessage(_defaultMessage);
            SetTextOptionCancel(_defaultCancel);
            SetTextOptionConfirm(_defaultConfirm);
        }
        public void SetTextMessage(string text)
        {
            _windowText.text = text;
        }
        public void SetTextOptionCancel(string text)
        {
            _cancelText.text = text;
        }
        public void SetTextOptionConfirm(string text)
        {
            _confirmText.text = text;
        }

        private void Confirm()
        {
            OnConfirm.OnNext(Unit.Default);
            OnConfirm = new();
        }

        private void Cancel()
        {
            OnCancel.OnNext(Unit.Default);
            OnCancel = new();
        }
    }
}

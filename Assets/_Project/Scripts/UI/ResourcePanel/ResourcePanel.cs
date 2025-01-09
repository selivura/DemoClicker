using R3;
using System;
using UnityEngine;
using Zenject;

namespace Selivura.DemoClicker.UI
{
    public class ResourcePanel : MonoBehaviour
    {
        [Inject] private InventoryService _inventoryService;

        [SerializeField] private Item _itemToSearch;
        [SerializeField] TextWithIcon _textWithIcon;

        private Item _displayItem;
        private CompositeDisposable _disposable = new();
        private IDisposable _displayItemSubscription;
        private void Awake()
        {
            SetItemToSearch(_itemToSearch);
            _inventoryService.OnItemAdded.Subscribe(CheckForItem).AddTo(_disposable);
            _inventoryService.OnItemRemoved.Subscribe(OnItemRemoved).AddTo(_disposable);
        }
        public void SetItemToSearch(Item item)
        {
            _itemToSearch = item;
            var newItem = _inventoryService.FindItemByID(item.ID);

            if (newItem != null)
            {
                SetDisplayItem(newItem);
            }
            else
            {
                SetTextWithIconToSearchItem();
            }
        }
        private void SetTextWithIconToSearchItem()
        {
            _textWithIcon.Image.sprite = _itemToSearch.Icon;
            _textWithIcon.Text.text = 0.ToString();
        }
        private void OnItemRemoved(Item item)
        {
            if (item.ID != _itemToSearch.ID)
                return;
            _displayItemSubscription?.Dispose();
            SetTextWithIconToSearchItem();
        }
        private void SetDisplayItem(Item item)
        {
            _displayItemSubscription?.Dispose();
            _displayItem = item;
            _displayItemSubscription = _displayItem.OnStateChanged.Subscribe(UpdateTextWithIcon);
            UpdateTextWithIcon(_displayItem);
        }
        private void CheckForItem(Item item)
        {
            if (item.ID == _itemToSearch.ID)
            {
                SetDisplayItem(item);
            }
        }
        private void UpdateTextWithIcon(Item item)
        {
            _textWithIcon.Image.sprite = item.Icon;
            _textWithIcon.Text.text = item.Stack.ToString();
        }
        private void OnDestroy()
        {
            _disposable.Dispose();
            _displayItemSubscription?.Dispose();
        }

    }
}

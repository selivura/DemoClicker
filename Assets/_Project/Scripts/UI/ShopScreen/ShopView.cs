using R3;
using Selivura.DemoClicker.UI;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Selivura.DemoClicker
{
    public class ShopView : MonoBehaviour
    {
        [SerializeField] ShopViewModel _viewModel;

        [SerializeField] BigInfoPanel _bigInfoPanel;
        [SerializeField] TextWithIconButton _itemCostButton;
        [SerializeField] ShopLotFrame _shopLotViewPrefab;

        [SerializeField] private Transform _container;

        [SerializeField] private Item _nothingSelectedItem;

        [Tooltip("Pallete that will be used to set color of quality icon. Must have colors for all qualities.")]
        [SerializeField] protected UIColorPalette colorPalette;

        [Tooltip("Sprites that will be used to set sprite of quality icon. Must have sprites for all qualities.")]
        [SerializeField] protected List<Sprite> qualitySprites = new();

        private CompositeDisposable _disposable = new();
        private void Start()
        {
            _itemCostButton.OnButtonClick.AddListener(_viewModel.BuyCurrentLot);
            _viewModel.OnItemsChanged.Subscribe(OnItemsChanged).AddTo(_disposable);
            _viewModel.OnSelectedLotChanged.Subscribe(OnLotSelected).AddTo(_disposable);
            ClearItemPanel();
        }
        private void OnItemsChanged(List<ShopLot> itemLots)
        {
            for (int i = 0; i < _container.childCount; i++)
            {
                Destroy(_container.GetChild(i).gameObject);
            }
            foreach (var lot in itemLots)
            {
                var spawnedSlot = Instantiate(_shopLotViewPrefab, _container);

                spawnedSlot.TextWithIcon.Text.text = "x" + lot.Price.Price;
                spawnedSlot.TextWithIcon.Image.sprite = lot.Price.Item.Icon;
                spawnedSlot.IconImage.sprite = lot.ItemForSale.Icon;

                spawnedSlot.Button.OnButtonClick.AddListener(() => _viewModel.SelectLot(lot));
            }
            ClearItemPanel();
        }
        private void UpdateItemPanel(ShopLot lot)
        {
            Item item = lot.ItemForSale;
            _bigInfoPanel.NameText.text = item.Name;
            _bigInfoPanel.DescText.text = item.Description;
            _bigInfoPanel.Icon.sprite = item.Icon;

            _bigInfoPanel.Quality.sprite = qualitySprites[(int)item.Quality];
            _bigInfoPanel.Quality.color = colorPalette.Colors[(int)item.Quality];

            _bigInfoPanel.AmountText.text = "x" + lot.AmountForSale;
            _itemCostButton.ButtonComponent.interactable = true;
        }
        private void ClearItemPanel()
        {
            _bigInfoPanel.NameText.text = "???";
            _bigInfoPanel.DescText.text = "???";

            _bigInfoPanel.AmountText.text = "x???";

            _itemCostButton.TextWithIcon.Text.text = "x???";
            _itemCostButton.ButtonComponent.interactable = false;
        }
        private void OnLotSelected(ShopLot lot)
        {
            _itemCostButton.TextWithIcon.Image.sprite = lot.Price.Item.Icon;
            _itemCostButton.TextWithIcon.Text.text = "x" + lot.Price.Price;
            UpdateItemPanel(lot);
        }
        private void OnDestroy()
        {
            _disposable.Dispose();
            _itemCostButton.OnButtonClick.RemoveListener(_viewModel.BuyCurrentLot);
        }
    }
}

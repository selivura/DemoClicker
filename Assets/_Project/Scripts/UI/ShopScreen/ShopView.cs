using R3;
using Selivura.DemoClicker.UI;
using System.Collections.Generic;
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


        private CompositeDisposable _disposable = new();
        private void Awake()
        {
            _itemCostButton.OnButtonClick.AddListener(_viewModel.BuyCurrentLot);
            _viewModel.OnItemsChanged.Subscribe(OnItemsChanged).AddTo(_disposable);
            _viewModel.OnSelectedLotChanged.Subscribe(OnLotSelected).AddTo(_disposable);
        }
        private void Start()
        {
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
                spawnedSlot.TextWithIcon.IconImage.sprite = lot.Price.Item.Icon;
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
            _bigInfoPanel.SplashArt.sprite = item.SplashArt;

            _bigInfoPanel.SetQuality(item.Quality);

            _bigInfoPanel.AmountText.text = "x" + lot.AmountForSale;
        }
        private void ClearItemPanel()
        {
            _bigInfoPanel.ClearPanel();

            _itemCostButton.TextWithIcon.Clear();
        }
        private void OnLotSelected(ShopLot lot)
        {
            _itemCostButton.TextWithIcon.IconImage.sprite = lot.Price.Item.Icon;
            _itemCostButton.TextWithIcon.Text.text = "x" + lot.Price.Price;
            _itemCostButton.ButtonComponent.interactable = _viewModel.CanAffordSelectedLot();
            UpdateItemPanel(lot);
        }
        private void OnDestroy()
        {
            _disposable.Dispose();
            _itemCostButton.OnButtonClick.RemoveListener(_viewModel.BuyCurrentLot);
        }
    }
}

using R3;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Selivura.DemoClicker
{
    public class ShopViewModel : MonoBehaviour
    {
        [Inject] ShopService _shopService;

        private ShopLot _selectedLot;
        public Subject<List<ShopLot>> OnItemsChanged = new();
        public Subject<ShopLot> OnSelectedLotChanged = new();

        private CompositeDisposable _disposable = new();
        private void Start()
        {
            _shopService.OnItemsChanged.Subscribe((itemLots) => OnItemsChanged.OnNext(itemLots)).AddTo(_disposable);
            //_itemCostButton.SetItemAndCost(new ItemPrice(_nothingSelectedItem, 1));
        }
        public void SelectLot(ShopLot lot)
        {
            _selectedLot = lot;
            OnSelectedLotChanged.OnNext(_selectedLot);
        }
        private void OnDestroy()
        {
            _disposable.Dispose();
        }

        public void BuyCurrentLot()
        {
            _shopService.Buy(_selectedLot);
        }
    }
}

using R3;
using Selivura.DemoClicker.UI;
using System.Collections.Generic;
using UnityEngine;

namespace Selivura.DemoClicker
{
    public class InventoryScreenView : InventoryView
    {
        [SerializeField] private ItemFrame _itemFramePrefab;
        [SerializeField] private BigInfoPanel _bigInfoPanel;
        [SerializeField] private Transform _container;
        private List<ItemFrame> _spawned = new();

        private CompositeDisposable _spawnedDisposable = new();

        private void Start()
        {
            ClearItemPanel();
        }
        protected override void OnInventoryUpdated(List<Item> items)
        {
            foreach (var spawnedVM in _spawned)
            {
                Destroy(spawnedVM.gameObject);
            }
            _spawned.Clear();
            foreach (var item in items)
            {
                var spawnedSlot = Instantiate(_itemFramePrefab, _container);

                spawnedSlot.Text.text = "x" + item.Stack;
                spawnedSlot.IconImage.sprite = item.Icon;

                spawnedSlot.Button.OnButtonClick.AsObservable().Subscribe(_ => UpdateItemPanel(item)).AddTo(_spawnedDisposable);
                _spawned.Add(spawnedSlot);
            }
        }
        private void UpdateItemPanel(Item item)
        {
            _bigInfoPanel.NameText.text = item.Name;
            _bigInfoPanel.DescText.text = item.Description;
            _bigInfoPanel.Icon.sprite = item.Icon;
            _bigInfoPanel.SplashArt.sprite = item.SplashArt;

            _bigInfoPanel.SetQuality(item.Quality);

            _bigInfoPanel.AmountText.text = "x" + item.Stack;
        }
        private void ClearItemPanel()
        {
            _bigInfoPanel.ClearPanel();
        }
        private void OnDestroy()
        {
            _spawnedDisposable.Dispose();
        }
    }
}

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

        [Tooltip("Pallete that will be used to set color of quality icon. Must have colors for all qualities.")]
        [SerializeField] protected UIColorPalette colorPalette;

        [Tooltip("Sprites that will be used to set sprite of quality icon. Must have sprites for all qualities.")]
        [SerializeField] protected List<Sprite> qualitySprites = new();
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

            _bigInfoPanel.Quality.sprite = qualitySprites[(int)item.Quality];
            _bigInfoPanel.Quality.color = colorPalette.Colors[(int)item.Quality];

            _bigInfoPanel.AmountText.text = "x" + item.Stack;
        }
        private void ClearItemPanel()
        {
            _bigInfoPanel.NameText.text = "???";
            _bigInfoPanel.DescText.text = "???";

            _bigInfoPanel.AmountText.text = "x???";
        }
        private void OnDestroy()
        {
            _spawnedDisposable.Dispose();
        }
    }
}

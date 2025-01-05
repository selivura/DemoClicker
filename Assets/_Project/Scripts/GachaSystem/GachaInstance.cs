using UnityEngine;
using Zenject;

namespace Selivura.DemoClicker
{
    public class GachaInstance : MonoBehaviour
    {
        [SerializeField] BannerRollData _currentRollData;
        [SerializeField] GachaBanner _banner;

        [Inject] InventoryService _inventoryService;
        public void Pull()
        {
            GachaDrop drop = _banner.Pull(_currentRollData);
            _inventoryService.GiveItem(drop);
        }
    }
}

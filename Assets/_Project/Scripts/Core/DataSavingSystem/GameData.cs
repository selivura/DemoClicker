
using System.Collections.Generic;

namespace Selivura.DemoClicker.Persistence
{
    [System.Serializable]
    public class GameData
    {
        public string Name;
        public SavedInventory Inventory;
        public List<BannerSaveData> BannerSaves;
    }
}

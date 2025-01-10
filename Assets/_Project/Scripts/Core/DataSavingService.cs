using UnityEngine;
using Zenject;

namespace Selivura.DemoClicker.Persistence
{
    public class DataSavingService : MonoBehaviour
    {
        [SerializeField] private string _saveName = "Save";
        [SerializeField] public GameData GameData;

        [Inject] InventoryService _inventoryService;
        [Inject] GachaService _gachaService;

        IDataService _dataService;

        private void Awake()
        {
            _dataService = new FileDataService(new JsonSerializer());
            LoadGame(_saveName);
        }

        public void NewGame()
        {
            GameData = new GameData { Name = "Save"};
            Debug.Log("New game created");
        }

        public void SaveGame()
        {
            GameData.Inventory = _inventoryService.CaptureState();
            GameData.BannerSaves = _gachaService.CaptureState();
            _dataService.Save(GameData);
            Debug.Log("Game saved");
        }

        public void LoadGame(string gameName)
        {
            try
            {
                GameData = _dataService.Load(gameName);
                _gachaService.RestoreState(GameData.BannerSaves);
                _inventoryService.RestoreState(GameData.Inventory);
                Debug.Log("Game loaded");
            }
            catch
            {
                Debug.Log("Cant load game");
                NewGame();
            }
        }

        public void DeleteGame(string gameName)
        {
            _dataService.Delete(gameName);
            Debug.Log("Deleted game");
            if(GameData.Name == gameName)
            {
                NewGame();
            }
        }
        //REMOVE THIS
        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.S))
            {
                SaveGame();
            }
            if (Input.GetKeyDown(KeyCode.N))
            {
                NewGame();
            }
            if(Input.GetKeyDown(KeyCode.D))
            {
                DeleteGame(_saveName);
            }
            if(Input.GetKeyDown(KeyCode.L))
            {
                LoadGame(_saveName);
            }
        }

        private void OnApplicationQuit()
        {
            SaveGame();
        }
    }

    public interface ISaveable<T>
    {
        T CaptureState();

        void RestoreState(T state);

    }
}

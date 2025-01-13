using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Selivura.DemoClicker.Persistence
{
    public class DataSavingService : MonoBehaviour
    {
        [SerializeField] private string _saveName = "Save";
        [SerializeField] public GameData GameData = new();

        [Inject] InventoryService _inventoryService;
        [Inject] GachaService _gachaService;

        IDataService _dataService;

        private void Awake()
        {
            _dataService = new FileDataService(new JsonSerializer());
            LoadGame();
        }

        public void NewGame()
        {
            Debug.Log("New game created");
        }

        public void SaveGame()
        {
            GameData.Inventory = _inventoryService.CaptureState();
            GameData.BannerSaves = _gachaService.CaptureState();
            _dataService.Save(GameData);
            Debug.Log("Game saved");
        }

        public void LoadGame()
        {
            try
            {
                GameData = _dataService.Load(_saveName);
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

        public void DeleteGame()
        {
            _dataService.Delete(_saveName);
            Debug.Log("Deleted game");
            SceneManager.LoadScene(0, LoadSceneMode.Single);
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
                DeleteGame();
            }
            if(Input.GetKeyDown(KeyCode.L))
            {
                LoadGame();
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

using System.Collections.Generic;
using System.IO;
using UnityEngine;
namespace Selivura.DemoClicker.Persistence
{
    public class FileDataService : IDataService
    {
        private ISerializer _serializer;
        private string _dataPath;
        private string _fileExtension;

        public FileDataService(ISerializer serializer)
        {
            _serializer = serializer;
            _dataPath = Application.persistentDataPath;
            _fileExtension = "json";
        }

        
        public void Save(GameData data, bool overwrite = true)
        {
            string path = GetPathToFile(data.Name);

            if (!overwrite && File.Exists(path))
            {
                throw new IOException($"File {data.Name}.{_fileExtension} already exists and cannot be overwriten.");
            }

            File.WriteAllText(path, _serializer.Serialize(data));
        }

        public GameData Load(string name)
        {
            string path = GetPathToFile(name);

            if (!File.Exists(path))
            {
                throw new IOException($"File {name}.{_fileExtension} does not exists and cannot be loaded.");
            }

            return _serializer.Deserialize<GameData>(File.ReadAllText(path));
        }
        public void Delete(string name)
        {
            string path = GetPathToFile(name);

            if (File.Exists(path))
            {
               File.Delete(path);
            }
        }

        //public void DeleteAll()
        //{
        //    foreach (string filePath in Directory.GetFiles(_dataPath))
        //    {
        //        File.Delete(filePath);
        //    }
        //}

        public IEnumerable<string> ListSaves()
        {
            foreach (string path in Directory.GetFiles(_dataPath))
            {
                if(Path.GetExtension(path) == _fileExtension)
                {
                    yield return Path.GetFileNameWithoutExtension(path);
                }
            }
        }

        private string GetPathToFile(string fileName)
        {
            return Path.Combine(_dataPath, $"{fileName}.{_fileExtension}");
        }
    }
}

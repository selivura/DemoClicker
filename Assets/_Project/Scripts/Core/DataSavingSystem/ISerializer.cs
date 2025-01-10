using System.Collections.Generic;

namespace Selivura.DemoClicker.Persistence
{
    public interface ISerializer
    {
        string Serialize<T>(T obj);

        T Deserialize<T>(string json);
    }
    public interface IDataService
    {
        void Save(GameData data, bool overwrite = true);
        GameData Load(string name);
        void Delete(string name);
        IEnumerable<string> ListSaves();
    }
}

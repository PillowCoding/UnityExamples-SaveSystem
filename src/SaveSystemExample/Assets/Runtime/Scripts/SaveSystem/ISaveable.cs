using Newtonsoft.Json.Linq;

#nullable enable

namespace PillowCoding.SaveManager
{
    public interface ISaveable
    {
        string Key { get; }
        object ToSaveable();
        void FromSaveable(JObject @object);
    }
}

using Newtonsoft.Json.Linq;

#nullable enable

namespace PillowCoding.SaveManager
{
    internal class Saveable
    {
        public Saveable(
            string key,
            object data)
        {
            this.Key = key;
            this.Data = data;
        }

        /// <summary>
        /// The unique key of the saveable.
        /// </summary>
        public string Key { get; }

        /// <summary>
        /// The data.
        /// </summary>
        public object Data { get; }
    }
}

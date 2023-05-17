using Newtonsoft.Json.Linq;

#nullable enable

namespace PillowCoding.SaveManager
{
    public interface ISaveable
    {
        /// <summary>
        /// The unique key of the saveable.
        /// </summary>
        string Key { get; }

        /// <summary>
        /// Called to retrieve the data to save.
        /// </summary>
        /// <returns>An object of the data to save.</returns>
        object ToSaveable();

        /// <summary>
        /// Called when saved data can be loaded.
        /// </summary>
        /// <param name="object">The object to load. If null, there was no data to load.</param>
        void FromSaveable(JContainer? @object);
    }
}

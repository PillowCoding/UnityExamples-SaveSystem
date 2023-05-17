using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

using Object = UnityEngine.Object;

#nullable enable

namespace PillowCoding.SaveManager
{
    public class SaveManager : MonoBehaviour
    {
        private static SaveManager? _instance;
        public static SaveManager Instance => SaveManager.GetOrFetchSingleton();

        private List<ISaveable> _saveables = new List<ISaveable>();
        private string? _saveFilePath;

        private void Awake()
        {
            this._saveFilePath = Path.Combine(Application.persistentDataPath, "save.json");
        }

        private void Start()
        {
            Object.DontDestroyOnLoad(this);
        }

        public void Register<TSaveable>(TSaveable saveable)
            where TSaveable : MonoBehaviour, ISaveable
        {
            if (this.HasRegistered(saveable))
            {
                Debug.LogWarning($"The saveable {saveable.Key} ({saveable.name}) was already registered.", saveable);
                return;
            }

            this._saveables.Add(saveable);
        }

        public void UnRegister<TSaveable>(TSaveable saveable)
            where TSaveable : MonoBehaviour, ISaveable
        {
            if (!this.HasRegistered(saveable))
            {
                Debug.LogWarning($"The saveable {saveable.Key} ({saveable.name}) is not registered.", saveable);
                return;
            }

            if (!this._saveables.Remove(saveable))
            {
                Debug.LogError($"The saveable {saveable.Key} ({saveable.name}) could not be removed.", saveable);
            }
        }

        public bool HasRegistered<TSaveable>(TSaveable saveable)
            where TSaveable : MonoBehaviour, ISaveable
            => this._saveables.Any(x => x.Key.ToLower() == saveable.Key.ToLower());

        public void StartSave()
        {
            Debug.Log("Start save...");

            // Collect saveable data.
            var dataCollection = this._saveables.Select(x => new Saveable(x.Key, x.ToSaveable()));
            var json = JsonConvert.SerializeObject(dataCollection, Formatting.Indented);

            // You should make this async, but for the sake of showing an example this has been done synchronous.
            File.WriteAllText(this._saveFilePath, json);

            Debug.Log("Finished save.");
        }

        public void StartLoad()
        {
            Debug.Log("Start load...");

            if (!File.Exists(this._saveFilePath)) {
                Debug.LogWarning("There is no save to load.");
                return;
            }

            // You should make this async, but for the sake of showing an example this has been done synchronous.
            var json = File.ReadAllText(this._saveFilePath);
            var dataCollection = JsonConvert.DeserializeObject<Saveable[]>(json);

            // Return data to saveables.
            foreach (var saveable in this._saveables) {
                var dataObject = dataCollection.SingleOrDefault(x => x.Key.ToLower() == saveable.Key.ToLower());
                saveable.FromSaveable((JContainer?)dataObject?.Data);
            }

            Debug.Log("Finished load.");
        }

        private static SaveManager GetOrFetchSingleton()
        {
            if (SaveManager._instance != null)
            {
                return SaveManager._instance;
            }

            var instances = Object.FindObjectsOfType<SaveManager>();
            if (instances.Length > 1)
            {
                Debug.LogWarning($"Found more than one instance of {nameof(SaveManager)}. Using first.");
            }
            if (instances.Length == 0)
            {
                throw new Exception($"Required instance of {nameof(SaveManager)} was not found.");
            }

            SaveManager._instance = instances.First();
            return SaveManager._instance;
        }
    }
}

using System;
using System.Collections.Generic;
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

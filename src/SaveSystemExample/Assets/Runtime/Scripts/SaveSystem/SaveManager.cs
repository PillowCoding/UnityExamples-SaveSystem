using System;
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

        private void Start()
        {
            Object.DontDestroyOnLoad(this);
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

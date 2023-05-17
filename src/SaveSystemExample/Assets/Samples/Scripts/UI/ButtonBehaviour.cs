using PillowCoding.SaveManager;
using UnityEngine;

#nullable enable

public class ButtonBehaviour : MonoBehaviour
{
    private SaveManager SaveManager => SaveManager.Instance;
    [SerializeField] private PositionableObjectManager _positionableObjectManager;

    private void OnGUI()
    {
        GUILayout.BeginArea(new Rect(10, 60, 300, 300));
        GUILayout.Label("Control the scene with the buttons below.");

        if (GUILayout.Button("Spawn an object at a random position"))
        {
            this._positionableObjectManager.Spawn();
        }

        if (GUILayout.Button("Save the current scene"))
        {
            this.SaveManager.StartSave();
        }

        if (GUILayout.Button("Load the saved data into the current scene"))
        {
            this.SaveManager.StartLoad();
        }

        GUILayout.EndArea();
    }
}

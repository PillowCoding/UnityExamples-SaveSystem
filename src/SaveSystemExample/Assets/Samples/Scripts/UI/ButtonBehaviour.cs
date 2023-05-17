using PillowCoding.SaveManager;
using System.Diagnostics;
using UnityEngine;

public class ButtonBehaviour : MonoBehaviour
{
    private SaveManager SaveManager => SaveManager.Instance;

    private void OnGUI()
    {
        GUILayout.BeginArea(new Rect(10, 60, 300, 300));
        GUILayout.Label("Control the scene with the buttons below.");

        if (GUILayout.Button("Spawn an object at a random position"))
        {
        }

        if (GUILayout.Button("Save the current scene"))
        {
        }

        if (GUILayout.Button("Load the saved data into the current scene"))
        {
        }

        GUILayout.EndArea();
    }
}

using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.IO;

public class BuildSettingsHelper : MonoBehaviour
{
    [MenuItem("Tools/Sync Scenes to Build Settings")]
    public static void SyncScenes()
    {
        List<EditorBuildSettingsScene> buildScenes = new List<EditorBuildSettingsScene>();
        string[] sceneFiles = Directory.GetFiles("Assets/Scenes", "*.unity", SearchOption.AllDirectories);

        foreach (string sceneFile in sceneFiles)
        {
            buildScenes.Add(new EditorBuildSettingsScene(sceneFile, true));
        }

        EditorBuildSettings.scenes = buildScenes.ToArray();
        Debug.Log("Successfully added " + buildScenes.Count + " scenes to Build Settings.");
    }
}

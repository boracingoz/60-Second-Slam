using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using System.IO;

public class LevelGenerator : EditorWindow
{
    [MenuItem("Tools/Level Generator")]
    public static void ShowWindow()
    {
        GetWindow<LevelGenerator>("Level Generator");
    }

    void OnGUI()
    {
        GUILayout.Label("Level Generator", EditorStyles.boldLabel);

        if (GUILayout.Button("Generate Next Level"))
        {
            GenerateNextLevel();
        }
    }

    void GenerateNextLevel()
    {
        string currentScenePath = EditorSceneManager.GetActiveScene().path;
        int nextLevelNumber = EditorSceneManager.GetActiveScene().buildIndex + 2;
        string newScenePath = Path.Combine(Path.GetDirectoryName(currentScenePath), nextLevelNumber.ToString() + ".unity");

        if (AssetDatabase.CopyAsset(currentScenePath, newScenePath))
        {
            EditorBuildSettingsScene[] scenes = EditorBuildSettings.scenes;
            ArrayUtility.Add(ref scenes, new EditorBuildSettingsScene(newScenePath, true));
            EditorBuildSettings.scenes = scenes;
            EditorSceneManager.OpenScene(newScenePath);
        }
    }
}

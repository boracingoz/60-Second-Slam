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
        // Mevcut aktif sahneyi al
        string currentScenePath = EditorSceneManager.GetActiveScene().path;
        
        // Yeni sahne numarasını bul
        int nextLevelNumber = EditorSceneManager.GetActiveScene().buildIndex + 2;
        
        // Yeni sahne yolu oluştur
        string newScenePath = Path.Combine(Path.GetDirectoryName(currentScenePath), nextLevelNumber.ToString() + ".unity");

        // Mevcut sahneyi yeni yola kopyala
        if (AssetDatabase.CopyAsset(currentScenePath, newScenePath))
        {
            Debug.Log($"Level {nextLevelNumber} created successfully at: {newScenePath}");
            
            // Yeni sahneyi build settings'e ekle
            EditorBuildSettingsScene[] scenes = EditorBuildSettings.scenes;
            ArrayUtility.Add(ref scenes, new EditorBuildSettingsScene(newScenePath, true));
            EditorBuildSettings.scenes = scenes;
            
            // Yeni sahneyi aç
            EditorSceneManager.OpenScene(newScenePath);
        }
        else
        {
            Debug.LogError($"Failed to create Level {nextLevelNumber}");
        }
    }
}

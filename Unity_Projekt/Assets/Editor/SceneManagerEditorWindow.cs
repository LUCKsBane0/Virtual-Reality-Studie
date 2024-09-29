using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerEditorWindow : EditorWindow
{
    private SceneManagerPersistent sceneManager;

    // Add a menu item to open the SceneManagerEditor window
    [MenuItem("Tools/Scene Manager")]
    public static void ShowWindow()
    {
        GetWindow<SceneManagerEditorWindow>("Scene Manager");
    }

    private void OnGUI()
    {
        // Find SceneManagerPersistent in the scene
        sceneManager = FindObjectOfType<SceneManagerPersistent>();

        if (sceneManager == null)
        {
            EditorGUILayout.HelpBox("No SceneManagerPersistent found in the scene.", MessageType.Warning);
            if (GUILayout.Button("Refresh"))
            {
                Repaint();
            }
            return;
        }

        // Display scene pools
        EditorGUILayout.LabelField("Scene Pools", EditorStyles.boldLabel);

        foreach (var pool in sceneManager.pools)
        {
            // Show pool name
            EditorGUILayout.LabelField($"Pool: {pool.poolName}", EditorStyles.boldLabel);

            // Display a list of scenes within the pool
            foreach (var sceneAsset in pool.sceneAssets)
            {
                // Each scene has a button to load it
                if (GUILayout.Button($"Load {sceneAsset.name}"))
                {
                    LoadScene(sceneAsset.name);
                }
            }

            // Add a button to load a random scene from the pool
            if (GUILayout.Button($"Load Random Scene from {pool.poolName}"))
            {
                sceneManager.LoadSceneFromPool(pool.poolName);
            }

            EditorGUILayout.Space();
        }

        if (GUILayout.Button("Refresh"))
        {
            Repaint();
        }
    }

    // Helper function to load a scene by name
    private void LoadScene(string sceneName)
    {
        // Check if we are in Play Mode or Edit Mode
        if (Application.isPlaying)
        {
            // Use SceneManager.LoadScene for Play Mode
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            // Use EditorSceneManager.OpenScene for Edit Mode
            string scenePath = GetScenePath(sceneName);
            if (!string.IsNullOrEmpty(scenePath))
            {
                UnityEditor.SceneManagement.EditorSceneManager.OpenScene(scenePath);
            }
            else
            {
                Debug.LogError($"Scene {sceneName} cannot be found. Make sure it is added to the build settings.");
            }
        }
    }

    // Helper function to get the scene path from the build settings
    private string GetScenePath(string sceneName)
    {
        for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            string scenePath = SceneUtility.GetScenePathByBuildIndex(i);
            string sceneFileName = System.IO.Path.GetFileNameWithoutExtension(scenePath);
            if (sceneFileName == sceneName)
            {
                return scenePath;
            }
        }
        return null;
    }
}

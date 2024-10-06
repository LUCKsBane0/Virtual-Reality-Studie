using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerPersistent : MonoBehaviour
{
    [Header("Scene Pools")]
    public List<ScenePool> pools; // List of scene pools that can be modified in the inspector

    private static SceneManagerPersistent instance;

    // Singleton getter
    public static SceneManagerPersistent getInstance()
    {
        if (instance == null)
        {
            Debug.LogError("No Instance found!");
            return null;
        }
        return instance;
    }

    private void Awake()
    {
        // Ensure this object persists across scenes
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Function to load a scene by its name
    public void LoadScene(string sceneName)
    {
        // Check if the scene exists in the build settings
        if (Application.CanStreamedLevelBeLoaded(sceneName))
        {
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.LogError($"Scene {sceneName} not found in the build settings.");
        }
    }

    // Function to load a random scene from a pool by pool name
    public void LoadSceneFromPool(string poolName)
    {
        // Find the pool by name
        ScenePool selectedPool = pools.Find(pool => pool.poolName == poolName);

        if (selectedPool != null && selectedPool.sceneNames.Count > 0)
        {
            // Pick a random scene from the pool
            string randomScene = selectedPool.GetRandomSceneName();

            // Load the randomly selected scene
            LoadScene(randomScene);
        }
        else
        {
            Debug.LogError($"Pool {poolName} not found or it contains no scenes.");
        }
    }
}

[System.Serializable]
public class ScenePool
{
    public string poolName;                 // Name of the pool
    public List<string> sceneNames;         // List of scene names (strings)

    // Get the scene name from the list of strings
    public string GetRandomSceneName()
    {
        if (sceneNames.Count == 0) return null;

        // Get a random scene name
        return sceneNames[Random.Range(0, sceneNames.Count)];
    }
}

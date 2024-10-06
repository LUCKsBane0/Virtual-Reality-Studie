using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor; // Required for SceneAsset

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

        if (selectedPool != null && selectedPool.sceneAssets.Count > 0)
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
    public List<SceneAsset> sceneAssets;    // List of SceneAsset references

    // Get the scene name from the SceneAsset (used for runtime scene loading)
    public string GetRandomSceneName()
    {
        if (sceneAssets.Count == 0) return null;

        // Get a random SceneAsset and return its scene name
        SceneAsset randomSceneAsset = sceneAssets[Random.Range(0, sceneAssets.Count)];
        return randomSceneAsset.name; // Use the scene name
    }
}

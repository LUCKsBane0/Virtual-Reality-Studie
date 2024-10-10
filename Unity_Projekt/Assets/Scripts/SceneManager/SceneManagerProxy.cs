using UnityEngine;

public class SceneManagerProxy : MonoBehaviour
{
    // Proxy method for loading a scene by name

    public string getCurrentScenario()
    {
        var sceneManager = SceneManagerPersistent.getInstance();
        if (sceneManager != null)
        {
            return sceneManager.currentScenario;
        }
        else
        {
            Debug.LogError("SceneManagerPersistent instance not found.");
            return null;
        }
    }

    public void LoadScene(string sceneName)
    {
        var sceneManager = SceneManagerPersistent.getInstance();
        if (sceneManager != null)
        {
            sceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.LogError("SceneManagerPersistent instance not found.");
        }
    }

    // Proxy method for loading a scene from a pool by name
    public void LoadSceneFromPool(string poolName)
    {
        var sceneManager = SceneManagerPersistent.getInstance();
        if (sceneManager != null)
        {
            sceneManager.LoadSceneFromPool(poolName);
        }
        else
        {
            Debug.LogError("SceneManagerPersistent instance not found.");
        }
    }
}

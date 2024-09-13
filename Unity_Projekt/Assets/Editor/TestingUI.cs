using UnityEngine;
using UnityEditor;

public class AmbiencePlayerEditorWindow : EditorWindow
{
    private AmbiencePlayer ambiencePlayer; // Reference to the AmbiencePlayer script

    // Add menu item to open the window
    [MenuItem("Window/Ambience Player Control")]
    public static void ShowWindow()
    {
        // Create the window
        GetWindow<AmbiencePlayerEditorWindow>("Ambience Player Control");
    }

    private void OnEnable()
    {
        // Try to find the AmbiencePlayer by name in the scene when the window is opened
        GameObject foundObject = GameObject.Find("AmbiencePlayer");

        if (foundObject != null)
        {
            ambiencePlayer = foundObject.GetComponent<AmbiencePlayer>();
        }
    }

    private void OnGUI()
    {
        // Title for the window
        GUILayout.Label("Ambience Control Panel", EditorStyles.boldLabel);

        // Reference to the AmbiencePlayer component
        ambiencePlayer = (AmbiencePlayer)EditorGUILayout.ObjectField("Ambience Player", ambiencePlayer, typeof(AmbiencePlayer), true);

        // If a valid AmbiencePlayer is assigned
        if (ambiencePlayer != null)
        {
            // Button to trigger Lightning
            if (GUILayout.Button("Trigger Lightning"))
            {
                ambiencePlayer.PlayLightning();
            }

            // Button to trigger Thunder
            if (GUILayout.Button("Trigger Thunder"))
            {
                ambiencePlayer.PlayThunder();
            }
        }
        else
        {
            EditorGUILayout.HelpBox("AmbiencePlayer object not found. Please assign manually.", MessageType.Warning);
        }
    }
}

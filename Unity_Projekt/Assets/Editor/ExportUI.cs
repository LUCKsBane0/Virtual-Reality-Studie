using UnityEngine;
using UnityEditor;

public class PersonDataEditorWindow : EditorWindow
{
    public StudienTeilnehmer personData; // Reference to the ScriptableObject
    private ExportSystem exporter; // Reference to the exporter component

    [MenuItem("Window/Person Data Exporter")]
    public static void ShowWindow()
    {
        // Show an editor window
        GetWindow<PersonDataEditorWindow>("Person Data Exporter");
    }

    private void OnGUI()
    {
        // Display a field to assign the ScriptableObject
        GUILayout.Label("Person Data Exporter", EditorStyles.boldLabel);

        personData = (StudienTeilnehmer)EditorGUILayout.ObjectField("Person Data", personData, typeof(StudienTeilnehmer), false);

        if (GUILayout.Button("Export Data"))
        {
            if (personData != null)
            {
                ExportData();
            }
            else
            {
                EditorUtility.DisplayDialog("Error", "Please assign a PersonData ScriptableObject before exporting.", "OK");
            }
        }

        if (GUILayout.Button("Upload Data to Server"))
        {
            if (personData != null)
            {
                ExportData(); // First, make sure the data is exported
                UploadDataToServer(); // Then upload it
            }
            else
            {
                EditorUtility.DisplayDialog("Error", "Please assign a PersonData ScriptableObject before uploading.", "OK");
            }
        }
    }

    private void ExportData()
    {
        // Create a temporary GameObject with the ExportSystem component attached
        GameObject tempGO = new GameObject("TempExporter");
        exporter = tempGO.AddComponent<ExportSystem>();
        exporter.personData = personData; // Assign the ScriptableObject to the exporter

        // Call the ExportData method
        exporter.ExportData();

        // Clean up
        DestroyImmediate(tempGO);
    }

    private void UploadDataToServer()
    {
        // Create a temporary GameObject with the ExportSystem component attached
        GameObject tempGO = new GameObject("TempExporter");
        exporter = tempGO.AddComponent<ExportSystem>();
        exporter.personData = personData; // Assign the ScriptableObject to the exporter

        // Call the UploadToServer method (we assume the data has been exported already)
        exporter.UploadToServer();

        // Clean up
        DestroyImmediate(tempGO);
    }
}

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

        personData = (StudienTeilnehmer )EditorGUILayout.ObjectField("Person Data", personData, typeof(StudienTeilnehmer ), false);

        if (GUILayout.Button("Export Data"))
        {
            if (personData != null)
            {
                // Create a temporary GameObject with the ScriptableObjectExporter attached to it
                GameObject tempGO = new GameObject("TempExporter");
                exporter = tempGO.AddComponent<ExportSystem>();
                exporter.personData = personData; // Assign the ScriptableObject to the exporter

                // Call the ExportData method
                exporter.ExportData();

                // Destroy the temporary GameObject after exporting
                DestroyImmediate(tempGO);
            }
            else
            {
                EditorUtility.DisplayDialog("Error", "Please assign a PersonData ScriptableObject before exporting.", "OK");
            }
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;



public class ExportSystem : MonoBehaviour

{

    
    public StudienTeilnehmer personData; // Assign in Unity Inspector
    public string fileName = "personData.json";

    // Method to export data from the ScriptableObject to JSON
    public void ExportData()
    {
        // Convert the ScriptableObject data to JSON
        string json = JsonUtility.ToJson(personData, true);

        // Save the JSON string to a file
        string path = Path.Combine(Application.persistentDataPath, fileName);
        File.WriteAllText(path, json);

        Debug.Log("Data exported to: " + path);
    }
}

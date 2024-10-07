using System.Collections;
using UnityEngine;
using System.IO;
using UnityEngine.Networking;
using System.Collections.Generic;

public class ExportSystem : MonoBehaviour
{
    public StudienTeilnehmer personData; // Assign in Unity Inspector
    public string fileName = "personData.json";
    public string uploadUrl = "http://localhost:3000/upload"; // URL of your local server

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        ResetPersonData();
    }

    // Method to export data from the ScriptableObject to JSON
    public void ExportData()
    {
        string json = JsonUtility.ToJson(personData, true);
        string path = Path.Combine(Application.persistentDataPath, fileName);
        File.WriteAllText(path, json);

        Debug.Log("Data exported to: " + path);
    }

    // Upload JSON file to a file server
    public void UploadToServer()
    {
        StartCoroutine(UploadFileCoroutine());
    }

    private IEnumerator UploadFileCoroutine()
    {
        string path = Path.Combine(Application.persistentDataPath, fileName);

        // Check if the file exists
        if (!File.Exists(path))
        {
            Debug.LogError("File does not exist.");
            yield break;
        }

        // Create a UnityWebRequest to send the file to the server
        UnityWebRequest request = UnityWebRequest.Put(uploadUrl, File.ReadAllBytes(path));

        // Set the content type to application/json
        request.SetRequestHeader("Content-Type", "application/json");

        // Send the request and wait for it to complete
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("File upload failed: " + request.error);
        }
        else
        {
            Debug.Log("File uploaded successfully.");
        }
    }

    // Setters for personData attributes
    public void SetID(string id) => personData.ID = id;
    public void SetAlter(int alter) => personData.Alter = alter;
    public void SetGeschlecht(string geschlecht) => personData.Geschlecht = geschlecht;
    public void SetVR_Erfahrung(int erfahrung) => personData.VR_Erfahrung = erfahrung;
    public void SetGaming_Erfahrung(int erfahrung) => personData.Gaming_Erfahrung = erfahrung;
    public void SetStudienTyp(string typ) => personData.StudienTyp = typ;
    public void SetStudienGruppe(string gruppe) => personData.StudienGruppe = gruppe;

    // Adders for lists (Szenarien, Pfad, Bewertungen, ZoegernZeiten, SzenarioZeiten)
    public void AddSzenario(string szenario) => personData.Szenarien.Add(szenario);
    public void AddPfad(string pfad) => personData.Pfad.Add(pfad);
    public void AddBewertung(string bewertung) => personData.Bewertungen.Add(bewertung);
    public void AddZoegernZeit(float zoegernZeit) => personData.Zögern_Zeiten.Add(zoegernZeit);
    public void AddSzenarioZeit(float szenarioZeit) => personData.Szenario_Zeiten.Add(szenarioZeit);

    // Reset the personData to default values and generate a new ID
    public void ResetPersonData()
    {
        personData.ID = System.Guid.NewGuid().ToString(); // Generate a new ID
        personData.Alter = 0;
        personData.Geschlecht = "";
        personData.VR_Erfahrung = 0;
        personData.Gaming_Erfahrung = 0;
        personData.StudienTyp = "";
        personData.StudienGruppe = "";
        personData.Szenarien = new List<string>();
        personData.Pfad = new List<string>();
        personData.Bewertungen = new List<string>();
        personData.Zögern_Zeiten = new List<float>();
        personData.Szenario_Zeiten = new List<float>();

        Debug.Log("PersonData has been reset with a new ID: " + personData.ID);
    }
}

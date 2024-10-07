using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ExportSystem : MonoBehaviour
{
    public StudienTeilnehmer personData; // Assign in Unity Inspector
    public string fileName = "personData.json";

    private void Awake()
    {
        // Ensure this object persists between scene loads
        DontDestroyOnLoad(this.gameObject);
        ResetPersonData();
    }

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

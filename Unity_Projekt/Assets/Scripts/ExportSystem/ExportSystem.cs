using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.IO;
using System.Collections.Generic;

[System.Serializable]
public class AdminCredentials
{
    public string ADMIN_USER;
    public string ADMIN_PASSWORD;
}

public class ExportSystem : MonoBehaviour
{
    public StudienTeilnehmer personData; // Assign in Unity Inspector
    public string fileName = "personData.json";
    public string uploadUrl = "http://192.168.178.43:7331/upload"; // URL of your local server
    private AdminCredentials credentials;
    private bool uploadSuccess;  // Store result of the upload
    private string uploadError;  // Store error message if any
    private void Awake()
    {
        LoadCredentials();
    }

    // Method to export data from the ScriptableObject to JSON
    public void ExportData()
    {
        string modifiedFileName = personData.ID + "_" + fileName;

        string json = JsonUtility.ToJson(personData, true);
        string path = Path.Combine(Application.persistentDataPath, modifiedFileName);
        File.WriteAllText(path, json);

        Debug.Log("Data exported to: " + path);
    }

    // Method to start the file upload and check results later
    public void UploadToServer()
    {
        LoadCredentials();
        StartCoroutine(UploadFileCoroutine((success, error) =>
        {
            // This block will be executed AFTER the coroutine finishes
            if (success)
            {
                Debug.Log("File uploaded successfully.");
            }
            else
            {
                Debug.LogError("File upload failed: " + error);
            }
        }));
    }

    // Coroutine to upload the file to the server
    private IEnumerator UploadFileCoroutine(System.Action<bool, string> callback)
    {
        string modifiedFileName = personData.ID + "_" + fileName;
        string path = Path.Combine(Application.persistentDataPath, modifiedFileName);

        // Check if the file exists
        if (!File.Exists(path))
        {
            Debug.LogError($"File does not exist at path: {path}");
            callback(false, "File does not exist.");
            yield break;
        }

        // Create the form and add the file
        WWWForm form = new WWWForm();
        form.AddBinaryData("file", File.ReadAllBytes(path), modifiedFileName, "application/json");

        // Create the UnityWebRequest
        UnityWebRequest request = UnityWebRequest.Post(uploadUrl, form);
        request.timeout = 10; // Set a timeout for the request

        // Add Basic Authentication header
        if (credentials == null || string.IsNullOrEmpty(credentials.ADMIN_USER) || string.IsNullOrEmpty(credentials.ADMIN_PASSWORD))
        {
            Debug.LogError("Missing credentials: Ensure ADMIN_USER and ADMIN_PASSWORD are set.");
            callback(false, "Missing credentials.");
            yield break;
        }

        string encodedCredentials = System.Convert.ToBase64String(
            System.Text.Encoding.ASCII.GetBytes($"{credentials.ADMIN_USER}:{credentials.ADMIN_PASSWORD}")
        );
        request.SetRequestHeader("Authorization", "Basic " + encodedCredentials);

        Debug.Log($"Sending file to server at: {uploadUrl}");
        Debug.Log($"File Path: {path}");
        Debug.Log($"Authorization Header: Basic {encodedCredentials}");

        // Send the request and wait for the response
        yield return request.SendWebRequest();

        // Handle the response
        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log($"Upload successful! Response: {request.downloadHandler.text}");
            callback(true, request.downloadHandler.text); // Notify success with server response
        }
        else
        {
            Debug.LogError($"Upload failed! Error: {request.error}");
            Debug.LogError($"Response Code: {request.responseCode}");
            Debug.LogError($"Response Text: {request.downloadHandler.text}");
            callback(false, request.error); // Notify failure with error message
        }
    }


    // Load credentials from a JSON file
    private void LoadCredentials()
    {
        TextAsset credentialsFile = Resources.Load<TextAsset>("credentials"); // No need for .json extension
        if (credentialsFile != null)
        {
            string json = credentialsFile.text;
            credentials = JsonUtility.FromJson<AdminCredentials>(json);
            Debug.Log("Credentials loaded successfully.");
        }
        else
        {
            Debug.LogError("Credentials file not found in Resources folder.");
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
    public void AddZoegernZeit(float zoegernZeit) {
        Debug.Log("Actually Adding Zoegern Zeit");
        personData.Z�gern_Zeiten.Add(zoegernZeit); 
    }
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
        personData.Z�gern_Zeiten = new List<float>();
        personData.Szenario_Zeiten = new List<float>();

        Debug.Log("PersonData has been reset with a new ID: " + personData.ID);
    }

    public void setFileName(string fileName_)
    {
        fileName = fileName_;
    }

    public void setUploadURL(string uploadURL_)
    {
        uploadUrl = uploadURL_;
    }
}

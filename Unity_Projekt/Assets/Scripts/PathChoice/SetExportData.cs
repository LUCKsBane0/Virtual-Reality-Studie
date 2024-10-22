using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetExportData : MonoBehaviour
{
    public static bool hasEntered;
    private ExportSystem exportSystem;
    private TimeMeasurement_Local time;
    public string XR_Origin_Name;
    public string PATH_NAME;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == XR_Origin_Name && !hasEntered)
        {
            hasEntered = true;
            exportSystem.AddPfad(PATH_NAME);
            exportSystem.AddZoegernZeit(time.getElapsedTime());
        }
    }

    void Start()
    {
        exportSystem = Object.FindAnyObjectByType<ExportSystem>();
        time = Object.FindAnyObjectByType<TimeMeasurement_Local>();
    }

    // Update is called once per frame
 
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetExportData_path : MonoBehaviour
{
    private ExportSystem exportSystem;
    public string XR_Origin_Name;
    public string PATH_NAME;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == XR_Origin_Name)
        {
            exportSystem.personData.Pfad.Add(PATH_NAME);
        }
    }

    void Start()
    {
        exportSystem = Object.FindAnyObjectByType<ExportSystem>();
    }

    // Update is called once per frame
 
}

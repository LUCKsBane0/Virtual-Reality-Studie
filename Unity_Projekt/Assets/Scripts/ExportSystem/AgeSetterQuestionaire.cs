using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AgeSetterQuestionaire : MonoBehaviour
{
    public ExportSystem exportSystem;
    public Slider slider;
    // Start is called before the first frame update
   public void SetExportAge()
    {
        
        exportSystem.SetAlter((int)slider.value);
    }
}

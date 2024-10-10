using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RateSetterEval : MonoBehaviour
{
    public ExportSystem exportSystem;
    public Slider slider;
    public SceneManagerProxy sceneManagerProxy;
    // Start is called before the first frame update
    public void SetExportEval()
    {

        exportSystem.AddBewertung(sceneManagerProxy.getCurrentScenario() + "_" + slider.value.ToString());
    }
}

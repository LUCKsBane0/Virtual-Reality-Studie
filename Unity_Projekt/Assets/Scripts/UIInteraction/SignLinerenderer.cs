using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactors.Visuals;

public class SignLinerenderer : MonoBehaviour
{
    public XRInteractorLineVisual lineRenderer;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "XR Origin (XR Rig)")
        {
            lineRenderer.enabled = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "XR Origin (XR Rig)")
        {
            lineRenderer.enabled = false;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactors.Visuals;

public class SignLinerenderer : MonoBehaviour
{
    public XRInteractorLineVisual lineRenderer_right;
    public XRInteractorLineVisual lineRenderer_left;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "XR Origin (XR Rig)")
        {
            lineRenderer_left.enabled = true;
            lineRenderer_right.enabled = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "XR Origin (XR Rig)")
        {
            lineRenderer_left.enabled = false;
            lineRenderer_right.enabled = false;
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

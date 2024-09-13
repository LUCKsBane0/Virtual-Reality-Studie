using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateSetSample : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.name == "XR Origin (XR Rig)")
        {
            FindObjectOfType<StateSystem>().SetState(StateSystem.State.SurpassedLine);
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

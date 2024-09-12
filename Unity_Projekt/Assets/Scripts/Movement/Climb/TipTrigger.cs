using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class TipTrigger : MonoBehaviour
{
    
    private void OnTriggerEnter(Collider other)
    {
       
        if (other.tag == "Climbable")
        {
            transform.parent.GetComponent<Climb>().TipEntered(other.gameObject);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Climbable")
        {
            transform.parent.GetComponent<Climb>().TipExited(other.gameObject);
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

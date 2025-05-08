using System.Collections;
using System.Collections.Generic;
using UnityEngine;
    

public class IceAxeAreaExit : MonoBehaviour
{
    [SerializeField] private ToggleIceAxe IceAxeToggle;

    void OnTriggerExit(Collider other)
    {
        // Check if the exiting object is the player
        if (other.CompareTag("Player"))
        {
            IceAxeToggle.ToggleAxesOff();
        }
    }

    //temp untill other way
    void OnTriggerEnter(Collider other)
    {
        // Check if the exiting object is the player
        if (other.CompareTag("Player"))
        {
            IceAxeToggle.ToggleAxesOn();
        }
    }
}

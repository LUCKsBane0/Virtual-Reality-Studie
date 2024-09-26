using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisonTrigger : Trigger
{
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "XR Origin (XR Rig)")
        {
            this.TriggerTargetMet();
        }
    }
}

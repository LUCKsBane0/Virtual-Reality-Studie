using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisonTrigger : Trigger
{
    public string PlayerObjectName;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == PlayerObjectName)
        {
            this.TriggerTargetMet();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceAxePositioner : MonoBehaviour
{
    [SerializeField] private Transform iceAxePositioner; // Reference to the VR controller (can be left or right)
    public void Start()
    {
        Attach();
    }


    public void Attach()
    {
        transform.SetParent(iceAxePositioner);
        // Optionally, reset the local position/rotation if you want to match the target's position exactly
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
    }

    public void Detach()
    {
        transform.SetParent(null);
    }
}

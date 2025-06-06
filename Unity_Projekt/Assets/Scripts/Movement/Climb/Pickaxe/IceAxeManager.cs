using System.Collections.Generic;
using UnityEngine;

public class IceAxeManager : MonoBehaviour
{
    [Header("Axe References")]
    [SerializeField] private GameObject iceAxeR;
    [SerializeField] private GameObject iceAxeL;
    [SerializeField] private IceAxe IceAxeLeft;
    [SerializeField] private IceAxe IceAxeRight;

    [Header("Movement Systems")]
    [SerializeField] private GravityController gravityController;
    [SerializeField] private ArmSwingLocomotion armSwingLocomotion;

    private bool isAxesActive = false;



    // === Axe Toggling ===
    /**
    public void ToggleAxes()
    {
        isAxesActive = !isAxesActive;
        SetAxesActive(isAxesActive);
    }

    public void ToggleAxesOn()
    {
        isAxesActive = true;
        SetAxesActive(true);
    }

    public void ToggleAxesOff()
    {
        isAxesActive = false;
        SetAxesActive(false);
    }

    private void SetAxesActive(bool active)
    {
        if (iceAxeR != null) iceAxeR.SetActive(active);
        if (iceAxeL != null) iceAxeL.SetActive(active);
    }
    */

    // === Movement Handling ===
    public void KillMovement()
    {
        gravityController?.DisableGravity();
        if (armSwingLocomotion != null) armSwingLocomotion.enable = false;
    }

    public void ReviveMovement()
    {
        gravityController?.EnableGravity();
        if (armSwingLocomotion != null) armSwingLocomotion.enable = true;
    }

    public void CheckClimb()
    {
        if (IceAxeLeft != null && IceAxeRight != null)
        {
            if (!IceAxeLeft.IsClimbing() && !IceAxeRight.IsClimbing())
            {
                ReviveMovement();
            }
        }
    }
}
using UnityEngine;

public class IceAxeManager : MonoBehaviour
{
    [Header("Axe References")]
    public GameObject iceAxeR;
    public GameObject iceAxeL;
    public IceAxe IceAxeLeft;
    public IceAxe IceAxeRight;

    [Header("Movement Systems")]
    public GravityController gravityController;
    public ArmSwingLocomotion armSwingLocomotion;

    private IceAxe lastPressedAxe;
    private bool isAxesActive = false;

    // === Axe Toggling ===
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

    // === Climb Input Handling ===
    public void RegisterTriggerPress(IceAxe axe)
    {
        lastPressedAxe = axe;
    }

    public bool CanClimb(IceAxe axe)
    {
        return axe == lastPressedAxe;
    }

    public void ClearClimbState()
    {
        lastPressedAxe = null;
    }

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

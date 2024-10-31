using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ClimbManager : MonoBehaviour
{
    [Header("References")]
    [Tooltip("Reference to the GravityController attached to the XR Origin or player.")]
    public GravityController gravityController;

    [Tooltip("Reference to the ArmSwingLocomotion component.")]
    public ArmSwingLocomotion armSwingLocomotion;

    // A counter to track how many climb interactables are currently active
    private int activeClimbInteractables = 0;

    void Start()
    {
        // Ensure gravity is enabled and arm-swing is enabled at the start
        gravityController.EnableGravity();
        
    }

    /// <summary>
    /// Call this when a climb interactable is activated (grabbed).
    /// </summary>
    public void OnClimbStart()
    {
        activeClimbInteractables++;
        UpdateClimbState();
    }

    /// <summary>
    /// Call this when a climb interactable is deactivated (released).
    /// </summary>
    public void OnClimbStop()
    {
        activeClimbInteractables--;
        UpdateClimbState();
    }

    // Method to update gravity and arm-swing based on climb state
    private void UpdateClimbState()
    {
        bool isClimbing = activeClimbInteractables > 0;

        if (isClimbing)
        {
            gravityController.DisableGravity();
            if (armSwingLocomotion != null)
            {
                armSwingLocomotion.enable = false;
            }
        }
        else
        {
            gravityController.EnableGravity();
            if (armSwingLocomotion != null)
            {
                armSwingLocomotion.enable = true;
            }
        }
    }
}

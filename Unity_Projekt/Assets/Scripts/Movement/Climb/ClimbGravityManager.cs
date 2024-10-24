using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ClimbGravityManager : MonoBehaviour
{
    [Header("References")]
    [Tooltip("Reference to the GravityController attached to the XR Origin or player.")]
    public GravityController gravityController;

    // A counter to track how many climb interactables are currently active
    private int activeClimbInteractables = 0;

    void Start()
    {
        // Ensure gravity is enabled at the start
        gravityController.EnableGravity();
    }

    /// <summary>
    /// Call this when a climb interactable is activated (grabbed).
    /// </summary>
    public void OnClimbStart()
    {
        activeClimbInteractables++;
        if (activeClimbInteractables > 0)
        {
            gravityController.DisableGravity();
        }
    }

    /// <summary>
    /// Call this when a climb interactable is deactivated (released).
    /// </summary>
    public void OnClimbStop()
    {
        activeClimbInteractables--;
        if (activeClimbInteractables <= 0)
        {
            gravityController.EnableGravity();
        }
    }
}

using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Locomotion.Climbing;

public class ClimbInteractableEventHandler : MonoBehaviour
{
    [Header("References")]
    public ClimbManager climbGravityManager;

    private ClimbInteractable grabInteractable;

    void Awake()
    {
        grabInteractable = GetComponent<ClimbInteractable>();

        // Subscribe to the grab events
        grabInteractable.selectEntered.AddListener(OnClimbStarted);
        grabInteractable.selectExited.AddListener(OnClimbStopped);
    }

    void OnDestroy()
    {
        // Unsubscribe from the grab events
        grabInteractable.selectEntered.RemoveListener(OnClimbStarted);
        grabInteractable.selectExited.RemoveListener(OnClimbStopped);
    }

    private void OnClimbStarted(SelectEnterEventArgs args)
    {
        if (climbGravityManager != null)
        {
            climbGravityManager.OnClimbStart();
        }
    }

    private void OnClimbStopped(SelectExitEventArgs args)
    {
        if (climbGravityManager != null)
        {
            climbGravityManager.OnClimbStop();
        }
    }
}

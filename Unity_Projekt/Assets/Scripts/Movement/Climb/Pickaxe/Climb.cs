using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
public class Climb : MonoBehaviour
{
    // Assign the "Climbable" layer in the Inspector
    private XRGrabInteractable grabInteractable;
    public Transform player;          // The player object that moves when climbing
    private bool isStuck = false;     // Track if the pickaxe is stuck
    private Vector3 lastHandPosition; // To track the hand's movement for climbing
    private bool AxeGrabbed = false;


    private void Start()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
        grabInteractable.selectExited.AddListener(OnRelease);
        grabInteractable.selectEntered.AddListener(OnGrab);
    }

    public void TipEntered (GameObject go)
    {
        if (AxeGrabbed == true)
        {
            StickToRock(go.transform);
        }
    }
        


    private void OnGrab(SelectEnterEventArgs arg)
    {
        AxeGrabbed = true;
    }

    private void OnRelease(SelectExitEventArgs arg)
    {
        AxeGrabbed = false;
    }


    private void StickToRock(Transform rock)
    {
        isStuck = true;
        // Save the initial hand position for movement tracking
        lastHandPosition = GetHandPosition();
        player.GetComponent<CharacterController>().enabled = false;  // Disable regular movement

        Debug.Log("Pickaxe is stuck in the rock.");
    }
    public void TipExited(GameObject go)
    {
        ReleaseFromRock();
    }

    private void ReleaseFromRock()
    {
        isStuck = false;
        player.GetComponent<CharacterController>().enabled = true;  // Re-enable regular movement

        Debug.Log("Pickaxe is released from the rock.");
    }
    private Vector3 GetHandPosition()
    {
        // Get the hand/controller position. Replace this with actual hand tracking (e.g., XR input).
        return transform.position;  // Example: use the transform's position
    }
    private void Update()
    {
        if (isStuck)
        {
            Vector3 currentHandPosition = GetHandPosition();
            Vector3 handMovement = currentHandPosition - lastHandPosition;

            // Move the player based on hand movement
            player.position -= handMovement;
            lastHandPosition = currentHandPosition;

            Debug.Log("Player is climbing.");
        }
    }
    // Function to check if a collider is on the climbable layer

}

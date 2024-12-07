using UnityEngine;

public class IceAxeClimb : MonoBehaviour
{
    public GameObject iceWallObject;  // The Ice Wall or terrain object you're interacting with
    public BoxCollider climbCollider; // BoxCollider that checks if the axe is securely embedded for climbing
    private bool isGrabbing = false;  // Whether the grab button is pressed
    private bool isClimbing = false;  // Is the player currently climbing?
    private Transform player;  // Reference to the player's transform (could be the VR Rig or Player object)
    private Vector3 originalPlayerPosition;  // Store the player's position when climbing starts
    private Quaternion originalPlayerRotation;  // Store the player's rotation when climbing starts
    private Transform controller;  // The controller's transform (for VR controller)
    private Vector3 originalAxePosition;  // Store the Ice Axe's position when it locks in place
    private Quaternion originalAxeRotation;  // Store the Ice Axe's rotation when it locks in place

    // New public variable to allow easy changing of the grab key
    public string grabButton = "Grab"; // Set this to the input key or button for grabbing (e.g., "Fire1", "Grip", etc.)

    private void Start()
    {
        // Get the player's transform (for movement adjustments)
        player = GameObject.FindWithTag("Player").transform; // Or any other reference to your player object

        // Get the controller's transform (for tracking the VR controller's position)
        controller = GameObject.FindWithTag("Controller").transform; // Make sure to use the correct tag for your VR controller
    }

    private void Update()
    {
        // Check if the grab/trigger button is pressed (adjust for your VR input system)
        isGrabbing = Input.GetButton(grabButton); // Use the grabButton string to determine if the button is pressed
        if (isGrabbing && !isClimbing)
        {
            CheckClimbable();
        }

        // Handle player movement relative to Ice Axe when climbing
        if (isClimbing)
        {
            HandleClimbing();
        }

        // End the climbing state when the player releases the grab button
        if (!isGrabbing && isClimbing)
        {
            EndClimbing();
        }
    }

    private void CheckClimbable()
    {
        // Get the collider of the iceWallObject (Ice Wall or terrain)
        Collider iceWallCollider = iceWallObject.GetComponent<Collider>();

        // Check if the climb collider intersects with the target's collider
        if (climbCollider.bounds.Intersects(iceWallCollider.bounds))
        {
            Debug.Log("Axe is securely embedded, ready to climb!");
            LockIceAxe();
            StartClimbing();
        }
    }

    private void LockIceAxe()
    {
        // Lock the Ice Axe in place (Disable any movement or interactions with the controller)
        climbCollider.enabled = false; // Disable the climbCollider to prevent further movement

        // Store the Ice Axe's position and rotation when it locks
        originalAxePosition = controller.position;
        originalAxeRotation = controller.rotation;

        // Optionally, disable any interaction logic with the Axe
        // For example, disable the Ice Axe collider if necessary to prevent further movement.
        // e.g., iceAxeCollider.enabled = false;
    }

    private void StartClimbing()
    {
        // Set climbing state to true
        isClimbing = true;

        // Store the player's current position and rotation relative to the Ice Axe
        originalPlayerPosition = player.position;
        originalPlayerRotation = player.rotation;

        Debug.Log("Climbing started!");
    }

    private void HandleClimbing()
    {
        // The player's movement should now be relative to the Ice Axe
        Vector3 controllerMovement = controller.position - originalAxePosition;
        Quaternion controllerRotation = controller.rotation * Quaternion.Inverse(originalAxeRotation);

        // Update the player's position and rotation to match climbing logic
        player.position = originalPlayerPosition + controllerMovement;
        player.rotation = originalPlayerRotation * controllerRotation;

        // Add your own climbing movement here, for example, using player input to control vertical or horizontal movement.
    }

    private void EndClimbing()
    {
        // When climbing ends, re-enable movement control (if necessary)
        isClimbing = false;
        climbCollider.enabled = true; // Re-enable the collider to allow normal interactions
        Debug.Log("Climbing ended!");
    }
}
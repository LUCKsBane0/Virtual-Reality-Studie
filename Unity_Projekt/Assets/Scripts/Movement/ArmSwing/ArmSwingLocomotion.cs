using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Locomotion.Movement;

public class ArmSwingLocomotion : MonoBehaviour
{
    [Header("Locomotion Settings")]
    public bool enable = true;            // Enable/disable arm-swing locomotion
    public float movementThreshold = 0.01f; // Minimum hand movement required to trigger locomotion
    public float movementSpeed = 5.0f;     // Fixed movement speed when threshold is met

    [Header("References")]
    public XRNode leftHandNode = XRNode.LeftHand;   // Left hand input
    public XRNode rightHandNode = XRNode.RightHand; // Right hand input
    public CharacterController characterController;
    public Transform cameraTransform; // Reference to the main camera (headset)
    public ContinuousMoveProvider continuousMoveProvider; // Stick-based movement provider

    private Vector3 leftHandPreviousPosition;        // Previous position of the left hand
    private Vector3 rightHandPreviousPosition;       // Previous position of the right hand

    private void Start()
    {
        // Initialize hand positions
        leftHandPreviousPosition = GetControllerPosition(leftHandNode);
        rightHandPreviousPosition = GetControllerPosition(rightHandNode);

        if (cameraTransform == null)
        {
            cameraTransform = Camera.main.transform; // Assign the camera if not set
        }
    }

    private void Update()
    {
        if (enable)
        {
            // Disable stick-based movement when arm-swing locomotion is enabled
            if (continuousMoveProvider != null)
            {
                continuousMoveProvider.enabled = false;
            }

            ArmSwingMovement();
        }
        else
        {
            // Re-enable stick-based movement when arm-swing is disabled
            if (continuousMoveProvider != null)
            {
                continuousMoveProvider.enabled = true;
            }
        }
    }

    private void ArmSwingMovement()
    {
        
        // Get the current hand positions
        Vector3 leftHandCurrentPosition = GetControllerPosition(leftHandNode);
        Vector3 rightHandCurrentPosition = GetControllerPosition(rightHandNode);

        // Calculate hand movement (up-down motion)
        Vector3 leftHandMovement = leftHandCurrentPosition - leftHandPreviousPosition;
        Vector3 rightHandMovement = rightHandCurrentPosition - rightHandPreviousPosition;

        // Average the movement of both hands
        float handMovement = (Mathf.Abs(leftHandMovement.y) + Mathf.Abs(rightHandMovement.y));

        // Check if the hand movement meets the threshold
        if (handMovement > movementThreshold) // If hands move enough on the y-axis
        {
            // Get the forward direction from the player's camera (ignoring vertical rotation)
            Vector3 movementDirection = cameraTransform.forward;
            movementDirection.y = 0; // Keep movement horizontal
            movementDirection.Normalize();

            // Apply movement at a constant speed
            characterController.Move(movementDirection * movementSpeed);

            // Print the movement direction to the console
            Debug.Log($"Moving in direction: {movementDirection}");
        }

        // Update previous hand positions
        leftHandPreviousPosition = leftHandCurrentPosition;
        rightHandPreviousPosition = rightHandCurrentPosition;
    }

    // Method to get the controller position using InputDevices and CommonUsages.devicePosition
    private Vector3 GetControllerPosition(XRNode handNode)
    {
        InputDevice device = InputDevices.GetDeviceAtXRNode(handNode);
        Vector3 position;

        if (device.TryGetFeatureValue(CommonUsages.devicePosition, out position))
        {
            return position;
        }

        return Vector3.zero; // Return zero if position cannot be obtained
    }
}

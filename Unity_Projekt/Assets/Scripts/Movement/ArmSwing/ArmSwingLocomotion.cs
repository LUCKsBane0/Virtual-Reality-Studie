using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Locomotion.Movement;

public class ArmSwingLocomotion : MonoBehaviour
{
    [Header("Locomotion Settings")]
    public bool enable = true;                // Enable/disable arm-swing locomotion
    public float movementThreshold = 0.01f;   // Minimum hand movement speed required to trigger locomotion
    public float movementSpeed = 5.0f;        // Fixed movement speed when threshold is met
    public float armSpeedThreshold = 0.05f;   // Speed threshold for individual arm movement
    public float movementBufferDuration = 0.5f; // Buffer duration for continuous movement

    [Header("Movement Mode")]
    public bool useArms = true;   // Whether to use arms or camera for determining movement direction

    [Header("References")]
    public XRNode leftHandNode = XRNode.LeftHand;   // Left hand input
    public XRNode rightHandNode = XRNode.RightHand; // Right hand input
    public CharacterController characterController;
    public Transform cameraTransform; // Reference to the main camera (headset)
    public Transform leftControllerTransform;   // Reference to the left controller (set manually or via script)
    public Transform rightControllerTransform;  // Reference to the right controller (set manually or via script)
    public ContinuousMoveProvider continuousMoveProvider; // Stick-based movement provider

    private Vector3 leftHandPreviousPosition;  // Previous position of the left hand
    private Vector3 rightHandPreviousPosition; // Previous position of the right hand

    private float movementBuffer;              // Buffer timer for continuous movement
    private bool isMovingWithArms = false;     // Flag to track if moving by arm-swing

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
            ArmSwingMovement();
        }
    }

    private void ArmSwingMovement()
    {
        isMovingWithArms = false; // Reset the arm-swing movement flag

        // Get the current hand positions
        Vector3 leftHandCurrentPosition = GetControllerPosition(leftHandNode);
        Vector3 rightHandCurrentPosition = GetControllerPosition(rightHandNode);

        // Calculate hand movement (up-down motion)
        Vector3 leftHandMovement = leftHandCurrentPosition - leftHandPreviousPosition;
        Vector3 rightHandMovement = rightHandCurrentPosition - rightHandPreviousPosition;

        // Check if both arms are moving in opposite directions (up and down) and at a speed greater than the threshold
        if (Mathf.Abs(leftHandMovement.y) > armSpeedThreshold && Mathf.Abs(rightHandMovement.y) > armSpeedThreshold)
        {
            // Add both arm movements' y components together
            float combinedYMovement = leftHandMovement.y + rightHandMovement.y;

            // If combined Y movement is close to zero (arms moving opposite to each other), trigger locomotion
            if (Mathf.Abs(combinedYMovement) < movementThreshold)
            {
                // Decide movement direction based on the mode (useArms or camera-based)
                Vector3 movementDirection = useArms ? GetCombinedControllerForwardDirection() : GetCameraForwardDirection();
                movementDirection.y = 0; // Keep movement horizontal
                movementDirection.Normalize();

                // Use SimpleMove to apply movement
                characterController.SimpleMove(movementDirection * movementSpeed);

                // Set buffer timer to keep moving and mark that movement was triggered by arms
                movementBuffer = movementBufferDuration;
                isMovingWithArms = true;

               // Debug.Log($"Moving in direction: {movementDirection}");
            }
        }

        // Continue movement during the buffer period only if not moving with arms
        if (movementBuffer > 0 && !isMovingWithArms)
        {
            Vector3 movementDirection = useArms ? GetCombinedControllerForwardDirection() : GetCameraForwardDirection();
            movementDirection.y = 0;
            movementDirection.Normalize();

            characterController.SimpleMove(movementDirection * movementSpeed);
            movementBuffer -= Time.deltaTime;
        }

        // Update previous hand positions
        leftHandPreviousPosition = leftHandCurrentPosition;
        rightHandPreviousPosition = rightHandCurrentPosition;
    }

    // Method to get the average forward direction from both controllers
    private Vector3 GetCombinedControllerForwardDirection()
    {
        if (leftControllerTransform != null && rightControllerTransform != null)
        {
            // Average the forward directions of both controllers
            Vector3 combinedDirection = (leftControllerTransform.forward + rightControllerTransform.forward) / 2;
            return combinedDirection;
        }
        else if (leftControllerTransform != null)
        {
            return leftControllerTransform.forward;
        }
        else if (rightControllerTransform != null)
        {
            return rightControllerTransform.forward;
        }

        // Default to no movement if both controllers are unavailable
        return Vector3.zero;
    }

    // Method to get the forward direction from the camera
    private Vector3 GetCameraForwardDirection()
    {
        return cameraTransform.forward;
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

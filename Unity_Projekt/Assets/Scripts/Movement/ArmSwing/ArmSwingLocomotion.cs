using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class ArmSwingLocomotion : MonoBehaviour
{
    [Header("Locomotion Settings")]
    public bool enable = true;
    public float movementThreshold = 0.01f;
    public float armSpeedThreshold = 0.05f;
    public float movementBufferDuration = 0.5f;
    public float swingSensitivity = 1.0f;

    [Header("Speed Range")]
    public float minSpeed = 2.0f;
    public float maxSpeed = 10.0f;
    public float speedSmoothing = 0.1f; // New: Smoothing factor for speed transition

    [Header("Movement Mode")]
    public bool useArms = true;

    [Header("References")]
    public XRNode leftHandNode = XRNode.LeftHand;
    public XRNode rightHandNode = XRNode.RightHand;
    public CharacterController characterController;
    public Transform cameraTransform;
    public Transform leftControllerTransform;
    public Transform rightControllerTransform;

    private Vector3 leftHandPreviousPosition;
    private Vector3 rightHandPreviousPosition;

    private float movementBuffer;
    private bool isMovingWithArms = false;
    public float currentMovementSpeed;
    private float targetMovementSpeed; // New: Target speed for smooth transition

    private Vector3[] movementDirections;
    private Vector3 currentMovementDirection;

    [Header("Smoothing Settings")]
    public float directionSmoothing = 0.1f;

    private void Start()
    {
        // Initialize hand positions
        leftHandPreviousPosition = GetControllerPosition(leftHandNode);
        rightHandPreviousPosition = GetControllerPosition(rightHandNode);

        if (cameraTransform == null)
        {
            cameraTransform = Camera.main.transform;
        }

        currentMovementSpeed = minSpeed;
        targetMovementSpeed = minSpeed; // Initialize target speed
        currentMovementDirection = Vector3.zero;

        // Generate 16 predefined movement directions (every 22.5 degrees)
        movementDirections = new Vector3[16];
        for (int i = 0; i < 16; i++)
        {
            float angle = i * 22.5f;
            movementDirections[i] = new Vector3(Mathf.Cos(Mathf.Deg2Rad * angle), 0, Mathf.Sin(Mathf.Deg2Rad * angle));
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
        isMovingWithArms = false;

        // Get current hand positions
        Vector3 leftHandCurrentPosition = GetControllerPosition(leftHandNode);
        Vector3 rightHandCurrentPosition = GetControllerPosition(rightHandNode);

        // Calculate hand movement (up-down motion)
        Vector3 leftHandMovement = leftHandCurrentPosition - leftHandPreviousPosition;
        Vector3 rightHandMovement = rightHandCurrentPosition - rightHandPreviousPosition;

        // Calculate combined hand speed
        float combinedHandSpeed = (Mathf.Abs(leftHandMovement.y) + Mathf.Abs(rightHandMovement.y)) / 2.0f;

        if (Mathf.Abs(leftHandMovement.y) > armSpeedThreshold && Mathf.Abs(rightHandMovement.y) > armSpeedThreshold)
        {
            float combinedYMovement = leftHandMovement.y + rightHandMovement.y;

            if (Mathf.Abs(combinedYMovement) < movementThreshold)
            {
                float adjustedSpeed = combinedHandSpeed * swingSensitivity;
                float speedPercentage = Mathf.Clamp01(adjustedSpeed / armSpeedThreshold);
                targetMovementSpeed = Mathf.Lerp(minSpeed, maxSpeed, speedPercentage); // Set target speed

                // Get the target direction and smooth it
                Vector3 targetDirection = useArms ? GetCombinedControllerForwardDirection() : GetCameraForwardDirection();
                targetDirection = GetClosestDirection(targetDirection);
                currentMovementDirection = Vector3.Lerp(currentMovementDirection, targetDirection, directionSmoothing);

                // Smoothly transition the movement speed
                currentMovementSpeed = Mathf.Lerp(currentMovementSpeed, targetMovementSpeed, speedSmoothing);
                characterController.SimpleMove(currentMovementDirection * currentMovementSpeed);

                movementBuffer = movementBufferDuration;
                isMovingWithArms = true;

               // Debug.Log($"Swing Speed Percentage: {speedPercentage * 100}%, Movement Speed: {currentMovementSpeed} m/s");
            }
        }

        if (movementBuffer > 0 && !isMovingWithArms)
        {
            // Apply smoothing for the buffered movement direction as well
            Vector3 targetDirection = useArms ? GetCombinedControllerForwardDirection() : GetCameraForwardDirection();
            targetDirection = GetClosestDirection(targetDirection);
            currentMovementDirection = Vector3.Lerp(currentMovementDirection, targetDirection, directionSmoothing);

            // Continue moving with smoothed speed
            currentMovementSpeed = Mathf.Lerp(currentMovementSpeed, targetMovementSpeed, speedSmoothing);
            characterController.SimpleMove(currentMovementDirection * currentMovementSpeed);

            movementBuffer -= Time.deltaTime;
        }

        leftHandPreviousPosition = leftHandCurrentPosition;
        rightHandPreviousPosition = rightHandCurrentPosition;
    }

    // Find closest predefined direction
    private Vector3 GetClosestDirection(Vector3 movementDirection)
    {
        float maxDot = -1f;
        Vector3 closestDirection = movementDirection;

        foreach (Vector3 direction in movementDirections)
        {
            float dot = Vector3.Dot(movementDirection, direction);
            if (dot > maxDot)
            {
                maxDot = dot;
                closestDirection = direction;
            }
        }
        return closestDirection;
    }

    private Vector3 GetCombinedControllerForwardDirection()
    {
        if (leftControllerTransform != null && rightControllerTransform != null)
        {
            Vector3 combinedDirection = (leftControllerTransform.forward + rightControllerTransform.forward) / 2;
            return combinedDirection.normalized;
        }
        else if (leftControllerTransform != null)
        {
            return leftControllerTransform.forward;
        }
        else if (rightControllerTransform != null)
        {
            return rightControllerTransform.forward;
        }
        return Vector3.zero;
    }

    private Vector3 GetCameraForwardDirection()
    {
        return cameraTransform.forward;
    }

    private Vector3 GetControllerPosition(XRNode handNode)
    {
        InputDevice device = InputDevices.GetDeviceAtXRNode(handNode);
        if (device.TryGetFeatureValue(CommonUsages.devicePosition, out Vector3 position))
        {
            return position;
        }
        return Vector3.zero;
    }
}

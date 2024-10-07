using UnityEngine;

public class TimerText : MonoBehaviour
{
    public Transform playerCameraTransform; // Reference to the player's camera (or player transform)

    private void Start()
    {
        if (playerCameraTransform == null)
        {
            // Automatically find the player's camera if not set
            playerCameraTransform = Camera.main.transform;
        }
    }

    private void Update()
    {
        // Get the position of the player
        Vector3 playerPosition = playerCameraTransform.position;

        // Get the position of the text (this object)
        Vector3 textPosition = transform.position;

        // Calculate the direction from the text to the player (on the horizontal plane)
        Vector3 directionToPlayer = playerPosition - textPosition;

        // Ignore the vertical difference (Y-axis)
        directionToPlayer.y = 0;

        // Ensure the direction is normalized
        directionToPlayer.Normalize();

        // Calculate the target rotation for the text to face the player
        Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer);

        // Apply the target rotation but keep the X rotation fixed (facing downwards)
        transform.rotation = Quaternion.Euler(0, targetRotation.eulerAngles.y, 0); // Rotated 90 degrees on X to face downwards
    }
}

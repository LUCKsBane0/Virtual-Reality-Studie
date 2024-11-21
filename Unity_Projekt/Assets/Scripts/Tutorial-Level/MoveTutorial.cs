using System.Collections;
using UnityEngine;

public class MoveTutorial : MonoBehaviour
{
    [Header("Tutorial Settings")]
    public GameObject rightController;        // Right controller prefab
    public GameObject leftController;         // Left controller prefab
    public float movementAmplitude = 0.1f;    // Amplitude for up-down movement
    public float animationSpeed = 1.0f;       // Speed of the up-down animation
    public float idleTimeThreshold = 15.0f;   // Time threshold for inactivity
    public float controllerDistance = 1.0f;   // Distance from the camera

    [Header("Activation Settings")]
    public bool isActive = true;              // Controls if the script is active

    private Transform playerCamera;
    private Vector3 previousPlayerPosition;
    private float idleTimer = 0.0f;
    private bool tutorialActive = false;

    private Vector3 rightControllerOffset;
    private Vector3 leftControllerOffset;
    private float animationTimer = 0.0f;

    void Start()
    {
        playerCamera = Camera.main.transform; // Reference the main camera
        previousPlayerPosition = playerCamera.position;

        // Set initial offsets for controllers
        rightControllerOffset = new Vector3(0.2f, 0, 0);  // Right of the camera
        leftControllerOffset = new Vector3(-0.2f, 0, 0); // Left of the camera

        // Deactivate controllers initially
        rightController.SetActive(false);
        leftController.SetActive(false);
    }

    void Update()
    {
        if (!isActive) // If script is deactivated, ensure controllers are hidden and return
        {
            if (tutorialActive)
            {
                tutorialActive = false;
                rightController.SetActive(false);
                leftController.SetActive(false);
            }
            return;
        }

        // Check if the player has moved
        if (Vector3.Distance(playerCamera.position, previousPlayerPosition) > 0.01f)
        {
            // Reset idle timer if movement is detected
            idleTimer = 0.0f;

            // Deactivate tutorial if active
            if (tutorialActive)
            {
                tutorialActive = false;
                rightController.SetActive(false);
                leftController.SetActive(false);
            }
        }
        else
        {
            // Increment idle timer if no movement is detected
            idleTimer += Time.deltaTime;

            // Activate tutorial if idle threshold is reached
            if (idleTimer >= idleTimeThreshold && !tutorialActive)
            {
                ActivateTutorial();
            }
        }

        // Update previous player position
        previousPlayerPosition = playerCamera.position;

        // If tutorial is active, animate and follow camera
        if (tutorialActive)
        {
            AnimateAndFollowCamera();
        }
    }

    private void ActivateTutorial()
    {
        tutorialActive = true;

        // Activate controllers
        rightController.SetActive(true);
        leftController.SetActive(true);
    }

    private void AnimateAndFollowCamera()
    {
        // Calculate up-down animation
        animationTimer += Time.deltaTime * animationSpeed;
        float verticalOffset = Mathf.Sin(animationTimer) * movementAmplitude;

        // Calculate controller positions relative to the camera
        Vector3 rightControllerPosition = playerCamera.position + playerCamera.forward * controllerDistance + rightControllerOffset;
        Vector3 leftControllerPosition = playerCamera.position + playerCamera.forward * controllerDistance + leftControllerOffset;

        // Apply positions and animation
        rightController.transform.position = rightControllerPosition + Vector3.up * verticalOffset;
        leftController.transform.position = leftControllerPosition - Vector3.up * verticalOffset;

        // Ensure controllers are parallel (aligned with the camera's forward direction)
        rightController.transform.rotation = Quaternion.LookRotation(Vector3.forward, Vector3.up);
        leftController.transform.rotation = Quaternion.LookRotation(Vector3.forward, Vector3.up);
    }
}

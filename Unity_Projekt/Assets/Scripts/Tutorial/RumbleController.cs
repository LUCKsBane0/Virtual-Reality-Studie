using System.Collections;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class RumbleController : MonoBehaviour
{
    [Header("XR Controller")]
    public XRNode controllerNode; // Specify whether it's the LeftHand or RightHand controller

    [Header("Rumble Settings")]
    public float intensity = 0.5f;       // Intensity of the rumble (0 to 1)
    public int numberOfRumbles = 3;      // How many times the controller should rumble
    public float durationPerRumble = 0.2f; // Duration of each rumble in seconds

    private InputDevice controllerDevice;

    private void Start()
    {
        // Get the controller device at the start
        controllerDevice = InputDevices.GetDeviceAtXRNode(controllerNode);
    }

    public void TriggerRumble()
    {
        StartCoroutine(Rumble());
    }

    private IEnumerator Rumble()
    {
        HapticCapabilities capabilities;

        if (controllerDevice.TryGetHapticCapabilities(out capabilities))
        {
            // Check if the device supports haptics
            if (capabilities.supportsImpulse)
            {
                for (int i = 0; i < numberOfRumbles; i++)
                {
                    // Send the haptic impulse with the given intensity and duration
                    controllerDevice.SendHapticImpulse(0, intensity, durationPerRumble);

                    // Wait for the duration of each rumble
                    yield return new WaitForSeconds(durationPerRumble);
                }

                // Stop the haptics after the rumbles are done
                controllerDevice.StopHaptics();
            }
            else
            {
                Debug.LogWarning("Haptics not supported on this controller.");
            }
        }
        else
        {
            Debug.LogWarning("Failed to get haptic capabilities for controller.");
        }
    }
}

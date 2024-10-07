using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class RayHandSwitcher : MonoBehaviour
{
    public XRRayInteractor leftHandRay;
    public XRRayInteractor rightHandRay;

    public InputActionReference leftTriggerAction;  // Assign via inspector
    public InputActionReference rightTriggerAction; // Assign via inspector

    private void OnEnable()
    {
        // Subscribe to the trigger input events
        leftTriggerAction.action.performed += OnLeftTriggerPressed;
        rightTriggerAction.action.performed += OnRightTriggerPressed;
    }

    private void OnDisable()
    {
        // Unsubscribe from the trigger input events
        leftTriggerAction.action.performed -= OnLeftTriggerPressed;
        rightTriggerAction.action.performed -= OnRightTriggerPressed;
    }

    private void OnLeftTriggerPressed(InputAction.CallbackContext context)
    {
        SwitchToLeftHand();
    }

    private void OnRightTriggerPressed(InputAction.CallbackContext context)
    {
        SwitchToRightHand();
    }

    private void SwitchToLeftHand()
    {
        leftHandRay.gameObject.SetActive(true);   // Enable left-hand ray
        rightHandRay.gameObject.SetActive(false); // Disable right-hand ray
    }

    private void SwitchToRightHand()
    {
        rightHandRay.gameObject.SetActive(true);  // Enable right-hand ray
        leftHandRay.gameObject.SetActive(false);  // Disable left-hand ray
    }
}

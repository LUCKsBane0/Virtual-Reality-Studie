using UnityEngine;
using UnityEngine.InputSystem;

public class ControllerButtonTrigger : Trigger
{
    [Header("XR Input Action Reference")]
    public InputActionReference inputActionReference; // Reference to the Input Action

    private void OnEnable()
    {
        // Subscribe to the performed event of the InputActionReference
        if (inputActionReference != null && inputActionReference.action != null)
        {
            inputActionReference.action.performed += OnButtonPressed; // Subscribe to the action performed event
        }
    }

    private void OnDisable()
    {
        // Unsubscribe from the performed event to avoid memory leaks, but do not disable the action
        if (inputActionReference != null && inputActionReference.action != null)
        {
            inputActionReference.action.performed -= OnButtonPressed; // Unsubscribe from the event
        }
    }

    private void OnButtonPressed(InputAction.CallbackContext context)
    {
        // Trigger the target met event when the button is pressed
        TriggerTargetMet();
        Debug.Log("XR Button Pressed, Trigger Target Met!");
    }
}

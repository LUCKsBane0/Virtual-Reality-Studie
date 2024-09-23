using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class TutorialStateSubscriber : MonoBehaviour
{
    [System.Serializable]
    public class StateAction
    {
        public TutorialStateSystem.TutorialState state; // The state to react to
        public UnityEvent onStateEntered;              // The action to trigger when entering this state
    }

    public List<StateAction> stateActions; // List of state-action mappings

    private void OnEnable()
    {
        // Subscribe to the state change event
        FindObjectOfType<TutorialStateSystem>().OnTutorialStateChanged += HandleStateChanged;
    }

    private void OnDisable()
    {
        // Unsubscribe to avoid memory leaks
        if (FindObjectOfType<TutorialStateSystem>() != null)
        {
            FindObjectOfType<TutorialStateSystem>().OnTutorialStateChanged -= HandleStateChanged;
        }
    }

    private void HandleStateChanged(TutorialStateSystem.TutorialState newState)
    {
        // Iterate through the list and trigger the actions for the matching state
        foreach (var stateAction in stateActions)
        {
            if (stateAction.state == newState)
            {
                Debug.Log("Entered State: " + newState.ToString());
                stateAction.onStateEntered.Invoke(); // Trigger the UnityEvent
            }
        }
    }

    // Example method to update UI text (this can be hooked up to the UnityEvent in the Inspector)
    public void UpdateUIText(TutorialStateSystem.TutorialState newState)
    {
        transform.Find("Canvas").Find("Text").GetComponent<TextMeshProUGUI>().SetText("Current State: " + newState.ToString());
    }
}

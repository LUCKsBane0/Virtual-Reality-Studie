using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TutorialStateSetter : MonoBehaviour
{
    [System.Serializable]
    public class StateTransition
    {
        public TutorialStateSystem.TutorialState currentState;  // The current state to check
        public TutorialStateSystem.TutorialState newState;      // The new state to set if the condition is met
        public UnityEvent<bool> condition;                      // UnityEvent to evaluate the condition
    }

    public List<StateTransition> stateTransitions;              // List of state transitions
    private TutorialStateSystem tutorialStateSystem;            // Reference to the state system

    private void Start()
    {
        tutorialStateSystem = FindObjectOfType<TutorialStateSystem>();

        if (tutorialStateSystem == null)
        {
            Debug.LogError("TutorialStateSystem not found in the scene.");
        }
    }

    // Call this method to evaluate conditions and set states
    public void CheckAndSetState()
    {
        foreach (var transition in stateTransitions)
        {
            // Check if the current state matches
            if (tutorialStateSystem.currentState == transition.currentState)
            {
                // Set up a bool that will be passed as a condition
                bool isConditionMet = false;

                // Invoke the condition to get the result
                transition.condition.Invoke(isConditionMet);

                if (isConditionMet) // If condition is met, change state
                {
                    SetNewState(transition.newState);
                    break; // Stop after the first valid transition
                }
            }
        }
    }

    private void SetNewState(TutorialStateSystem.TutorialState newState)
    {
        tutorialStateSystem.SetState(newState);
        Debug.Log($"State changed to: {newState}");
    }
}

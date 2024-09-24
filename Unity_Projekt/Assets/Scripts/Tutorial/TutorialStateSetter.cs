using System.Collections.Generic;
using UnityEngine;

public class TutorialStateSetter : MonoBehaviour
{
    [System.Serializable]
    public class StateTransition
    {
        public TutorialStateSystem.TutorialState previousState; // The state to transition from
        public TutorialStateSystem.TutorialState targetState;   // The state to transition to
    }

    [System.Serializable]
    public class StateCondition
    {
        public Trigger trigger;  // Reference to the Trigger component
        public List<StateTransition> stateTransitions = new List<StateTransition>(); // List of state transitions (previous -> target)
    }

    [Header("State System")]
    public List<StateCondition> stateConditions = new List<StateCondition>(); // List of conditions and transitions

    private TutorialStateSystem tutorialStateSystem;  // Reference to the state system

    private void Start()
    {
        // Find the TutorialStateSystem in the scene
        tutorialStateSystem = FindObjectOfType<TutorialStateSystem>();
        if (tutorialStateSystem == null)
        {
            Debug.LogError("TutorialStateSystem not found in the scene.");
        }

        // Subscribe to the OnTargetMet event of each Trigger
        foreach (var stateCondition in stateConditions)
        {
            if (stateCondition.trigger != null)
            {
                stateCondition.trigger.OnTargetMet.AddListener(() => OnConditionMet(stateCondition));
            }
        }
    }

    private void OnDestroy()
    {
        // Unsubscribe from the events to avoid memory leaks
        foreach (var stateCondition in stateConditions)
        {
            if (stateCondition.trigger != null)
            {
                stateCondition.trigger.OnTargetMet.RemoveListener(() => OnConditionMet(stateCondition));
            }
        }
    }

    private void OnConditionMet(StateCondition stateCondition)
    {
        // Get the current state of the system
        var currentState = tutorialStateSystem.currentState;

        // Check each transition to see if it matches the current state
        foreach (var transition in stateCondition.stateTransitions)
        {
            if (transition.previousState == currentState)
            {
                // If a match is found, transition to the target state
                tutorialStateSystem.SetState(transition.targetState);
                Debug.Log($"State changed from {transition.previousState} to {transition.targetState}");
                return; // Exit once the state is changed
            }
        }
        
        Debug.LogWarning($"No valid transition found from state {currentState}");
    }
}

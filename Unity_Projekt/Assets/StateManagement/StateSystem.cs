using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateSystem : MonoBehaviour
{
    public enum State
    {
        Start,Questionaire_QAGE, Questionaire_QGENDER, Questionaire_QVR, Questionaire_QGAMING, END
    }

    // Store the current state
    public State currentState;

    // Define an event that gets triggered when the state changes
    public event Action<State> OnStateChanged;

    // Method to change the state and notify subscribers
    public void SetState(State newState)
    {
        // Check if the new state is different from the current state
        if (newState != currentState)
        {
            // Update the current state
            currentState = newState;

            // Notify all subscribers about the state change
            OnStateChanged?.Invoke(currentState);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        // Example: Set initial state to Start
        SetState(State.Start);
    }

    // Update is called once per frame
    void Update()
    {
        // For testing: Change state on key press (you can remove this later)
    }
}

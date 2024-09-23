using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TutorialStateSystem : MonoBehaviour
{ public enum TutorialState { Start,LookAroundLeft, LookAroundRight, Controller,RayButton,Rayslider,
RayHands,Movement,End,SurpassedDoor}

    // Store the current state
    public TutorialState currentState;

    // Define an event that gets triggered when the state changes
    public event Action<TutorialState> OnTutorialStateChanged;

    // Method to change the state and notify subscribers
    public void SetState(TutorialState newState)
    {
        // Check if the new state is different from the current state
        if (newState != currentState)
        {
            // Update the current state
            currentState = newState;

            // Notify all subscribers about the state change
            OnTutorialStateChanged?.Invoke(currentState);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        // Example: Set initial state to Start
        SetState(TutorialState.Start);
    }

    // Update is called once per frame
    void Update()
    {
        // For testing: Change state on key press (you can remove this later)
    }
}

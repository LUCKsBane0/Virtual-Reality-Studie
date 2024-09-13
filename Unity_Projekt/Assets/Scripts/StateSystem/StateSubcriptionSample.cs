using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StateSubcriptionSample : MonoBehaviour
{
    private void OnEnable()
    {
        // Subscribe to the state change event
        FindObjectOfType<StateSystem>().OnStateChanged += HandleStateChanged;
    }

    private void OnDisable()
    {
        // Unsubscribe from the state change event to avoid memory leaks
        FindObjectOfType<StateSystem>().OnStateChanged -= HandleStateChanged;
    }

    private void HandleStateChanged(StateSystem.State newState)
    {
        // React to the state change
        if (newState == StateSystem.State.SurpassedLine)
        {
            Debug.Log("Surpassed the line!");
            GameObject.Find("Text").GetComponent<TextMeshProUGUI>().SetText("Current State: " + newState.ToString());
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

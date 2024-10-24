using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeMeasurement_Local : MonoBehaviour
{
    private bool timerRunning = false;  // Check if the timer is running
    private float elapsedTime = 0f;     // Track the elapsed time

  

    private void Start()
    {
        // Find the TextMeshProUGUI component attached to the "Text" child object
        StartTimer();
        UpdateTimerText(); // Initialize the timer text
    }

    // When the player enters the trigger, start the timer
 

    // Start the timer
    private void StartTimer()
    {
        timerRunning = true;
        elapsedTime = 0f; // Reset the timer
    }

    // Stop the timer
    public void StopTimer()
    {
        timerRunning = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (timerRunning)
        {
            // Increment the elapsed time
            elapsedTime += Time.deltaTime;
            UpdateTimerText(); // Update the UI text with the new time
        }
    }

    // Update the TextMeshProUGUI component with the current time
    private void UpdateTimerText()
    {
        // Format the time as minutes and seconds
        string timeFormatted = string.Format("{0:00}:{1:00}", Mathf.FloorToInt(elapsedTime / 60), Mathf.FloorToInt(elapsedTime % 60));
        
    }
    public float getElapsedTime()
    {
        return elapsedTime;
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeMeasurement : MonoBehaviour
{
    private bool timerRunning = false;  // Check if the timer is running
    private float elapsedTime = 0f;     // Track the elapsed time

    private TextMeshProUGUI timerText;  // Reference to the TextMeshProUGUI component

    private void Start()
    {
        // Find the TextMeshProUGUI component attached to the "Text" child object
        timerText = transform.Find("Canvas").Find("Text").GetComponent<TextMeshProUGUI>();
        UpdateTimerText(); // Initialize the timer text
    }

    // When the player enters the trigger, start the timer
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.name == "XR Origin (XR Rig)")
        {
            StartTimer();
        }
    }

    // When the player exits the trigger, stop the timer
    private void OnTriggerExit(Collider other)
    {
        if (other.transform.name == "XR Origin (XR Rig)")
        {
            StopTimer();
        }
    }

    // Start the timer
    private void StartTimer()
    {
        timerRunning = true;
        elapsedTime = 0f; // Reset the timer
    }

    // Stop the timer
    private void StopTimer()
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
        timerText.SetText("Time in Area: " + timeFormatted);
    }
}

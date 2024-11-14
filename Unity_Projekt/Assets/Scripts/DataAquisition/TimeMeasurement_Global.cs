using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeMeasurement_Global : MonoBehaviour
{
    private bool timerRunning = false;  // Check if the timer is running
    private float elapsedTime = 0f;     // Track the elapsed time

    public TextMeshProUGUI timerText;  // Reference to the TextMeshProUGUI component

    private void Start()
    {

        UpdateTimerText();
    }


    // Start the timer
    public void StartTimer()
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
        timerText.SetText(timeFormatted);
    }
    public float getElapsedTime()
    {
        return elapsedTime;
    }
}

using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class TimeMeasurement_Global : MonoBehaviour
{
    [Header("Timer Settings")]
    private bool timerRunning = false;  // Check if the timer is running
    private float elapsedTime = 0f;     // Track the elapsed time

    [Header("UI Components")]
    public TextMeshProUGUI timerText;   // Reference to the TextMeshProUGUI component

    [Header("Fade and Teleport Settings")]
    private Transform playerTransform;          // Reference to the player transform
    public Transform deathCheckpoint;          // Transform to teleport the player to
    private Volume fadeVolume;                  // Reference to the post-processing volume
    public float fadeDuration = 1.0f;          // Duration for the fade effect
    private bool isFadingOut = false;

    private Vignette vignette;                 // Vignette effect for fade
    private ColorAdjustments colorAdjustments; // Color adjustments for fade

    private void Start()
    {
        fadeVolume = GameObject.FindGameObjectWithTag("Volume").GetComponent<Volume>();
        playerTransform = GameObject.Find("XR Origin (XR Rig)").GetComponent<Transform>();
        if (fadeVolume.profile.TryGet(out vignette) && fadeVolume.profile.TryGet(out colorAdjustments))
        {
            vignette.intensity.value = 0f;  // Start with no vignette
            colorAdjustments.postExposure.value = 0f; // Neutral brightness
        }


        UpdateTimerText(); // Initialize timer text
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

    private void Update()
    {
        if (timerRunning)
        {
            // Increment the elapsed time
            elapsedTime += Time.deltaTime;
            UpdateTimerText(); // Update the UI text with the new time

            // Check if the timer has reached 7 minutes
            if (elapsedTime >= 420f) // 7 minutes in seconds
            {
                timerRunning = false; // Stop the timer
                StartCoroutine(FadeOutAndTeleport());
            }
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

    // Coroutine to fade out, teleport, and fade in
    private IEnumerator FadeOutAndTeleport()
    {
        if (isFadingOut) yield break;

        isFadingOut = true;

        // Fade out the screen (increase vignette intensity)
        float timer = 0f;
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            float weight = Mathf.Lerp(0, 1, timer / fadeDuration);
            vignette.intensity.value = weight;
            colorAdjustments.postExposure.value = weight * -10f; // Darken the screen

            yield return null;
        }

        // Teleport the player to the death checkpoint
        playerTransform.position = deathCheckpoint.position;
        playerTransform.rotation = deathCheckpoint.rotation;

        // Fade in the screen (decrease vignette intensity)
        timer = 0f;
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            float weight = Mathf.Lerp(1, 0, timer / fadeDuration);
            vignette.intensity.value = weight;
            colorAdjustments.postExposure.value = weight * -10f; // Brighten the screen back to normal

            yield return null;
        }

        // Reset post-exposure only after fade-out is fully complete
        colorAdjustments.postExposure.value = 0f;
        isFadingOut = false;
    }
}

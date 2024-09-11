using UnityEngine;

public class MoveSound : MonoBehaviour
{
    public AudioSource audioSource;  // Assign the AudioSource component
    public AudioClip[] stepSounds;   // Array of step sound clips
    public float stepInterval = 0.5f;  // Time between steps
    private float stepTimer;
    private Vector3 previousPosition;

    void Start()
    {
        stepTimer = stepInterval;
        previousPosition = transform.position;  // Store the initial position
    }

    void Update()
    {
        // Calculate the distance the player has moved since the last frame
        float distanceMoved = Vector3.Distance(previousPosition, transform.position);

        if (distanceMoved > 0.01f)  // Threshold to detect meaningful movement
        {
            stepTimer -= Time.deltaTime;
            if (stepTimer <= 0f)
            {
                PlayRandomStepSound();
                stepTimer = stepInterval;  // Reset timer
            }
        }
        else
        {
            stepTimer = stepInterval;  // Reset timer when not moving
        }

        // Update previous position to current position at the end of the frame
        previousPosition = transform.position;
    }

    void PlayRandomStepSound()
    {
        if (stepSounds.Length > 0)
        {
            // Pick a random step sound from the array
            int randomIndex = Random.Range(0, stepSounds.Length);
            audioSource.clip = stepSounds[randomIndex];
            audioSource.Play();
        }
    }
}

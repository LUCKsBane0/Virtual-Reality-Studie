using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StepSoundSet
{
    public List<AudioClip> stepSounds;  // A single set of step sounds
}

public class MoveSound : MonoBehaviour
{
    [Header("Audio Source")]
    public AudioSource audioSource;  // Assign the AudioSource component

    [Header("Step Sounds")]
    public List<StepSoundSet> stepSoundSets = new List<StepSoundSet>();  // List of step sound sets
    public int currentSet = 0;  // Index to select which set of sounds to use

    [Header("Step Timing")]
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
        // Check if the current set index is valid and the set contains sounds
        if (currentSet >= 0 && currentSet < stepSoundSets.Count && stepSoundSets[currentSet].stepSounds.Count > 0)
        {
            // Pick a random step sound from the selected set
            int randomIndex = Random.Range(0, stepSoundSets[currentSet].stepSounds.Count);
            audioSource.clip = stepSoundSets[currentSet].stepSounds[randomIndex];
            audioSource.Play();
        }
        else
        {
            Debug.LogWarning("Invalid sound set or set is empty.");
        }
    }
}

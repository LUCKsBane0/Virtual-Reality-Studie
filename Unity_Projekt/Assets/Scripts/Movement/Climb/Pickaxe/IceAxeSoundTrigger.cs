using System.Collections.Generic;
using UnityEngine;

public class IceAxeSoundTrigger : MonoBehaviour
{
    [SerializeField] private AudioClip[] enterSounds; // Array of audio clips for entering ice
    [SerializeField] private AudioClip[] exitSounds;  // Array of audio clips for exiting ice
    [SerializeField] private List<Collider> allowedIceWalls;
    private AudioSource audioSource;
    private bool IsInserted;

    private void Start()
    {
        // Get the AudioSource attached to this GameObject
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // Only respond to collisions with the specific object
        
        if (allowedIceWalls.Contains(other))
        {
            PlayRandomSound(enterSounds); // Play a random enter sound
            IsInserted = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (IsInserted && allowedIceWalls.Contains(other))
        {
            PlayRandomSound(exitSounds);
            IsInserted = false;
        }
    }

    private void PlayRandomSound(AudioClip[] soundArray)
    {
        if (soundArray.Length > 0)
        {
            Debug.Log($"played sound");
            AudioClip clip = soundArray[Random.Range(0, soundArray.Length)];
            audioSource.PlayOneShot(clip);
        }
    }
}
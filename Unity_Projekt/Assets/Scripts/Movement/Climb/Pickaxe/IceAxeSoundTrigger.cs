using UnityEngine;

public class IceAxeSoundTrigger : MonoBehaviour
{
    public AudioClip[] enterSounds; // Array of audio clips for entering ice
    public AudioClip[] exitSounds;  // Array of audio clips for exiting ice
    public GameObject iceWallObject; // Assign the specific object in the Inspector (To only check for IceWall collisions)
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
        if (other.gameObject == iceWallObject)
        {
            PlayRandomSound(enterSounds); // Play a random enter sound
            IsInserted = true;
        }
    }

    public void PlayExitSound()
    {
        PlayRandomSound(exitSounds);
        IsInserted = false;
    }
    private void OnTriggerExit(Collider other)
    {
        if (IsInserted && other.gameObject == iceWallObject)
        {
            PlayExitSound();
        }
    }


    private void PlayRandomSound(AudioClip[] soundArray)
    {
        if (soundArray.Length > 0)
        {
            // Pick a random clip from the array
            AudioClip clip = soundArray[Random.Range(0, soundArray.Length)];
            audioSource.PlayOneShot(clip);
        }
    }
}
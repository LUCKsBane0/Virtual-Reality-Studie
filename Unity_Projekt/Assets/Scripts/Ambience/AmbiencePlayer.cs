using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbiencePlayer : MonoBehaviour
{
    [Header("Ambient, Thunder, and Lightning Sounds")]
    public List<AudioClip> Ambiences = new List<AudioClip>(); // Ambient sounds, choosable from Inspector
    public List<AudioClip> LightningSounds = new List<AudioClip>(); // Lightning sounds
    public List<AudioClip> ThunderSounds = new List<AudioClip>(); // Thunder sounds

    [Header("Audio Sources")]
    public AudioSource audioSource_Ambience; // Audio source for ambient sounds
    public AudioSource audioSource_Thunder_Lightning; // Audio source for thunder and lightning sounds

    [Header("Player and Sound Parameters")]
    public Transform player; // The player or central object to base distances on
    public float maxDistance = 50f; // Maximum distance for random sound position (e.g., for thunder and lightning)
    public float fadeDuration = 2f; // Duration for fade-in/out effects

    [Header("Ambience Selection")]
    public int selectedAmbienceIndex = 0; // Index to select specific ambient sound from list

    [Header("Control Play on Start")]
    public bool playAmbienceOnStart = true; // Determines if ambience should play at the start of the scene

    // Play lightning sound at a specific position
    public void PlayLightning(Transform position)
    {
        if (LightningSounds.Count > 0)
        {
            AudioClip clip = LightningSounds[Random.Range(0, LightningSounds.Count)];
            audioSource_Thunder_Lightning.transform.position = position.position; // Set sound position
            audioSource_Thunder_Lightning.clip = clip;
            audioSource_Thunder_Lightning.Play();
        }
    }

    // Play lightning sound at a random position near the player
    public void PlayLightning()
    {
        if (LightningSounds.Count > 0)
        {
            Vector3 randomPosition = player.position + Random.insideUnitSphere * maxDistance;
            randomPosition.y = Mathf.Max(randomPosition.y, 0); // Ensure sound is above ground

            AudioClip clip = LightningSounds[Random.Range(0, LightningSounds.Count)];
            audioSource_Thunder_Lightning.transform.position = randomPosition; // Set random sound position
            audioSource_Thunder_Lightning.clip = clip;
            audioSource_Thunder_Lightning.Play();
        }
    }

    // Play thunder sound at a random position near the player
    public void PlayThunder()
    {
        if (ThunderSounds.Count > 0)
        {
            Vector3 randomPosition = player.position + Random.insideUnitSphere * maxDistance;
            randomPosition.y = Mathf.Max(randomPosition.y, 0); // Ensure sound is above ground

            AudioClip clip = ThunderSounds[Random.Range(0, ThunderSounds.Count)];
            audioSource_Thunder_Lightning.transform.position = randomPosition; // Set random sound position
            audioSource_Thunder_Lightning.clip = clip;
            audioSource_Thunder_Lightning.Play();
        }
    }

    // Play thunder sound at a specific position
    public void PlayThunder(Transform position)
    {
        if (ThunderSounds.Count > 0)
        {
            AudioClip clip = ThunderSounds[Random.Range(0, ThunderSounds.Count)];
            audioSource_Thunder_Lightning.transform.position = position.position; // Set sound position
            audioSource_Thunder_Lightning.clip = clip;
            audioSource_Thunder_Lightning.Play();
        }
    }

    // Add ambience to the list and start playing it
    public void SetAmbience(AudioClip clip)
    {
        if (!Ambiences.Contains(clip))
        {
            Ambiences.Add(clip); // Add the clip to the list if it’s not already there
        }
        StartAmbience();
    }

    // Start playing selected ambient sound with a fade-in effect
    public void StartAmbience()
    {
        if (Ambiences.Count > 0 && audioSource_Ambience.isPlaying == false)
        {
            // Play the selected ambience if it's valid
            if (selectedAmbienceIndex >= 0 && selectedAmbienceIndex < Ambiences.Count)
            {
                AudioClip selectedClip = Ambiences[selectedAmbienceIndex];
                StartCoroutine(FadeInAmbience(selectedClip));
            }
        }
    }

    // Stop playing ambient sound with a fade-out effect
    public void StopAmbience()
    {
        StartCoroutine(FadeOutAmbience());
    }

    // Coroutine to fade in ambient sound
    IEnumerator FadeInAmbience(AudioClip clip)
    {
        Debug.Log("Playing" + clip.name);
        audioSource_Ambience.clip = clip;
        audioSource_Ambience.volume = 0;
        audioSource_Ambience.Play();

        float startTime = Time.time;
        while (audioSource_Ambience.volume < 1)
        {
            audioSource_Ambience.volume = Mathf.Lerp(0, 1, (Time.time - startTime) / fadeDuration);
            yield return null;
        }
    }

    // Coroutine to fade out ambient sound
    IEnumerator FadeOutAmbience()
    {
        float startVolume = audioSource_Ambience.volume;
        float startTime = Time.time;

        while (audioSource_Ambience.volume > 0)
        {
            audioSource_Ambience.volume = Mathf.Lerp(startVolume, 0, (Time.time - startTime) / fadeDuration);
            yield return null;
        }

        audioSource_Ambience.Stop(); // Stop the audio after fading out
    }

    void Start()
    {
        // Play ambience on start if playAmbienceOnStart is true
        if (playAmbienceOnStart && Ambiences.Count > 0)
        {
            StartAmbience();
            Debug.Log("Starting Ambience");
        }
    }

    void Update()
    {
        
    }
}

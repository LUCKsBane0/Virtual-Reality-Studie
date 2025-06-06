using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class IceAxe : MonoBehaviour
{
    [Header("Input")]
    [SerializeField] private InputActionReference gripAction;
    [SerializeField] private float gripThreshold = 0.8f;

    [Header("Sound")]
    [SerializeField] private AudioClip[] enterSounds; // Array of audio clips for entering ice
    [SerializeField] private AudioClip[] exitSounds;  // Array of audio clips for exiting ice
    private AudioSource audioSource;

    [SerializeField] private IceAxe OtherIceAxe;

    [SerializeField] private List<Collider> allowedIceWalls;
    [SerializeField] private IceAxeManager iceAxeManager;

    private bool isInClimbZone = false;
    private bool isAttached = false;
    private float lastAttachTime = -Mathf.Infinity;
    private bool gripWasPressed = false;

    private void Start()
    {
        // Get the AudioSource attached to this GameObject
        audioSource = GetComponent<AudioSource>();
    }
    private void Update()
    {
        float gripValue = gripAction.action.ReadValue<float>();
        bool gripPressed = gripValue >= gripThreshold;

        if (gripPressed && !gripWasPressed)
        {
            OtherIceAxe.Detach();
            TryAttach();
        }
        else if (!gripPressed && gripWasPressed)
        {
            Detach();
        }
        if(isAttached)
        {
            //Do climb logic
        }


        gripWasPressed = gripPressed;
    }

    private void TryAttach()
    {
        if (!isInClimbZone) return;

        isAttached = true;
        lastAttachTime = Time.time;
        iceAxeManager?.KillMovement();

        Debug.Log($"{name}: Attached to IceWall.");
    }

    private void Detach()
    {
        if (!isAttached) return;

        isAttached = false;
        iceAxeManager?.CheckClimb();
        Debug.Log($"{name}: Detached.");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (allowedIceWalls.Contains(other))
        {
            isInClimbZone = true;
            PlayRandomSound(enterSounds);
            Debug.Log($"{name}: Entered climb zone.");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (allowedIceWalls.Contains(other))
        {
            isInClimbZone = false;
            PlayRandomSound(exitSounds);
            Debug.Log($"{name}: Exited climb zone.");
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

    public bool IsClimbing()
    {
        return isAttached;
    }
}

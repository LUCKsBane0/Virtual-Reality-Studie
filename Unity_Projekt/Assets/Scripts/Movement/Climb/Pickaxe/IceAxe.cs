using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class IceAxe : MonoBehaviour
{
    [Header("Input")]
    [SerializeField] private InputActionReference gripAction;
    [SerializeField] private float gripThreshold = 0.8f;

    [Header("Sound")]
    [SerializeField] private AudioClip[] enterSounds;
    [SerializeField] private AudioClip[] exitSounds;
    private AudioSource audioSource;

    [Header("References")]
    [SerializeField] private IceAxe otherIceAxe;
    [SerializeField] private List<Collider> allowedIceWalls;
    [SerializeField] private IceAxeManager iceAxeManager;
    [SerializeField] private CharacterController characterController;   // <-- add via Inspector

    private bool isInClimbZone;
    private bool isAttached;
    private bool gripWasPressed;
    private Vector3 lastControllerPos;   // track hand position while attached

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        float gripValue = gripAction.action.ReadValue<float>();
        bool gripPressed = gripValue >= gripThreshold;

        if (gripPressed && !gripWasPressed)
        {
            otherIceAxe.Detach();
            TryAttach();
        }
        else if (!gripPressed && gripWasPressed)
        {
            Detach();
        }

        if (isAttached)
            HandleClimbMotion();   // move player

        gripWasPressed = gripPressed;
    }

    /* ---------- climbing movement ---------- */
    private void HandleClimbMotion()
    {
        // ? controller movement this frame
        Vector3 delta = transform.position - lastControllerPos;
        if (delta.y < 0f && characterController.isGrounded)
        {
            delta.y = 0f;

        }

        characterController.Move(-delta);


        lastControllerPos = transform.position;
    }

    /* ---------- attach / detach ---------- */
    private void TryAttach()
    {
        if (!isInClimbZone) return;

        isAttached = true;
        lastControllerPos = transform.position;   // reset tracker
        iceAxeManager?.KillMovement();
        Debug.Log($"{name}: Attached to IceWall.");
    }

    public void Detach()
    {
        if (!isAttached) return;

        isAttached = false;
        iceAxeManager?.CheckClimb();
        Debug.Log($"{name}: Detached.");
    }

    /* ---------- trigger handling ---------- */
    void OnTriggerEnter(Collider other)
    {
        if (!allowedIceWalls.Contains(other)) return;

        isInClimbZone = true;
        PlayRandomSound(enterSounds);
    }

    void OnTriggerExit(Collider other)
    {
        if (!allowedIceWalls.Contains(other)) return;

        isInClimbZone = false;
        PlayRandomSound(exitSounds);
    }

    /* ---------- audio ---------- */
    private void PlayRandomSound(AudioClip[] clips)
    {
        if (clips.Length == 0) return;

        audioSource.PlayOneShot(clips[Random.Range(0, clips.Length)]);
    }

    public bool IsClimbing() => isAttached;
}

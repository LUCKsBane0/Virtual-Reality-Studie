using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit.Locomotion.Teleportation;

public class IceAxe : MonoBehaviour
{
    [SerializeField] private IceAxe OtherIceAxe;
    [SerializeField] private CharacterController characterController;
    [SerializeField] private IceAxePositioner IceAxePositioner;
    [SerializeField] private IceAxeSoundTrigger IceAxeSoundTrigger;
    [SerializeField] private InputActionProperty gripAction;
    [SerializeField] private BoxCollider colliderSecureClimb;
    [SerializeField] private BoxCollider colliderAudioClimb;
    [SerializeField] private Transform player;
    [SerializeField] private Transform hand;
    [SerializeField] private IceAxeManager IceAxeManager;
    [SerializeField] private float gripThreshold = 0.5f;
    [SerializeField] private bool debugLogs = false;
    [SerializeField] private LayerMask climbableLayers;
    [SerializeField] private float climbCooldown = 0.25f;

    private bool climbing = false;
    private bool gripPressedLastFrame = false;
    private Vector3 lastHandPosition;
    private float lastClimbTime = -1f;

    public bool IsClimbing()
    {
        return climbing;
    }

    public void StopClimb()
    {
        IceAxePositioner.Attach();
        IceAxeSoundTrigger.PlayExitSound();
        climbing = false;
        if (debugLogs) Debug.Log("Stopped climbing!");
    }

    public void StartClimb()
    {
        IceAxePositioner.Detach();
        lastHandPosition = GetHandPosition();

        if (OtherIceAxe != null && OtherIceAxe.IsClimbing())
        {
            OtherIceAxe.StopClimb();
        }

        climbing = true;
        lastClimbTime = Time.time;
        IceAxeManager.KillMovement();

        if (debugLogs) Debug.Log("Started climb!");
    }

    private Vector3 GetHandPosition()
    {
        return hand.position;
    }

    private void Climb()
    {
        Vector3 currentHandPosition = GetHandPosition();
        Vector3 handMovement = currentHandPosition - lastHandPosition;

        if (characterController != null)
        {
            characterController.Move(-handMovement);
            lastHandPosition = currentHandPosition;
        }

        if (debugLogs) Debug.Log("Player is climbing.");
    }

    private bool IsClimbableTouching()
    {
        Collider[] hits = Physics.OverlapBox(colliderSecureClimb.bounds.center, colliderSecureClimb.bounds.extents, colliderSecureClimb.transform.rotation, climbableLayers);
        if (hits.Length > 0) return true;

        hits = Physics.OverlapBox(colliderAudioClimb.bounds.center, colliderAudioClimb.bounds.extents, colliderAudioClimb.transform.rotation, climbableLayers);
        return hits.Length > 0;
    }

    void Update()
    {
        float gripValue = gripAction.action.ReadValue<float>();
        bool gripPressed = gripValue >= gripThreshold;

        if (gripPressed && !gripPressedLastFrame)
        {
            IceAxeManager.RegisterTriggerPress(this);
        }

        if (!climbing && gripPressed && Time.time - lastClimbTime > climbCooldown && IsClimbableTouching())
        {
            if (IceAxeManager.CanClimb(this))
            {
                StartClimb();
            }
        }

        if (climbing && !gripPressed)
        {
            IceAxeManager.CheckClimb();
            StopClimb();
            IceAxeManager.ClearClimbState();
        }

        if (climbing)
        {
            Climb();
        }

        gripPressedLastFrame = gripPressed;
    }
}

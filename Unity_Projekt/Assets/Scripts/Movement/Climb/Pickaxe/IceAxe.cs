using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit.Locomotion.Teleportation;

public class IceAxe : MonoBehaviour
{
    [SerializeField] private IceAxe OtherIceAxe;
    [SerializeField] private CharacterController characterController;
    [SerializeField] private IceAxeEnableMovement IceAxeEnableMovement;
    [SerializeField] private IceAxePositioner IceAxePositioner;
    [SerializeField] private IceAxeSoundTrigger IceAxeSoundTrigger;
    [SerializeField] private InputActionProperty gripAction;
    [SerializeField] private BoxCollider colliderSecureClimb;
    [SerializeField] private BoxCollider colliderAudioClimb;
    [SerializeField] private GameObject iceWall;
    [SerializeField] private Transform player;
    [SerializeField] private Transform hand;

    private bool climbing = false;
    private Vector3 lastHandPosition;

    void Start()
    {
        climbing = false;
    }

    public bool IsClimbing()
    { 
        return climbing; 
    }


    public void StopClimb()
    {
        IceAxePositioner.Attach();
        IceAxeSoundTrigger.PlayExitSound();
        climbing = false;
        Debug.Log("Stopped climbing!");
    }


    private Vector3 GetHandPosition()
    {
        return hand.position;
    }

    private void Climb()
    {
        Vector3 currentHandPosition = GetHandPosition();
        Vector3 handMovement = currentHandPosition - lastHandPosition;

        // Move the player based on hand movement
        if (characterController != null)
        {
            characterController.Move(-handMovement);
        };

        Debug.Log("Player is climbing.");
    }

    void Update()
    {
        if (climbing)
        {
            Climb();
        }

        if (!climbing && gripAction.action.ReadValue<float>() > 0.1f &&
            (colliderSecureClimb.bounds.Intersects(iceWall.GetComponent<Collider>().bounds) ||
             colliderAudioClimb.bounds.Intersects(iceWall.GetComponent<Collider>().bounds)))
        {
            IceAxePositioner.Detach();
            lastHandPosition = GetHandPosition();
            OtherIceAxe.StopClimb();
            climbing = true;
            Debug.Log("Starting climb!");

            IceAxeEnableMovement.KillMovement();
        }



        if (climbing && gripAction.action.ReadValue<float>() <= 0.1f)
        {
            IceAxeEnableMovement.CheckClimb();
            StopClimb();
        }
    }
}



/** This currently doesnt work right because both want to climb at the same time
 *  write a manager that stores is climbing overall and then if one of them achieves climb state switches based on 1 button input
**/
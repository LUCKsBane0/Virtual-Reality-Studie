using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceAxeEnableMovement : MonoBehaviour
{
    public GravityController gravityController;
    public ArmSwingLocomotion armSwingLocomotion;
    public GameObject Locomotion;


    [SerializeField] public IceAxe IceAxeLeft;
    [SerializeField] public IceAxe IceAxeRight;


    public void ReviveMovement()
    {
        gravityController.EnableGravity();
        armSwingLocomotion.enable = true;
        //Locomotion.SetActive(true);
        var characterController = GetComponent<CharacterController>();
        if (characterController != null)
        {
            characterController.enabled = true;
        }
    }

    public void KillMovement()
    {
        gravityController.DisableGravity();
        armSwingLocomotion.enable = false;
        //Locomotion.SetActive(true);
        var characterController = GetComponent<CharacterController>();
        if (characterController != null)
        {
            characterController.enabled = false;
        }
    }


    public void CheckClimb()
    {
        if (IceAxeLeft != null && IceAxeRight != null)
            if (!IceAxeLeft.IsClimbing() && !IceAxeRight.IsClimbing())
            {
                ReviveMovement();
            }
    }
}

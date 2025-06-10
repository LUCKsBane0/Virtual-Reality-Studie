using System.Collections.Generic;
using UnityEngine;

public class IceAxeManager : MonoBehaviour
{
    [Header("Axe References")]
    [SerializeField] private GameObject iceAxeR;
    [SerializeField] private GameObject iceAxeL;
    [SerializeField] private IceAxe iceAxeLeft;
    [SerializeField] private IceAxe iceAxeRight;

    [Header("Movement Systems")]
    [SerializeField] private GravityController gravityController;
    [SerializeField] private ArmSwingLocomotion armSwingLocomotion;

    [Header("Hit / Pick Areas")]
    [SerializeField] public BoxCollider hitTarget;
    [SerializeField] public BoxCollider icePickArea1;
    [SerializeField] public BoxCollider icePickArea2;
    [SerializeField] public BoxCollider icePickArea3;
    [SerializeField] public LineRenderer rope1;
    [SerializeField] public LineRenderer rope2;

    [SerializeField] private CharacterController characterController;

    private bool wasInsideIcePick = false;   // track last frame state

    private bool wasInsidePick13 = false;
    // === Update ===
    void Update()
    {
        // --- per-area checks ---
        bool in1 = icePickArea1.bounds.Intersects(hitTarget.bounds);
        bool in2 = icePickArea2.bounds.Intersects(hitTarget.bounds);
        bool in3 = icePickArea3.bounds.Intersects(hitTarget.bounds);

        bool inside = in1 || in2 || in3;

        /* -------- general enter / exit -------- */
        if (inside)
        {
            if (iceAxeL.activeSelf == false)
            {
                iceAxeL.SetActive(true);
                iceAxeR.SetActive(true);
            }
        }
        else if (!inside)
        {
            if (iceAxeL.activeSelf == true)
            {

                iceAxeL.SetActive(false);
                iceAxeR.SetActive(false);
            }
        }

        if(in1 && wasInsidePick13)
        {
            return;
        }
       
        if (in1 && !wasInsidePick13)
        {
            rope1.enabled = true;
        }
        if (!in1 && wasInsidePick13)
        {
            rope1.enabled = false;
        }



        if (in3 && wasInsidePick13)
        {
            return;
        }
       
        if (in3 && !wasInsidePick13)
        {
            rope2.enabled = true;
        }
        if (!in3 && wasInsidePick13)
        {
            rope2.enabled = false;
        }

        if (!in1 && !in3)
        {
            rope1.enabled = false;
            rope2.enabled = false;
        }


    }

    /* ---------- private fields ---------- */

   


    // === Movement Handling ===
    public void KillMovement()
    {
        gravityController?.DisableGravity();
        if (armSwingLocomotion != null) armSwingLocomotion.enable = false;
    }

    public void ReviveMovement()
    {
        bool groundedAndInPick =
            characterController != null && !characterController.isGrounded &&
            (icePickArea1.bounds.Intersects(hitTarget.bounds) ||
             icePickArea3.bounds.Intersects(hitTarget.bounds));

        if (!groundedAndInPick)
            gravityController?.EnableGravity();   // allow fall
        else
            gravityController?.DisableGravity();  // stay “stuck”

        if (armSwingLocomotion != null) armSwingLocomotion.enable = true;
    }


    public void CheckClimb()
    {
        if (iceAxeLeft != null && iceAxeRight != null)
        {
            if (!iceAxeLeft.IsClimbing() && !iceAxeRight.IsClimbing())
            {
                ReviveMovement();
            }
        }
    }
}

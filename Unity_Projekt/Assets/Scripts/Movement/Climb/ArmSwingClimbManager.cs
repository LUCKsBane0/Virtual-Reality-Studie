using UnityEngine;

public class ArmSwingClimbManager : MonoBehaviour
{
    public ArmSwingLocomotion armSwingLocomotion;  // Reference to the ArmSwingLocomotion component

    private int climbCount = 0;  // Keeps track of how many climb nodes are currently grabbed

    // Call this method when grabbing a climbable node
    public void StartClimbing()
    {
        climbCount++;
        UpdateArmSwingState();
    }

    // Call this method when releasing a climbable node
    public void StopClimbing()
    {
        climbCount = Mathf.Max(0, climbCount - 1);  // Prevent count from going negative
        UpdateArmSwingState();
    }

    private void UpdateArmSwingState()
    {
        // Disable arm-swing movement if any climb nodes are grabbed
        armSwingLocomotion.enable = (climbCount == 0);
    }
}

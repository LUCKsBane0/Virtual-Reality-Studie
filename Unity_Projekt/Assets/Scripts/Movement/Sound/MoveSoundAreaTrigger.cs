using Unity.VisualScripting;
using UnityEngine;

public class MoveSoundAreaTrigger : MonoBehaviour
{
    // Reference to the MoveSound script on the player
    public MoveSound moveSound;

    // The sound set number for this trigger zone
    public int soundSetNumber = 0;

    private void OnTriggerEnter(Collider other)
    {
        // Check if the player enters the trigger zone
        if (other.gameObject.name == "XR Origin (XR Rig)")
        {
            // Change the move sound set based on the assigned soundSetNumber
            moveSound.currentSet = soundSetNumber;
            Debug.Log("Changed Move Sound to Set: " + soundSetNumber);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Optionally, reset to a default sound set when the player exits
        if (other.gameObject.name == "XR Origin (XR Rig)")
        {
            moveSound.currentSet = 0; // 0 can be your default sound set
            Debug.Log("Reset to Default Move Sound Set.");
        }
    }
}

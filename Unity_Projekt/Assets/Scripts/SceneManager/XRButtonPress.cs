using UnityEngine;
using UnityEngine.SceneManagement;

public class XRButtonPress : MonoBehaviour
{
    public SceneManagerProxy SceneManagerProxy;
    public ExportSystem exportSystem;
    public TimeMeasurement_Global time;
    private bool pressed = false;
    // This is called when another collider enters the trigger zone
    private void OnTriggerEnter(Collider other)
    {
       
        // Check if the other object has the name "Direct Interactor"
        if (other.gameObject.name == "Trigger_Button_END")
        {
            OnPressed(); // Call OnPressed when the Direct Interactor touches the button
        }
    }

    // Optional: This can be used for when the Direct Interactor leaves the button's area
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name.Contains("Direct Interactor"))
        {
            OnReleased(); // Call OnReleased when the Direct Interactor leaves the button
        }
    }

    // Custom press logic (visual effects, sound, etc.)
    public void OnPressed()
    {
        Debug.Log("Button Pressed!");
        if(!pressed)
        {
            GetComponent<Trigger>().TriggerTargetMet_DO_USE_WITH_CARE();
            // PUT GLOBAL TIME
            exportSystem.AddSzenarioZeit(time.getElapsedTime());

            // PUT SCENE
            exportSystem.AddSzenario(SceneManager.GetActiveScene().name);
            pressed = true;
        }
      

       
    }

    // Optional: Custom release logic
    public void OnReleased()
    {
        Debug.Log("Button Released!");
        // Logic to return the button to its normal state
    }
}

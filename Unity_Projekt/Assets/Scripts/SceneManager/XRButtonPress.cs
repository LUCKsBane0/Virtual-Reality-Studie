using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;
using UnityEngine.SceneManagement;
public class XRButtonPress : MonoBehaviour
{
    private XRBaseInteractable interactable;
    public SceneManagerProxy SceneManagerProxy;
    public ExportSystem exportSystem;
    public TimeMeasurement_Global time;
    private void Awake()
    {
        interactable = GetComponent<XRBaseInteractable>();

        // Subscribe to hover entered and exited events
        interactable.hoverEntered.AddListener(OnHoverEntered);
        interactable.hoverExited.AddListener(OnHoverExited);
    }

    private void OnDestroy()
    {
        // Unsubscribe to prevent memory leaks
        interactable.hoverEntered.RemoveListener(OnHoverEntered);
        interactable.hoverExited.RemoveListener(OnHoverExited);
    }

    // Function called when the button is touched
    private void OnHoverEntered(HoverEnterEventArgs args)
    {
        if (args.interactorObject is XRDirectInteractor)
        {
            OnPressed(); // Call the function when Direct Interactor touches the button
        }
    }

    // Function called when the button is no longer touched
    private void OnHoverExited(HoverExitEventArgs args)
    {
        if (args.interactorObject is XRDirectInteractor)
        {
            OnReleased(); // Call a function to handle the release action
        }
    }

    // Your custom press logic (visual effects, sound, etc.)
    public void OnPressed()
    {
        //PUT GLOBAL TIME
        exportSystem.AddSzenarioZeit(time.getElapsedTime());
        //PUT SCENE
        exportSystem.AddSzenario(SceneManager.GetActiveScene().name);
        SceneManagerProxy.LoadSceneFromPool("pool");   
        Debug.Log("LOADING SCENE");
    }

    // Optional: Custom release logic
    private void OnReleased()
    {
        Debug.Log("Button Released!");
        // Logic to return the button to its normal state
    }
}

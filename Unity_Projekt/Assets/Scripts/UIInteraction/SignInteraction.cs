using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactors;
using UnityEngine.XR.Interaction.Toolkit.Interactors.Visuals;

public class SignInteraction : MonoBehaviour
{
    private GameObject uiPanel;
   
    public GameObject leftHand; // Assign the Left Hand GameObject
    public GameObject rightHand; // Assign the Right Hand GameObject
    public string postProcessingLayer = "Post Processing"; // The Post Processing Layer name
    private int defaultLayer = 0; // Default layer index, usually 0

    private void Start()
    {
        uiPanel = this.gameObject;
        // Hide the UI panel initially
        if (uiPanel != null)
        {
            uiPanel.SetActive(false);
        }

        // Get default layer index
        defaultLayer = LayerMask.NameToLayer("Default");
    }

    

    // This method will be called when the wooden sign is clicked
    public void OnSelectEntered()
    {
        if (uiPanel != null)
        {
            // Show the UI panel
            uiPanel.SetActive(true);
        }

        // Set the layer of the hands and their children to Post Processing
        SetLayerRecursively(leftHand, LayerMask.NameToLayer(postProcessingLayer));
        SetLayerRecursively(rightHand, LayerMask.NameToLayer(postProcessingLayer));
    }

    public void OnSelectExit()
    {
        if (uiPanel != null)
        {
            // Hide the UI panel
            uiPanel.SetActive(false);
        }

        // Reset the layer of the hands and their children to Default
        SetLayerRecursively(leftHand, defaultLayer);
        SetLayerRecursively(rightHand, defaultLayer);
    }

    // Helper method to set the layer recursively for a GameObject and its children
    private void SetLayerRecursively(GameObject obj, int newLayer)
    {
        if (obj == null) return;

        obj.layer = newLayer;
        foreach (Transform child in obj.transform)
        {
            SetLayerRecursively(child.gameObject, newLayer);
        }
    }
}

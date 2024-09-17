using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactors;
using UnityEngine.XR.Interaction.Toolkit.Interactors.Visuals;

public class SignInteraction : MonoBehaviour
{
    private GameObject uiPanel;
    public XRInteractorLineVisual lineRenderer;
    private void Start()
    {
        uiPanel = this.gameObject;
        // Hide the UI panel initially
        if (uiPanel != null)
        {
            uiPanel.SetActive(false);
        }

      
  
    }

    

    public void hideRaycast()
    {
        lineRenderer.enabled = false;
    }

    public void showRaycast()
    {
        lineRenderer.enabled = true;
    }
    // This method will be called when the wooden sign is clicked
    public void OnSelectEntered()
    {
        if (uiPanel != null)
        {
            // Show the UI panel
            uiPanel.SetActive(true);
        }
    }
    public void OnSelectExit()
    {
        uiPanel.SetActive(false);

    }

    
}

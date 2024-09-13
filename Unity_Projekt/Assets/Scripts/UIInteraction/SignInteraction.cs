using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class SignInteraction : MonoBehaviour
{
    public GameObject uiPanel;         // Assign your UI panel in the Inspector
    public Transform player;           // Assign the player's transform
    public Vector3 panelOffset = new Vector3(0f, 1.5f, 2f); // Offset for the UI Panel position

    private XRBaseInteractor interactor; // To reference the Ray Interactor

    private void Start()
    {
        // Hide the UI panel initially
        if (uiPanel != null)
        {
            uiPanel.SetActive(false);
        }

        // Optionally, find the Ray Interactor if needed
        interactor = FindObjectOfType<XRRayInteractor>();
    }

    // This method will be called when the wooden sign is clicked
    public void OnSelectEntered()
    {
        ShowUIPanel();
    }

    private void ShowUIPanel()
    {
        if (uiPanel != null && player != null)
        {
            // Position the UI panel in front of the player
            uiPanel.transform.position = player.position + player.forward * panelOffset.z + new Vector3(0f, panelOffset.y, 0f);

            // Rotate the panel to face the player
            uiPanel.transform.LookAt(player);

            // Show the UI panel
            uiPanel.SetActive(true);
        }
    }
}

using UnityEngine;

public class ToggleIceAxe : MonoBehaviour
{
    public GameObject iceAxeR; // Assign the Ice Axe GameObjects in the Inspector
    public GameObject iceAxeL;

    private bool isAxesActive = false;

    public void ToggleAxes()
    {
        isAxesActive = !isAxesActive;
        iceAxeR.SetActive(isAxesActive);
        iceAxeL.SetActive(isAxesActive);
    }
    public void ToggleAxesOn()
    {
        isAxesActive = true;
        iceAxeR.SetActive(true);
        iceAxeL.SetActive(true);
    }

    public void ToggleAxesOff()
    {
        isAxesActive = false;
        iceAxeR.SetActive(false);
        iceAxeL.SetActive(false);
    }
}
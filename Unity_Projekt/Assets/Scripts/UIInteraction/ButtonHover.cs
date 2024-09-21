using UnityEngine;
using UnityEngine.UI;

public class ButtonHover : MonoBehaviour
{
    public Image buttonImage; // Reference to the UI Button's Image component
    public Color hoverColor = Color.white; // The color when hovering over the button
    private Color originalColor; // To store the original color of the button

    // Called when the button is hovered over
    public void OnHover()
    {
        if (buttonImage != null)
        {
            buttonImage.color = hoverColor; // Change the color to the hover color
        }
    }

    // Called when the hover ends (reset the button color)
    public void OnHoverExit()
    {
        if (buttonImage != null)
        {
            buttonImage.color = originalColor; // Reset to the original color
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if (buttonImage != null)
        {
            originalColor = buttonImage.color; // Store the original color of the button
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}

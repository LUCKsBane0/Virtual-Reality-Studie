using System.Collections;
using UnityEngine;

public class HighlightControllerButtons : MonoBehaviour
{
    [Header("Materials")]
    public Material originalMaterial;    // Original material
    public Material highlightMaterial;   // Highlight material

    [Header("Controller Button Objects")]
    public Renderer leftTriggerRenderer;  // Renderer for the left trigger button
    public Renderer leftGripRenderer;     // Renderer for the left grip button
    public Renderer rightTriggerRenderer; // Renderer for the right trigger button
    public Renderer rightGripRenderer;    // Renderer for the right grip button

    [Header("Colors")]
    public Color highlightColor = Color.red; // Color for highlighting (set via Inspector)
    public Color resetColor = Color.gray;    // Color to reset both materials to at start

    [Header("Settings")]
    public float fadeDuration = 1f;       // Duration of the fade animation

    private Coroutine currentCoroutine;

    private void Start()
    {
        // Reset both materials' color to gray at the start
        ResetMaterialColors();
    }

    public void HighlightButton(string buttonName)
    {
        Renderer buttonRenderer = GetButtonRenderer(buttonName);
        if (buttonRenderer != null)
        {
            if (currentCoroutine != null)
            {
                StopCoroutine(currentCoroutine);
            }

            // Set the highlight material and start fading to the highlight color
            buttonRenderer.material = highlightMaterial;
            currentCoroutine = StartCoroutine(FadeToColor(buttonRenderer, highlightColor));
        }
    }

    public void RemoveHighlight(string buttonName)
    {
        Renderer buttonRenderer = GetButtonRenderer(buttonName);
        if (buttonRenderer != null)
        {
            if (currentCoroutine != null)
            {
                StopCoroutine(currentCoroutine);
            }

            // Set the original material and reset to the original color
            currentCoroutine = StartCoroutine(FadeToColor(buttonRenderer, resetColor, true));
        }
    }

    private void ResetMaterialColors()
    {
        // Set both the original and highlight materials to the resetColor at start
        originalMaterial.color = resetColor;
        highlightMaterial.color = resetColor;
    }

    private Renderer GetButtonRenderer(string buttonName)
    {
        switch (buttonName)
        {
            case "LeftTrigger":
                return leftTriggerRenderer;
            case "LeftGrip":
                return leftGripRenderer;
            case "RightTrigger":
                return rightTriggerRenderer;
            case "RightGrip":
                return rightGripRenderer;
            default:
                return null;
        }
    }

    // Coroutine to fade the highlight material, then switch back to the original material
    private IEnumerator FadeToColor(Renderer renderer, Color targetColor, bool resetToOriginal = false)
    {
        Material buttonMaterial = renderer.material; // This will create a unique instance of the material for this renderer
        Color startColor = buttonMaterial.color;     // Get the current color
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            // Gradually fade the color to the target color
            buttonMaterial.color = Color.Lerp(startColor, targetColor, elapsedTime / fadeDuration);
            yield return null;
        }

        // Ensure the final color is set exactly
        buttonMaterial.color = targetColor;

        // If resetting to the original material, set the material back to the originalMaterial
        if (resetToOriginal)
        {
            renderer.material = originalMaterial;
            ResetMaterialColors(); // Reset colors after highlight removal
        }
    }
}

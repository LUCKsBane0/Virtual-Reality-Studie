using System.Collections;
using UnityEngine;

public class HighlightControllerButtons : MonoBehaviour
{
    [Header("Materials")]
    public Material originalMaterial;    // Original material (static for all buttons)
    public Material highlightMaterial;   // Base highlight material (will be copied for each button)

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

    // Track separate coroutines for each button
    private Coroutine leftTriggerCoroutine;
    private Coroutine leftGripCoroutine;
    private Coroutine rightTriggerCoroutine;
    private Coroutine rightGripCoroutine;

    private void Start()
    {
        Debug.Log("Starting HighlightControllerButtons script...");
        ResetMaterialColors();  // Reset the colors at the start
    }

    public void HighlightButton(string buttonName)
    {
        Renderer buttonRenderer = GetButtonRenderer(buttonName);
        if (buttonRenderer != null)
        {
            Debug.Log($"HighlightButton called for {buttonRenderer.name}");
            StopAndResetCoroutine(buttonName);  // Stop any running coroutine for the button

            // Create a new instance of the highlight material
            Material newHighlightMaterial = new Material(highlightMaterial);
            newHighlightMaterial.color = highlightColor; // Set the highlight color

            // Set the new material to the button
            buttonRenderer.material = newHighlightMaterial;
            Debug.Log($"Highlight material applied to {buttonRenderer.name}");

            // Start the fade-in animation
            StartFadeCoroutine(buttonName, buttonRenderer, highlightColor, newHighlightMaterial);
        }
    }

    public void RemoveHighlight(string buttonName)
    {
        Renderer buttonRenderer = GetButtonRenderer(buttonName);
        if (buttonRenderer != null)
        {
            Debug.Log($"RemoveHighlight called for {buttonRenderer.name}");
            StopAndResetCoroutine(buttonName);  // Stop any running coroutine for the button

            // Start the fade-out animation
            StartFadeCoroutine(buttonName, buttonRenderer, resetColor, null, true);
        }
    }

    private void ResetMaterialColors()
    {
        Debug.Log("Resetting material colors to resetColor");
        originalMaterial.color = resetColor;
        highlightMaterial.color = resetColor;
    }

    private Renderer GetButtonRenderer(string buttonName)
    {
        switch (buttonName)
        {
            case "LeftTrigger": return leftTriggerRenderer;
            case "LeftGrip": return leftGripRenderer;
            case "RightTrigger": return rightTriggerRenderer;
            case "RightGrip": return rightGripRenderer;
            default: return null;
        }
    }

    // Start fade coroutine for a specific button
    private void StartFadeCoroutine(string buttonName, Renderer buttonRenderer, Color targetColor, Material tempHighlightMaterial = null, bool resetToOriginal = false)
    {
        Coroutine fadeCoroutine = null;

        switch (buttonName)
        {
            case "LeftTrigger":
                leftTriggerCoroutine = StartCoroutine(FadeToColor(buttonRenderer, targetColor, tempHighlightMaterial, resetToOriginal));
                fadeCoroutine = leftTriggerCoroutine;
                break;
            case "LeftGrip":
                leftGripCoroutine = StartCoroutine(FadeToColor(buttonRenderer, targetColor, tempHighlightMaterial, resetToOriginal));
                fadeCoroutine = leftGripCoroutine;
                break;
            case "RightTrigger":
                rightTriggerCoroutine = StartCoroutine(FadeToColor(buttonRenderer, targetColor, tempHighlightMaterial, resetToOriginal));
                fadeCoroutine = rightTriggerCoroutine;
                break;
            case "RightGrip":
                rightGripCoroutine = StartCoroutine(FadeToColor(buttonRenderer, targetColor, tempHighlightMaterial, resetToOriginal));
                fadeCoroutine = rightGripCoroutine;
                break;
        }

        Debug.Log($"Started fade coroutine for {buttonRenderer.name}, Coroutine: {fadeCoroutine}");
    }

    // Stop any running coroutine for the button
    private void StopAndResetCoroutine(string buttonName)
    {
        switch (buttonName)
        {
            case "LeftTrigger":
                if (leftTriggerCoroutine != null)
                {
                    Debug.Log("Stopping leftTriggerCoroutine");
                    StopCoroutine(leftTriggerCoroutine);
                    leftTriggerCoroutine = null;
                }
                break;
            case "LeftGrip":
                if (leftGripCoroutine != null)
                {
                    Debug.Log("Stopping leftGripCoroutine");
                    StopCoroutine(leftGripCoroutine);
                    leftGripCoroutine = null;
                }
                break;
            case "RightTrigger":
                if (rightTriggerCoroutine != null)
                {
                    Debug.Log("Stopping rightTriggerCoroutine");
                    StopCoroutine(rightTriggerCoroutine);
                    rightTriggerCoroutine = null;
                }
                break;
            case "RightGrip":
                if (rightGripCoroutine != null)
                {
                    Debug.Log("Stopping rightGripCoroutine");
                    StopCoroutine(rightGripCoroutine);
                    rightGripCoroutine = null;
                }
                break;
        }
    }

    // Coroutine to fade the material color, then switch back to the original material
    private IEnumerator FadeToColor(Renderer renderer, Color targetColor, Material tempHighlightMaterial = null, bool resetToOriginal = false)
    {
        Debug.Log($"FadeToColor coroutine started for {renderer.name}");
        Material buttonMaterial = renderer.material;  // Get the current material
        Color startColor = buttonMaterial.color;      // Get the current color
        float elapsedTime = 0f;

        Debug.Log($"Starting fade from {startColor} to {targetColor} for {renderer.name}");

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            buttonMaterial.color = Color.Lerp(startColor, targetColor, elapsedTime / fadeDuration);
         
            yield return null;
        }

        // Ensure the final color is set exactly
        buttonMaterial.color = targetColor;
        Debug.Log($"Fade complete for {renderer.name}, target color: {targetColor}");

        // If resetting to the original material, switch back to the original and reset color
        if (resetToOriginal)
        {
            Debug.Log($"Resetting {renderer.name} to original material and color {resetColor}");
            renderer.material = originalMaterial;
            renderer.material.color = resetColor;
        }

        // Clean up the temporary highlight material
        if (tempHighlightMaterial != null && !resetToOriginal)
        {
            Debug.Log($"Destroying temporary highlight material for {renderer.name}");
            Destroy(tempHighlightMaterial);
        }
    }
}

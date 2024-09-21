using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class WhiteOutEffectURP : MonoBehaviour
{
    public Volume postProcessingVolume;  // Reference to the Volume component
    public float fadeSpeed = 1f;         // Speed at which the effect increases or decreases
    private Bloom bloom;
    private ColorAdjustments colorAdjustments;
    private bool isWhitingOut = false;
    private bool isReversingWhiteOut = false;

    private void Start()
    {
        // Get Bloom and Color Adjustments from the Volume
        postProcessingVolume.profile.TryGet<Bloom>(out bloom);
        postProcessingVolume.profile.TryGet<ColorAdjustments>(out colorAdjustments);

        // Initialize the effects to no intensity
        if (bloom != null)
        {
            bloom.intensity.value = 0f;
        }

        if (colorAdjustments != null)
        {
            colorAdjustments.postExposure.value = 0f;
        }
    }

    // This function triggers the white-out effect
    public void TriggerWhiteOut()
    {
        isWhitingOut = true;
        isReversingWhiteOut = false;
    }

    // This function reverses the white-out effect
    public void TriggerOffWhiteOut()
    {
        isReversingWhiteOut = true;
        isWhitingOut = false;
    }

    private void Update()
    {
        if (isWhitingOut)
        {
            // Gradually increase bloom intensity
            if (bloom != null)
            {
                bloom.intensity.value = Mathf.Lerp(bloom.intensity.value, 10f, fadeSpeed * Time.deltaTime);
            }

            // Gradually adjust post-exposure for white-out effect
            if (colorAdjustments != null)
            {
                colorAdjustments.postExposure.value = Mathf.Lerp(colorAdjustments.postExposure.value, -6f, fadeSpeed * Time.deltaTime);
            }
        }

        if (isReversingWhiteOut)
        {
            // Gradually decrease bloom intensity
            if (bloom != null)
            {
                bloom.intensity.value = Mathf.Lerp(bloom.intensity.value, 0f, fadeSpeed * Time.deltaTime);
            }

            // Gradually reset post-exposure to normal
            if (colorAdjustments != null)
            {
                colorAdjustments.postExposure.value = Mathf.Lerp(colorAdjustments.postExposure.value, 0f, fadeSpeed * Time.deltaTime);
            }

            // Stop reversing when fully reset
            if (bloom.intensity.value <= 0.01f && colorAdjustments.postExposure.value >= -0.01f)
            {
                bloom.intensity.value = 0.0f;
                colorAdjustments.postExposure.value = 0.0f;
                isReversingWhiteOut = false;
            }
        }
    }
}

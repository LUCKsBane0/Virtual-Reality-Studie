using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using System.Collections;

public class HealthSystem : MonoBehaviour
{
    [Header("Health Settings")]
    [Tooltip("The maximum health of the player.")]
    public int maxHealth = 100;

    [Tooltip("The current health of the player (starts at maxHealth).")]
    public int currentHealth;

    [Header("Health Bar UI")]
    [Tooltip("The Image component representing the health bar fill.")]
    public Image healthBarFill;  // Reference to the health bar fill UI

    [Header("CheckpointTP")]
    [Tooltip("The transform for the TP on death")]
    public Transform deathCheckpoint;

    [Header("Post-Processing Effects")]
    [Tooltip("The Post-Processing volume for the damage effect.")]
    public Volume damageVolume;  // Reference to the Post-Processing Volume
    private Vignette vignette;   // Vignette effect for damage feedback
    private ColorAdjustments colorAdjustments; // Color effect for damage

    private float damageEffectDuration = 1.0f;  // Duration of the damage effect
    private float damageEffectTimer = 0f;

    private bool isFadingOut = false; // Flag to check if fade-out is in progress
    private float fadeDuration = 1.0f; // Duration for the fade-out effect

    private void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthBar();  // Initialize the health bar

        // Try to get the Vignette and ColorAdjustments effects from the Volume
        if (damageVolume.profile.TryGet(out vignette) && damageVolume.profile.TryGet(out colorAdjustments))
        {
            vignette.intensity.value = 0f;  // Start with no vignette
            colorAdjustments.postExposure.value = 0f; // No brightness boost
        }
    }

    private void Update()
    {
        // Gradually fade the damage effect over time
        if (damageEffectTimer > 0)
        {
            damageEffectTimer -= Time.deltaTime;
            float weight = Mathf.Lerp(0, 1, damageEffectTimer / damageEffectDuration);
            vignette.intensity.value = weight * 0.5f;  // Adjust intensity here
            colorAdjustments.postExposure.value = weight * 1.0f; // Flash effect
        }
        else
        {
            vignette.intensity.value = 0f;  // Ensure it stays off after fading
            colorAdjustments.postExposure.value = 0f; // Reset brightness
        }
    }

    // Method to take damage
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);  // Ensure health doesn't exceed limits

        UpdateHealthBar();

        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
            TriggerDamageEffect();  // Trigger the visual effect
        }
    }

    // Trigger a brief damage effect
    private void TriggerDamageEffect()
    {
        damageEffectTimer = damageEffectDuration;  // Reset the timer for the damage effect
    }

    // Update the health bar UI based on current health
    private void UpdateHealthBar()
    {
        if (healthBarFill != null)
        {
            healthBarFill.fillAmount = (float)currentHealth / maxHealth;
        }
    }

    // Logic for when the player dies
    private void Die()
    {
        Debug.Log("Player has died!");
        StartCoroutine(FadeOutAndTeleport());
    }

    // Coroutine to fade out the screen and teleport the player
    private IEnumerator FadeOutAndTeleport()
    {
        if (isFadingOut) yield break;

        isFadingOut = true;

        // Fade out the screen (increase vignette intensity)
        float timer = 0f;
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            float weight = Mathf.Lerp(0, 1, timer / fadeDuration);
            vignette.intensity.value = weight;
            colorAdjustments.postExposure.value = weight * -10f; // Darken the screen

            yield return null;
        }

        // Teleport the player to the death checkpoint
        transform.position = deathCheckpoint.position;
        transform.rotation = deathCheckpoint.rotation;

        // Fade in the screen (decrease vignette intensity)
        timer = 0f;
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            float weight = Mathf.Lerp(1, 0, timer / fadeDuration);
            vignette.intensity.value = weight;
            colorAdjustments.postExposure.value = weight * -10f; // Brighten the screen back to normal

            yield return null;
        }

        isFadingOut = false;
    }
}

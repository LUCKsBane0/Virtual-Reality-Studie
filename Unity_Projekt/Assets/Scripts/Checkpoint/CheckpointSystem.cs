using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using System.Collections;
using System.Collections.Generic;

public class CheckpointSystem : MonoBehaviour
{

    [Header("Checkpoints")]
    public List<Transform> checkpoints;  // List of checkpoint transforms

    [Header("Post Processing")]
    public Volume postProcessingVolume;  // Reference to the Post-Processing Volume
    public float fadeDuration = 1.0f;    // Duration for fade in/out

    private Transform playerTransform;   // Player's transform
    private bool isFading = false;
    private ColorAdjustments colorAdjustments; // Reference to the Color Adjustments effect

    void Start()
    {
        // Assuming the player is tagged as "Player"
        playerTransform = GameObject.Find("XR Origin (XR Rig)").transform;

        // Ensure the Color Adjustments effect is in the Post-Processing Volume
        if (postProcessingVolume != null)
        {
            postProcessingVolume.profile.TryGet(out colorAdjustments);
        }
    }

    // Method to get the nearest checkpoint to the player's position
    public Transform GetNearestCheckpoint(Vector3 playerPosition)
    {
        Transform nearestCheckpoint = null;
        float minDistance = Mathf.Infinity;

        foreach (Transform checkpoint in checkpoints)
        {
            float distance = Vector3.Distance(playerPosition, checkpoint.position);
            if (distance < minDistance)
            {
                nearestCheckpoint = checkpoint;
                minDistance = distance;
            }
        }

        return nearestCheckpoint;
    }

    // Trigger event for entering the GameOverZone
    private void OnTriggerEnter(Collider other)
    {
        // Check if the player entered the GameOverZone
        if (other.gameObject.name == "XR Origin (XR Rig)" && !isFading)
        {
            if (other.gameObject.GetComponentInChildren<HealthSystem>() != null)
            {
                if(other.gameObject.GetComponentInChildren<HealthSystem>().currentHealth > 0)
                {
                    StartCoroutine(HandleGameOverZone());
                }
            }
            else
            {
                StartCoroutine(HandleGameOverZone());
            }
            
        }
    }

    // Coroutine to fade the screen and teleport the player to the nearest checkpoint
    private IEnumerator HandleGameOverZone()
    {
        isFading = true;

        // Fade to black using post-processing
        yield return StartCoroutine(FadeScreen(1));

        // Teleport player to nearest checkpoint
        Transform nearestCheckpoint = GetNearestCheckpoint(playerTransform.position);
        if (nearestCheckpoint != null)
        {
            playerTransform.position = nearestCheckpoint.position;
            playerTransform.rotation = nearestCheckpoint.rotation;
        }

        // Fade back to normal
        yield return StartCoroutine(FadeScreen(0));

        isFading = false;
    }

    // Coroutine to handle post-processing screen fading
    private IEnumerator FadeScreen(float targetWeight)
    {
        float startWeight = colorAdjustments.postExposure.value;
        float elapsedTime = 0f;

        float targetExposure = (targetWeight == 1) ? -10f : 0f; // Darken screen to fade out, back to 0 for fade in

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            colorAdjustments.postExposure.value = Mathf.Lerp(startWeight, targetExposure, elapsedTime / fadeDuration);
            yield return null;
        }

        colorAdjustments.postExposure.value = targetExposure;
    }
}

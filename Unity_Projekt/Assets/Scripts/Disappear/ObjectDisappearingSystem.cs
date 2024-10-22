using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDisappearingSystem : MonoBehaviour
{
    [Header("Object Groups")]
    public List<GameObject[]> objectGroups; // A list of object arrays (3 groups in total)

    [Header("Timing and Rhythm Settings")]
    public float disappearTime = 2.0f;      // Time the object stays disappeared
    public float intervalBetweenGroups = 1.0f; // Time between group activations
    public float groupRhythmTime = 5.0f;    // Time for group to follow the rhythm

    private void Start()
    {
        // Start the routine to activate groups in rhythm
        StartCoroutine(ActivateGroupsInRhythm());
    }

    private IEnumerator ActivateGroupsInRhythm()
    {
        // Infinite loop to keep triggering the groups in the given rhythm
        while (true)
        {
            for (int i = 0; i < objectGroups.Count; i++)
            {
                GameObject[] currentGroup = objectGroups[i];

                // For each object in the group, randomly select a few to disappear
                foreach (GameObject obj in currentGroup)
                {
                    StartCoroutine(DisappearAndReappear(obj));
                    yield return new WaitForSeconds(Random.Range(0, intervalBetweenGroups)); // Random delay between objects
                }

                // Wait for the rhythm time before activating the next group
                yield return new WaitForSeconds(groupRhythmTime);
            }
        }
    }

    private IEnumerator DisappearAndReappear(GameObject obj)
    {
        // Get the original scale and color
        Vector3 originalScale = obj.transform.localScale;
        Renderer renderer = obj.GetComponent<Renderer>();

        // Create an instance of the material to avoid affecting shared materials
        Material instanceMaterial = new Material(renderer.material);
        renderer.material = instanceMaterial;  // Assign the instance to the object

        // Change color to red and shrink over time
        float shrinkDuration = disappearTime / 2;
        float timer = 0;

        while (timer < shrinkDuration)
        {
            timer += Time.deltaTime;
            float scaleLerp = Mathf.Lerp(1, 0, timer / shrinkDuration);
            obj.transform.localScale = originalScale * scaleLerp;
            instanceMaterial.color = Color.Lerp(instanceMaterial.color, Color.red, timer / shrinkDuration);
            yield return null;
        }

        // After shrinking, make the object completely invisible
        obj.transform.localScale = Vector3.zero;

        // Wait for the disappearTime
        yield return new WaitForSeconds(disappearTime);

        // Reappear by scaling it back up to original size and resetting the color
        timer = 0;
        while (timer < shrinkDuration)
        {
            timer += Time.deltaTime;
            float scaleLerp = Mathf.Lerp(0, 1, timer / shrinkDuration);
            obj.transform.localScale = originalScale * scaleLerp;
            instanceMaterial.color = Color.Lerp(Color.red, Color.white, timer / shrinkDuration);  // Resetting color to original (white)
            yield return null;
        }

        // Ensure the object returns to its original state
        obj.transform.localScale = originalScale;
        instanceMaterial.color = Color.white;
    }
}

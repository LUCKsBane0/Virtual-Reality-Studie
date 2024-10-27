using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TileGroup
{
    public string groupName;        // Optional name for the group
    public List<GameObject> tiles;  // List of tiles in the group
}

public class TileDisappearController : MonoBehaviour
{
    [Header("Tile Groups")]
    public List<TileGroup> tileGroups = new List<TileGroup>();  // List of tile groups

    [Header("Timing Settings")]
    public float disappearDuration = 2.0f;  // Time for the tile to disappear
    public float reappearDelay = 2.0f;      // Delay before the tile reappears
    public float groupInterval = 5.0f;      // Interval at which the groups cycle
    public float highlightDuration = 2.0f;  // Time the tile stays highlighted before disappearing

    [Header("Material Settings")]
    public Material baseMaterial;   // Base material (normal state)
    public Material markedMaterial; // Marked material (highlighted before disappearing)

    private void Start()
    {
        // Start the disappearance process for all groups at the same time
        foreach (TileGroup group in tileGroups)
        {
            StartCoroutine(HandleGroupTiles(group));
        }
    }

    // Coroutine to handle a group of tiles disappearing randomly
    private IEnumerator HandleGroupTiles(TileGroup tileGroup)
    {
        while (true)
        {
            // Randomly choose a tile from the group
            GameObject randomTile = tileGroup.tiles[Random.Range(0, tileGroup.tiles.Count)];

            // Start disappearing and reappearing the tile
            StartCoroutine(DisappearTile(randomTile));

            // Wait before the next tile disappears within the same group
            yield return new WaitForSeconds(groupInterval);
        }
    }

    // Coroutine to handle the tile staying highlighted, shrinking, disappearing, and reappearing
    private IEnumerator DisappearTile(GameObject tile)
    {
        // Get the Renderer component and set the marked material
        Renderer tileRenderer = tile.GetComponent<Renderer>();
        tileRenderer.material = markedMaterial;

        // Stay highlighted for a while before shrinking
        yield return new WaitForSeconds(highlightDuration);

        // Shrink the tile over time (center it)
        Vector3 originalScale = tile.transform.localScale;
        Vector3 originalPosition = tile.transform.position;
        Vector3 centerPosition = tileRenderer.bounds.center;  // Get the center of the tile

        float shrinkDuration = disappearDuration / 2;
        float elapsedTime = 0f;

        // Shrinking phase
        while (elapsedTime < shrinkDuration)
        {
            float scale = Mathf.Lerp(1, 0, elapsedTime / shrinkDuration);
            tile.transform.localScale = originalScale * scale;

            // Move to ensure the tile shrinks towards its center
            tile.transform.position = Vector3.Lerp(originalPosition, centerPosition, elapsedTime / shrinkDuration);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        tile.transform.localScale = Vector3.zero;

        // Wait for the reappear delay
        yield return new WaitForSeconds(reappearDelay);

        // Set the base material back before growing the tile again
        tileRenderer.material = baseMaterial;

        // Growing phase
        elapsedTime = 0f;
        while (elapsedTime < shrinkDuration)
        {
            float scale = Mathf.Lerp(0, 1, elapsedTime / shrinkDuration);
            tile.transform.localScale = originalScale * scale;

            // Move back to the original position to expand from the center
            tile.transform.position = Vector3.Lerp(centerPosition, originalPosition, elapsedTime / shrinkDuration);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        tile.transform.localScale = originalScale;
    }
}

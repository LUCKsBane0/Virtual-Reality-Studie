using UnityEngine;

public class RandomRotation : MonoBehaviour
{
    [Header("Rotation Speed Settings")]
    [Tooltip("Minimum rotation speed (degrees per second).")]
    public float minRotationSpeed = 0.1f;

    [Tooltip("Maximum rotation speed (degrees per second).")]
    public float maxRotationSpeed = 0.5f;

    private Vector3 randomRotationSpeeds;  // Random rotation speed for each axis

    private void Start()
    {
        // Generate random rotation speeds within the given range for each axis
        randomRotationSpeeds = new Vector3(
            Random.Range(minRotationSpeed, maxRotationSpeed),
            Random.Range(minRotationSpeed, maxRotationSpeed),
            Random.Range(minRotationSpeed, maxRotationSpeed)
        );
    }

    private void Update()
    {
        // Smoothly rotate the object on all axes
        transform.Rotate(randomRotationSpeeds * Time.deltaTime);
    }
}

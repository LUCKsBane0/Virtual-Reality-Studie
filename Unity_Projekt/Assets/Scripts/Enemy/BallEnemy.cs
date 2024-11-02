using UnityEngine;

public class BallEnemy : MonoBehaviour
{
    [Header("Movement Settings")]
    public float speed = 5f;                  // Movement speed
    public float trackingDistance = 20f;      // Distance at which the ball starts tracking the player
    public float stopTrackingDistance = 3f;   // Distance at which the ball stops tracking the player

    private Transform player;                 // Reference to the player's transform
    private bool hasTrackedPlayer = false;    // Flag to check if the ball has tracked the player
    private Vector3 lastDirection;            // Last direction the ball was moving in

    [Header("Idle Settings")]
    public float idleAmplitude = 0.5f;        // Amplitude of the hovering motion
    public float idleFrequency = 1f;          // Frequency of the hovering motion
    private Vector3 initialPosition;

    private void Start()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("HitTarget").transform;
        }
        initialPosition = transform.position;
    }

    private void Update()
    {
        if (hasTrackedPlayer)
        {
            // Move in the last known direction
            transform.position += lastDirection * speed * Time.deltaTime;
        }
        else
        {
            TrackOrIdle();
        }
    }

    private void TrackOrIdle()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer < trackingDistance && distanceToPlayer > stopTrackingDistance)
        {
            // Start tracking the player if within range and hasn't been close enough to stop tracking
            Vector3 directionToPlayer = (player.position - transform.position).normalized;
            lastDirection = directionToPlayer;
            transform.position += lastDirection * speed * Time.deltaTime;
        }
        else if (distanceToPlayer <= stopTrackingDistance)
        {
            // Once within the stop tracking distance, lock the last direction and stop tracking the player
            hasTrackedPlayer = true;
        }
        else
        {
            // Idle when not tracking the player
            Hover();
        }
    }

    private void Hover()
    {
        // Simple idle hovering effect using sine wave for smooth up and down motion
        float newY = initialPosition.y + Mathf.Sin(Time.time * idleFrequency) * idleAmplitude;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }

    private void OnDrawGizmosSelected()
    {
        // Draw tracking distance and stop tracking distance in the editor
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, trackingDistance);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, stopTrackingDistance);
    }
}

using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("Common Settings")]
    public float speed = 3f;        // Common speed for all enemies
    public Transform player;        // Reference to the player

    protected Vector3 direction;    // Movement direction
    protected bool isTrackingPlayer = false;

    protected virtual void Start()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    protected virtual void Update()
    {
        // Movement logic will be implemented in the child classes.
    }
}

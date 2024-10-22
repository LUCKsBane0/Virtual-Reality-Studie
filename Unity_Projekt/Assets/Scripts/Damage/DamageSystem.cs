using UnityEngine;

public class DamageSystem : MonoBehaviour
{
    [Header("Damage Settings")]
    [Tooltip("The amount of damage this object deals to the player.")]
    public int damageAmount = 10;  // How much damage this object deals

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision);
        // Check if the object hit has a HealthSystem component (i.e., it's the player)
        HealthSystem playerHealth = collision.gameObject.GetComponent<HealthSystem>();

        if (playerHealth != null)
        {
            playerHealth.TakeDamage(damageAmount);  // Apply damage to the player
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        HealthSystem playerHealth = other.gameObject.GetComponent<HealthSystem>();
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(damageAmount);  // Apply damage to the player
        }
    }
}

using UnityEngine;
using UnityEngine.UI;

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

    private void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthBar();  // Initialize the health bar
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
        //TP to early button
        // Implement death handling here (e.g., respawn or game over logic)
    }
}

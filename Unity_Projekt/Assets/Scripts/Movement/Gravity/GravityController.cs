using UnityEngine;

public class GravityController : MonoBehaviour
{
    [Header("Gravity Settings")]
    [Tooltip("The CharacterController component of the player or XR Origin")]
    public CharacterController characterController;

    [Tooltip("Gravity force to apply. Default is Earth's gravity (-9.81).")]
    public float gravity = -9.81f;

    [Tooltip("Toggle to enable or disable gravity")]
    public bool enableGravity = true;

    private Vector3 velocity;

    // Update is called once per frame
    void Update()
    {
        if (enableGravity)
        {
            ApplyGravity();
        }
    }


    /// <summary>
    /// Applies gravity to the character, adjusting velocity based on time and grounded state.
    /// </summary>
    
    private void ApplyGravity()
    {
        // If grounded, reset gravity velocity
        if (characterController.isGrounded && velocity.y < 0)
        {
            velocity.y = 0f;
        }

        // Apply gravity
        velocity.y += gravity * Time.deltaTime;

        // Move the character controller according to gravity
        characterController.Move(velocity * Time.deltaTime);
    }

    /// <summary>
    /// Enables gravity application.
    /// </summary>
    public void EnableGravity()
    {
        enableGravity = true;
    }

    /// <summary>
    /// Disables gravity application.
    /// </summary>
    public void DisableGravity()
    {
        enableGravity = false;
    }
}

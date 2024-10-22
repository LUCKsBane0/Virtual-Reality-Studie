using UnityEngine;

public class ConeEnemy : EnemyController
{
    [Header("Hover Settings")]
    public Transform pointA;    // First hover point
    public Transform pointB;    // Second hover point
    public float hoverDuration = 3f;   // Time taken to move between points

    private float timer;

    protected override void Start()
    {
        base.Start();
        timer = 0f;
    }

    protected override void Update()
    {
        HoverBetweenPoints();
    }

    private void HoverBetweenPoints()
    {
        timer += Time.deltaTime / hoverDuration;
        float t = Mathf.PingPong(timer, 1f); // Loops between 0 and 1
        t = EaseInOutCubic(t); // Apply the cubic easing

        // Interpolate between the two points
        transform.position = Vector3.Lerp(pointA.position, pointB.position, t);
    }

    // Easing function for smooth cubic movement
    private float EaseInOutCubic(float t)
    {
        return t < 0.5f ? 4 * t * t * t : 1 - Mathf.Pow(-2 * t + 2, 3) / 2;
    }
}

using UnityEngine;

public class LookatTrigger : Trigger
{
    [Header("Raycast Settings")]
    public string targetObjectName = "LookAroundTargetLeft"; // The name of the GameObject to hit
    public float raycastDistance = 20f;                     // Max distance for the raycast
    public LayerMask raycastLayerMask;                      // LayerMask for filtering raycast hits

    private Camera playerCamera;
    private bool isTargetHit;

    private void Start()
    {
        playerCamera = Camera.main;
    }

    private void Update()
    {
        PerformRaycast();
    }

    private void PerformRaycast()
    {
        RaycastHit hit;
        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);

        if (Physics.Raycast(ray, out hit, raycastDistance, raycastLayerMask))
        {
            // Check if the hit object is the specified target
            if (hit.collider.gameObject.name == targetObjectName && !isTargetHit)
            {
                isTargetHit = true;
                TriggerTargetMet();  // Invoke the base class method to trigger the event
                Debug.Log("Target hit: " + targetObjectName);
            }
        }
        else
        {
            isTargetHit = false;
        }
    }
}

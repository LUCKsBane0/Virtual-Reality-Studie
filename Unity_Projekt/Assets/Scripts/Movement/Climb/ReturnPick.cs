using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class PickaxeReturn : MonoBehaviour
{
    public Transform pickaxeAnchor;  // Assign the anchor (e.g., right side position)
    public float returnSpeed = 3f;   // Speed for returning the pickaxe
    private XRGrabInteractable grabInteractable;
    private bool isReturning = false;

    void Start()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
        grabInteractable.selectExited.AddListener(OnRelease);
    }

    private void OnRelease(SelectExitEventArgs arg)
    {
        StartCoroutine(ReturnToAnchor());
    }

    private IEnumerator ReturnToAnchor()
    {
        isReturning = true;
        while (Vector3.Distance(transform.position, pickaxeAnchor.position) > 0.01f)
        {
            transform.position = Vector3.Lerp(transform.position, pickaxeAnchor.position, Time.deltaTime * returnSpeed);
            transform.rotation = Quaternion.Lerp(transform.rotation, pickaxeAnchor.rotation, Time.deltaTime * returnSpeed);
            yield return null;
        }
        transform.position = pickaxeAnchor.position;
        transform.rotation = pickaxeAnchor.rotation;
        isReturning = false;
    }

    void OnDestroy()
    {
        grabInteractable.selectExited.RemoveListener(OnRelease);
    }
}

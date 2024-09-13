using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardChest : MonoBehaviour
{
    public float roofOpenSpeed = 50f; // Speed at which the roof opens
    public float swordRiseSpeed = 0.1f; // Speed at which the sword rises
    public float swordRotationSpeed = 50f; // Speed at which the RewardSword will rotate on Y-axis

    private Transform chestRoof;
    private Transform rewardSword;
    private bool isOpening = false; // To track if the chest is opened
    private bool swordRisen = false; // To track if the sword has finished rising

    private void Start()
    {
        // Find the children with the specific names
        chestRoof = transform.Find("ChestRoof");
        rewardSword = transform.Find("RewardSword");

        // Make sure the child objects were found
        if (chestRoof == null)
        {
            Debug.LogError("ChestRoof not found as a child of " + gameObject.name);
        }

        if (rewardSword == null)
        {
            Debug.LogError("RewardSword not found as a child of " + gameObject.name);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "XR Origin (XR Rig)" && !isOpening)
        {
            isOpening = true;
            StartCoroutine(OpenChest());
        }
    }

    // Coroutine to open the chest slowly
    IEnumerator OpenChest()
    {
        // Rotate the ChestRoof to -90 degrees on the X-axis slowly
        float currentAngle = chestRoof.localEulerAngles.x;
        float targetAngle = -90f;

        // While current angle has not yet reached target angle
        while (Mathf.Abs(Mathf.DeltaAngle(currentAngle, targetAngle)) > 0.1f)
        {
            // Smoothly rotate the roof towards -90 degrees on the X-axis
            currentAngle = Mathf.MoveTowardsAngle(currentAngle, targetAngle, roofOpenSpeed * Time.deltaTime);
            chestRoof.localRotation = Quaternion.Euler(currentAngle, 0f, 0f);
            yield return null;
        }

        // After the roof opens, start raising the sword
        StartCoroutine(RaiseSword());
    }

    // Coroutine to raise the sword slowly
    IEnumerator RaiseSword()
    {
        float startHeight = rewardSword.localPosition.y;
        float targetHeight = startHeight + 0.3f; // Target Y position after rising

        while (rewardSword.localPosition.y < targetHeight)
        {
            // Smoothly raise the sword by adding to the Y position
            rewardSword.localPosition += new Vector3(0f, swordRiseSpeed * Time.deltaTime, 0f);
            yield return null;
        }

        // Mark the sword as fully risen so it can start rotating
        swordRisen = true;
    }

    private void Update()
    {
        // Rotate the RewardSword on Y-axis infinitely if the sword has fully risen
        if (swordRisen && rewardSword != null)
        {
            rewardSword.Rotate(0f, swordRotationSpeed * Time.deltaTime, 0f);
        }
    }
}

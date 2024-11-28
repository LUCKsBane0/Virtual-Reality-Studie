using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class MetaQuestNetworkTester : MonoBehaviour
{
    public string serverUrl = "http://192.168.178.43:7331/ping"; // Replace with your server's local IP and endpoint

    void Start()
    {
        Debug.Log("Starting Network Test");
        // Start the network test on application start
        StartCoroutine(TestNetworkConnection());
        StartCoroutine(TestNetworkConnection());
        StartCoroutine(TestNetworkConnection());
        StartCoroutine(TestNetworkConnection());
        StartCoroutine(TestNetworkConnection());
        StartCoroutine(TestNetworkConnection());
        StartCoroutine(TestNetworkConnection());
    }

    IEnumerator TestNetworkConnection()
    {
        Debug.Log($"Testing network connection to: {serverUrl}");

        using (UnityWebRequest request = UnityWebRequest.Get(serverUrl))
        {
            request.timeout = 10; // Set a timeout for the request
            yield return request.SendWebRequest(); // Send the request and wait for a response

            if (request.result == UnityWebRequest.Result.Success)
            {
                Debug.Log($"Connection successful: {request.downloadHandler.text}");
            }
            else
            {
                Debug.LogError($"Connection failed: {request.error}");
            }
        }
    }
}

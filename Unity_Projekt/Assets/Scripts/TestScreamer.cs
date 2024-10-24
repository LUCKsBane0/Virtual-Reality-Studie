using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScreamer : MonoBehaviour
{
    // Start is called before the first frame update
    public string Message;

    public void Scream()
    {
        Debug.Log(Message);
    }
    public void Scream(string message)
    {
        Debug.Log(message);
    }
}

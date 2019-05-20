using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    float currentTime = 0;
    private void Update()
    {
        currentTime += Time.deltaTime;
        Debug.Log("Seconds that passed: " + Mathf.Floor(currentTime));
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadarDetection : MonoBehaviour
{
    public bool playerDetected = false;
    private void Start()
    {
        GetComponent<SphereCollider>().radius = GetComponentInParent<AIController>().hearingDistance;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerDetected = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerDetected = false;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScanWayPoints : MonoBehaviour
{
    public static ScanWayPoints instance;

    public List<Transform> wayPoints;

    private void Start()
    {
        wayPoints = new List<Transform>();
        instance = this;
    }
    public void Scan()
    {
        GameObject[] points = GameObject.FindGameObjectsWithTag("Waypoint");
        for (int i = 0; i < points.Length; i++)
        {
            wayPoints.Add(points[i].transform);
        }
    }
}

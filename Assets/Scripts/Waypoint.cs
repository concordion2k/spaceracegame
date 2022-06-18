using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    public GameObject nextWaypoint;

    public delegate void HandleWaypointEntered(GameObject next);
    public event HandleWaypointEntered WaypointEntered;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            WaypointEntered(nextWaypoint);
    }
}

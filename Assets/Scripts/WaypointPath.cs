using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointPath : MonoBehaviour
{
    public GameObject currWaypoint;

    public delegate void HandleNextWaypoint(string next);
    public event HandleNextWaypoint WaypointChanged;

    private void Start()
    {
        currWaypoint = GameObject.FindGameObjectWithTag("Player").GetComponent<Spaceship>().currWaypoint;
        foreach (Transform go in gameObject.GetComponentInChildren<Transform>())
        {
            go.GetComponent<Waypoint>().WaypointEntered += OnWaypointEnter;
        }
    }

    private void OnWaypointEnter(GameObject wp)
    {
        currWaypoint = wp;
        WaypointChanged(wp.name);
    }
}

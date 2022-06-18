using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpdateWaypointInfo : MonoBehaviour
{

    private GameObject waypointText;

    // Start is called before the first frame update
    void Start()
    {
        Transform[] children = gameObject.GetComponentsInChildren<Transform>();

        foreach (Transform t in children)
        {
            if (t.name == "WaypointText")
                waypointText = t.gameObject;
        }

        waypointText.GetComponent<TextMeshProUGUI>().text = GameObject.FindGameObjectWithTag("Player").GetComponent<Spaceship>().currWaypoint.name;

        GameObject.Find("Waypoints").GetComponent<WaypointPath>().WaypointChanged += OnWaypointChanged;
    }

    private void OnWaypointChanged(string name)
    {
        waypointText.GetComponent<TextMeshProUGUI>().text = name;
    }
}

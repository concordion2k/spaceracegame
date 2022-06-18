using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayWaypoint : MonoBehaviour
{

    GameObject currentWaypoint;
    public GameObject marker;
    Canvas canvas;
    Camera mCamera;
    // Start is called before the first frame update
    void Start()
    {
        currentWaypoint = GameObject.FindGameObjectWithTag("Player").GetComponent<Spaceship>().currWaypoint;
        GameObject.Find("Waypoints").GetComponent<WaypointPath>().WaypointChanged += OnWaypointChanged;
        canvas = GetComponent<Canvas>();
        mCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

    void OnWaypointChanged(string name)
    {
        currentWaypoint = GameObject.Find(name);
    }

    // Update is called once per frame
    void Update()
    {
        DrawIndicator();
    }

    void DrawIndicator()
    {
        Vector3 wpScreenPos = mCamera.WorldToScreenPoint(currentWaypoint.transform.position);
        if (wpScreenPos.x > 0 || wpScreenPos.y > 0)
        {
            // This solution was found here:
            // https://forum.unity.com/threads/create-ui-health-markers-like-in-world-of-tanks.432935/

            // Offset position above object bbox (in world space)
            float offsetPosY = currentWaypoint.transform.position.y + 15f;

            // Final position of marker above GO in world space
            Vector3 offsetPos = new Vector3(currentWaypoint.transform.position.x, offsetPosY, currentWaypoint.transform.position.z);

            // Calculate *screen* position (note, not a canvas/recttransform position)
            Vector2 canvasPos;
            Vector2 screenPoint = mCamera.WorldToScreenPoint(offsetPos);

            // Convert screen position to Canvas / RectTransform space <- leave camera null if Screen Space Overlay
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.GetComponent<RectTransform>(), screenPoint, null, out canvasPos);

            // Set
            marker.GetComponent<RectTransform>().localPosition = canvasPos;
        }
    }
}

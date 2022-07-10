using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalCamera : MonoBehaviour
{
    public GameObject PlayerReference;
    // Causes the portal camera to adjust orientation to match the players perspective
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        this.transform.rotation = PlayerReference.transform.rotation;
    }
}

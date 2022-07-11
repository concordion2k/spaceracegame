using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fracture : MonoBehaviour
{
    [Tooltip("\"Fractured\" is the object that this will break into")]
    public GameObject fractured;

    public void FractureObject()
    {
        Instantiate(fractured, transform.position, transform.rotation); //Spawn in the broken version
        //fractured.GetComponent<Rigidbody>().AddForce(transform.up * 1); caused a crash? or maye something right before this change
        Destroy(gameObject); //Destroy the object to stop it getting in the way
    }
}

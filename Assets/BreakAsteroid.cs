using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakAsteroid : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        this.GetComponent<Fracture>().FractureObject();
    }
}

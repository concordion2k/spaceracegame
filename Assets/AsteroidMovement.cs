using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidMovement : MonoBehaviour
{
    Vector3 m_NewForce;
    // Start is called before the first frame update
    void Start()
    {
        m_NewForce = new Vector3(-5.0f, 1.0f, 0.0f);
        //GetComponent<Rigidbody>().velocity = Random.onUnitSphere * 10;
        GetComponent<Rigidbody>().AddForce(m_NewForce, ForceMode.Impulse);
    }

}

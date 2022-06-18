using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class FindShipTarget : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        GetComponent<CinemachineVirtualCamera>().Follow = GameObject.FindWithTag("Player").gameObject.transform;
    }
}

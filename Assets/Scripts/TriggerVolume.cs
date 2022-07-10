using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerVolume : MonoBehaviour
{
    public UnityEvent onEnterTrigger;

    private void OnTriggerEnter(Collider other)
    {
        onEnterTrigger.Invoke();
    }
}

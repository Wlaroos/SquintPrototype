using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerEvent : MonoBehaviour
{
    public UnityEvent _unityEvent;

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<FirstPersonController>() != null)
        {
            Debug.Log("Test");
            _unityEvent.Invoke();
        }
    }
}

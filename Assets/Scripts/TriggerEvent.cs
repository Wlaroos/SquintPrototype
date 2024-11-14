using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerEvent : MonoBehaviour
{
    public UnityEvent _unityEvent;
    private bool _doOnce;

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<FirstPersonController>() != null && !_doOnce)
        {
            //Debug.Log("Test");
            _unityEvent.Invoke();
            _doOnce = true;
        }
    }
}

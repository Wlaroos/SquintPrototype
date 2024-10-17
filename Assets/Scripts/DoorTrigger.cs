using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    [SerializeField] private GameObject _doorLeft;
    [SerializeField] private GameObject _doorRight;
    
    private Vector3 _doorLeftOpenPos;
    private Vector3 _doorRightOpenPos;
    
    private BoxCollider _bc;
    private bool _isPlayerInside;

    private void Awake()
    {
        _bc = GetComponent<BoxCollider>();
        _doorLeftOpenPos = new Vector3(25.775f, -1, 5.654f);
        _doorRightOpenPos = new Vector3(30f, -1, 1.44f);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && _isPlayerInside)
        {
            StartCoroutine(DoorOpen());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            _isPlayerInside = true;
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            _isPlayerInside = false;
        }
    }

    private IEnumerator DoorOpen()
    {
        while(Vector3.Distance(_doorLeft.transform.localPosition, _doorLeftOpenPos) > 0.1f)
        {
            _doorLeft.transform.localPosition = Vector3.Lerp(_doorLeft.transform.localPosition, _doorLeftOpenPos, Time.deltaTime * 2);
            _doorRight.transform.localPosition = Vector3.Lerp(_doorRight.transform.localPosition, _doorRightOpenPos, Time.deltaTime * 2);
            yield return null;
        }
    }
}

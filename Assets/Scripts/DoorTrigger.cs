using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : InteractableBase
{
    [SerializeField] private GameObject _doorLeft;
    [SerializeField] private GameObject _doorRight;
    
    [SerializeField] private AudioClip _doorOpenSFX;

    private Vector3 _doorLeftOpenPos;
    private Vector3 _doorLeftClosePos;
    private Vector3 _doorRightOpenPos;
    private Vector3 _doorRightClosePos;

    private void Awake()
    {
        _doorLeftClosePos = _doorLeft.transform.localPosition;
        _doorRightClosePos = _doorRight.transform.localPosition;
        _doorLeftOpenPos = new Vector3(25.775f, -1, 5.654f);
        _doorRightOpenPos = new Vector3(30f, -1, 1.44f);
    }

    public override void OnInteract()
    {
        base.OnInteract();

        OpenDoor();
    }

    private IEnumerator DoorOpen()
    {

        while (Vector3.Distance(_doorLeft.transform.localPosition, _doorLeftOpenPos) > 0.01f)
        {
            _doorLeft.transform.localPosition = Vector3.Lerp(_doorLeft.transform.localPosition, _doorLeftOpenPos, Time.deltaTime * 2);
            _doorRight.transform.localPosition = Vector3.Lerp(_doorRight.transform.localPosition, _doorRightOpenPos, Time.deltaTime * 2);
            yield return null;
        }

        //Destroy(this);
    }

    private IEnumerator DoorClose()
    {
        while (Vector3.Distance(_doorLeft.transform.localPosition, _doorLeftClosePos) > 0.01f)
        {
            _doorLeft.transform.localPosition = Vector3.Lerp(_doorLeft.transform.localPosition, _doorLeftClosePos, Time.deltaTime * 2);
            _doorRight.transform.localPosition = Vector3.Lerp(_doorRight.transform.localPosition, _doorRightClosePos, Time.deltaTime * 2);
            yield return null;
        }

        //Destroy(this);
    }


    public void OpenDoor()
    {
        StopAllCoroutines();
        StartCoroutine(DoorOpen());
        AudioHelper.PlayClip2D(_doorOpenSFX, 0.5f);
    }
    public void CloseDoor()
    {
        StopAllCoroutines();
        StartCoroutine(DoorClose());
        AudioHelper.PlayClip2D(_doorOpenSFX, 0.5f);
    }
}

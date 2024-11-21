using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfCheckoutDoorTrigger : InteractableBase
{
    [SerializeField] private GameObject _doorLeft;
    [SerializeField] private GameObject _doorRight;
    [SerializeField] private GameObject _groceryBagPrefab;

    private Vector3 _doorLeftOpenPos;
    private Vector3 _doorLeftClosePos;
    private Vector3 _doorRightOpenPos;
    private Vector3 _doorRightClosePos;

    private bool _isActive;
    private string _tooltip;

    private void Awake()
    {
        _doorLeftClosePos = _doorLeft.transform.localPosition;
        _doorRightClosePos = _doorRight.transform.localPosition;
        _doorLeftOpenPos = new Vector3(25.775f, -1, 5.654f);
        _doorRightOpenPos = new Vector3(30f, -1, 1.44f);

        this.enabled = false;
        _tooltip = base.TooltipMessage;
        base.SetTooltip("");
    }

    public override void OnInteract()
    {
        if (_isActive)
        {
            base.OnInteract();

            StopAllCoroutines();
            StartCoroutine(DoorOpen());

            Instantiate(_groceryBagPrefab, new Vector3(transform.position.x + 0.3f, transform.position.y + 0.5f, transform.position.z + 0.2f), Quaternion.Euler(0, 90, 0));

            SelfCheckoutDoorTrigger[] array = FindObjectsOfType<SelfCheckoutDoorTrigger>();
            foreach (var item in array)
            {
                Destroy(item);
            }
        }
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
    }
    public void CloseDoor()
    {
        StopAllCoroutines();
        StartCoroutine(DoorClose());
    }

    public void Activate()
    {
        _isActive = true;
        base.SetTooltip(_tooltip);
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfCheckoutDoorTrigger : InteractableBase
{
    [SerializeField] private GameObject _doorLeft;
    [SerializeField] private GameObject _doorRight;
    [SerializeField] private GameObject _groceryBagPrefab;
    
    [SerializeField] private AudioClip _doorOpenSFX;

    private SquintUIPanel _squintUIRef;

    private ExitDoor[] _exitArray;
    private SelfCheckoutDoorTrigger[] _doorArray;

    private Vector3 _doorLeftOpenPos;
    private Vector3 _doorLeftClosePos;
    private Vector3 _doorRightOpenPos;
    private Vector3 _doorRightClosePos;

    private bool _isActive;
    private string _tooltip;

    private void Awake()
    {
        _squintUIRef = FindObjectOfType<SquintUIPanel>();
        _exitArray = FindObjectsOfType<ExitDoor>();
        _doorArray = FindObjectsOfType<SelfCheckoutDoorTrigger>();

        _doorLeftClosePos = _doorLeft.transform.localPosition;
        _doorRightClosePos = _doorRight.transform.localPosition;
        _doorLeftOpenPos = new Vector3(25.775f, -1, 5.654f);
        _doorRightOpenPos = new Vector3(30f, -1, 1.44f);

        this.enabled = false;
        _tooltip = base.TooltipMessage;
        base.SetTooltip("");

        foreach (var item in _exitArray)
        {
            item.gameObject.SetActive(false);
        }
    }

    public override void OnInteract()
    {
        if (_isActive)
        {
            base.OnInteract();

            OpenDoor();

            _squintUIRef.SetObjectiveText("Exit the store");

            Instantiate(_groceryBagPrefab, new Vector3(transform.position.x + 0.3f, transform.position.y + 0.5f, transform.position.z + 0.2f), Quaternion.Euler(0, 90, 0));

            foreach (var item in _exitArray)
            {
                item.gameObject.SetActive(true);
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
        
        foreach (var item in _doorArray)
        {
            Destroy(item);
        }
    }
    
    public void OpenDoor()
    {
        StopAllCoroutines();
        StartCoroutine(DoorOpen());
        AudioHelper.PlayClip2D(_doorOpenSFX, 0.5f);
    }

    public void Activate()
    {
        _isActive = true;
        base.SetTooltip(_tooltip);
    }
}

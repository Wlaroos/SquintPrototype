using System.Collections; 
using System.Collections.Generic;
using UnityEngine;

public class PickupInteractable : InteractableBase
{

    private PickUp _pickUpPlayerRef;
    private Rigidbody _rb;
    private GameObject _holder;
    private bool _isColliding;

    private void Awake()
    {
        _pickUpPlayerRef = GameObject.FindObjectOfType<PickUp>();
        _holder = _pickUpPlayerRef._holder;
        _rb = GetComponent<Rigidbody>();

        if (base.TooltipMessage != null)
        {
            name = base.TooltipMessage;
        }
    }

    public override void OnInteract()
    {
        base.OnInteract();

        if (!_pickUpPlayerRef._hasItem)
        {
            //Debug.Log("PICKUP");

            _rb.constraints = RigidbodyConstraints.FreezeRotation;
            _rb.useGravity = false;

            transform.parent = _holder.transform; // Makes the object become a child of the parent so that it moves with the hands

            transform.position = _holder.transform.position; // Sets the position of the object to your hand position

            _pickUpPlayerRef._hasItem = true;
            _pickUpPlayerRef._canPickup = false;

            _holder.transform.localPosition = new Vector3(0, 0, 1);
        }
        else
        {
            _rb.constraints = RigidbodyConstraints.None;
            _rb.useGravity = true;

            transform.parent = null; // Make the object no longer be a child of the hands

            _pickUpPlayerRef._hasItem = false;
            _pickUpPlayerRef._canPickup = true;
        }
    }

    private void FixedUpdate()
    {
        if (_pickUpPlayerRef._hasItem && transform.parent != null)
        {
            _isColliding = Physics.CheckSphere(_rb.transform.position, _rb.GetComponent<Collider>().bounds.extents.magnitude, LayerMask.GetMask("Ground"));
            _rb.velocity = Vector3.zero;
        }

        if (!_isColliding && _pickUpPlayerRef._hasItem && transform.parent != null)
        {
            _rb.transform.localPosition = Vector3.Slerp(_rb.transform.localPosition, Vector3.zero, Time.deltaTime * 10);
        }
    }
}

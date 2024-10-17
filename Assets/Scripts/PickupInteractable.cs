using System.Collections; 
using System.Collections.Generic;
using UnityEngine;

namespace VHS
{    
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
        }

        public override void OnInteract()
        {
            base.OnInteract();

            if (!_pickUpPlayerRef._hasItem) // if you enter the collider of the object
            {
                //Debug.Log("PICKUP");

                _rb.constraints = RigidbodyConstraints.FreezeRotation;
                _rb.useGravity = false;

                transform.position = _holder.transform.position; // sets the position of the object to your hand position

                transform.parent = _holder.transform; //makes the object become a child of the parent so that it moves with the hands

                _pickUpPlayerRef._hasItem = true;
                _pickUpPlayerRef._canPickup = false;

                _holder.transform.localPosition = new Vector3(0, 0, 1);
            }
            else if (_pickUpPlayerRef._hasItem)
            {
                _rb.constraints = RigidbodyConstraints.None;
                _rb.useGravity = true;

                transform.parent = null; // make the object no be a child of the hands

                _pickUpPlayerRef._hasItem = false;
            }
        }

        private void FixedUpdate()
        {
            if (_pickUpPlayerRef._hasItem)
            {
                _isColliding = Physics.CheckSphere(_rb.transform.position, _rb.GetComponent<Collider>().bounds.extents.magnitude, LayerMask.GetMask("Ground"));
            }

            if (_pickUpPlayerRef._hasItem)
            {
                _rb.velocity = Vector3.zero;
            }

            if (!_isColliding && _pickUpPlayerRef._hasItem)
            {
                _rb.transform.localPosition = Vector3.Slerp(_rb.transform.localPosition, Vector3.zero, Time.deltaTime * 10);
            }
        }
    }
}

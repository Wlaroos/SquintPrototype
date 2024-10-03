using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    [SerializeField] private GameObject _holder; //reference to your hands/the position where you want your object to go
    [SerializeField] private float _minInspectDist = 1;
    [SerializeField] private float _maxInspectDist = 4;

    private bool _canPickup; //a bool to see if you can or cant pick up the item
    private bool _hasItem; // a bool to see if you have an item in your hand
    private bool _isColliding;
    private GameObject _objectIwantToPickUp; // the gameobject onwhich you collided with
    private Rigidbody _rb;
    private float _inspectDistance = 2f;

    private float maxDist = 1;
    private Vector3 offsetHeight;
    private Vector3 spherePos;

    // Start is called before the first frame update
    void Awake()
    {
        _canPickup = false;    //setting both to false
        _hasItem = false;
    }

    // Update is called once per frame
    void Update()
    {
        MouseScroll();
        PickupItem();
        DropItem();

        if (_hasItem)
        {
            _rb.velocity = Vector3.zero;
        }

        if(!_isColliding && _hasItem)
        {
            _rb.transform.localPosition = Vector3.Slerp(_rb.transform.localPosition, Vector3.zero, Time.deltaTime * 5);
        }
    }

    private void OnTriggerEnter(Collider other) // to see when the player enters the collider
    {
        if (other.gameObject.tag == "PickUp") //on the object you want to pick up set the tag to be anything, in this case "object"
        {
            Debug.Log("ENTER");

            _rb = other.GetComponentInParent<Rigidbody>();

            _canPickup = true;  //set the pick up bool to true

            _objectIwantToPickUp = other.transform.parent.gameObject; //set the gameobject you collided with to one you can reference
        }
    }
    private void OnTriggerExit(Collider other)
    {
        Debug.Log("EXIT");
        _canPickup = false; //when you leave the collider set the canpickup bool to false
    }


    private void FixedUpdate()
    {
        if (_hasItem)
        {
            _isColliding = Physics.CheckSphere(_rb.transform.position, _rb.GetComponent<BoxCollider>().size.x / 2, LayerMask.GetMask("Ground"));
        }
    }

    private void OnDrawGizmos()
    {
        if (_hasItem)
        {
            Gizmos.DrawWireSphere(_rb.transform.position, _rb.GetComponent<BoxCollider>().size.x / 2);
        }
    }

    private void PickupItem()
    {
        if (_canPickup == true) // if you enter thecollider of the objecct
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("PICKUP");

                //_rb.constraints = RigidbodyConstraints.FreezePosition;
                _rb.useGravity = false;

                _objectIwantToPickUp.transform.position = _holder.transform.position; // sets the position of the object to your hand position

                _objectIwantToPickUp.transform.parent = _holder.transform; //makes the object become a child of the parent so that it moves with the hands

                _hasItem = true;

                _inspectDistance = 2f;
            }
        }
    }

    private void DropItem()
    {
        if (Input.GetKeyDown(KeyCode.Q) && _hasItem == true) // if you have an item press key to remove the object
        {
            Debug.Log("DROP");

            //_rb.constraints = RigidbodyConstraints.None;
            _rb.useGravity = true;

            _objectIwantToPickUp.transform.parent = null; // make the object no be a child of the hands

            _hasItem = false;
        }
    }

    private void MouseScroll()
    {
        if (Input.mouseScrollDelta.magnitude > 0)
        {
            _inspectDistance += Input.mouseScrollDelta.y * 0.2f;
            _inspectDistance = Mathf.Clamp(_inspectDistance, 1, 4);
            _holder.transform.localPosition = new Vector3(0, 0, _inspectDistance);
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    [SerializeField] public GameObject _holder; // Reference to your hands/the position where you want your object to go
    [SerializeField] private float _minInspectDist = 1;
    [SerializeField] private float _maxInspectDist = 4;

    public bool _canPickup; // A bool to see if you can or cant pick up the item
    public bool _hasItem; // A bool to see if you have an item in your hand
    private bool _isColliding;
    private GameObject _objectIWantToPickUp; // The gameobject on which you collided with
    private Rigidbody _rb;
    private float _inspectDistance = 2f;
    
    private Vector3 offsetHeight;
    private Vector3 spherePos;

    // Start is called before the first frame update
    void Awake()
    {
        _canPickup = false;    // Setting both to false
        _hasItem = false;
    }

    // Update is called once per frame
    void Update()
    {
        MouseScroll();
        //PickupItem();
        //  DropItem();
    }
    


    /*private void OnTriggerEnter(Collider other) // To see when the player enters the collider
    {
        if (other.gameObject.tag == "PickUp") // On the object you want to pick up set the tag to be anything, in this case "object"
        {
            //Debug.Log("ENTER");

            _rb = other.GetComponentInParent<Rigidbody>();

            _canPickup = true;  // Set the pick up bool to true

            _objectIWantToPickUp = other.transform.parent.gameObject; // Set the gameobject you collided with to one you can reference
        }
    }
    private void OnTriggerExit(Collider other)
    {
        //Debug.Log("EXIT");
        _canPickup = false; // When you leave the collider set the can pickup bool to false
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
        if (_canPickup && !_hasItem) // if you enter the collider of the object
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                //Debug.Log("PICKUP");

                _rb.constraints = RigidbodyConstraints.FreezeRotation;
                _rb.useGravity = false;

                _objectIWantToPickUp.transform.position = _holder.transform.position; // sets the position of the object to your hand position

                _objectIWantToPickUp.transform.parent = _holder.transform; //makes the object become a child of the parent so that it moves with the hands

                _hasItem = true;
                _canPickup = false;

                _holder.transform.localPosition = new Vector3(0, 0, 1);
            }
        }
    }

    private void DropItem()
    {
        if (Input.GetKeyDown(KeyCode.Q) && _hasItem) // if you have an item press key to remove the object
        {
            //Debug.Log("DROP");

            _rb.constraints = RigidbodyConstraints.None;
            _rb.useGravity = true;
            
            _objectIWantToPickUp.transform.parent = null; // make the object no be a child of the hands

            _hasItem = false;
        }
    }*/

    private void MouseScroll()
    {
        if (Input.mouseScrollDelta.magnitude > 0)
        {
            _inspectDistance += Input.mouseScrollDelta.y * 0.2f;
            _inspectDistance = Mathf.Clamp(_inspectDistance, _minInspectDist, _maxInspectDist);
            _holder.transform.localPosition = new Vector3(0, 0, _inspectDistance);
        }
    }
}
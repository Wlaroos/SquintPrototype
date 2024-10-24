using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<PickupInteractable>() != null)
        {
            Debug.Log("COLLECTED -- " + other.name);
            Destroy(other.gameObject);
        }
    }
}

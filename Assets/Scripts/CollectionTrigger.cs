using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionTrigger : MonoBehaviour
{
    private ItemManager _itemManager;

    private void Awake()
    {
        _itemManager = FindObjectOfType<ItemManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<PickupInteractable>() != null)
        {
            _itemManager.CollisionCheck(other);
        }
    }
}

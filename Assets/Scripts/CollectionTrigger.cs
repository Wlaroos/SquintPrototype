using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionTrigger : MonoBehaviour
{
    private ListItem[] _groceryItemList;
    private ItemManager _itemManager;

    private void Awake()
    {
        _groceryItemList = FindObjectsOfType<ListItem>();
        _itemManager = FindObjectOfType<ItemManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<PickupInteractable>() != null)
        {
            foreach (var item in _groceryItemList)
            {
                if(item.ItemName == other.name)
                {
                    item.Collected();
                    _itemManager.CollectedItem();
                    //Debug.Log("COLLECTED -- " + other.name);
                    Destroy(other.gameObject);

                    break;
                }
            }
        }
    }
}

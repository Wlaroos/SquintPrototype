using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionTrigger : MonoBehaviour
{
    private ListItem[] _groceryItemArray;
    private List<ListItem> _groceryItemList = new List<ListItem>();
    private ItemManager _itemManager;

    private void Awake()
    {
        _groceryItemArray = FindObjectsOfType<ListItem>();
        _itemManager = FindObjectOfType<ItemManager>();

        foreach (var item in _groceryItemArray)
        {
            _groceryItemList.Add(item);
        }
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

                    _groceryItemList.Remove(item);

                    //Debug.Log("COLLECTED -- " + other.name);

                    Destroy(other.gameObject);

                    break;
                }
            }
        }
    }
}

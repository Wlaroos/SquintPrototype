using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    private int _amountGathered;
    private DoorTrigger _doorTrigger;

    // Start is called before the first frame update
    void Awake()
    {
        _doorTrigger = FindObjectOfType<DoorTrigger>();
    }

    public void CollectedItem()
    {
        _amountGathered++;

        Debug.Log(_amountGathered);

        if (_amountGathered >= 5)
        {
            Debug.Log("You Got All of Them!");
            _doorTrigger.OpenDoor();
        }
    }
}

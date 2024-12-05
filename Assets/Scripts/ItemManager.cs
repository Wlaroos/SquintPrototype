using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    [SerializeField] private GameObject _glassesPrefab;
    
    [SerializeField] private AudioClip _collectSFX;
    [SerializeField] private AudioClip _hitSFX;

    private ListItem[] _groceryItemArray;
    private SelfCheckoutDoorTrigger[] _selfCheckoutArray;
    private List<ListItem> _groceryItemList = new List<ListItem>();

    private CameraBlur _blurRef;
    private int _amountGathered;
    private DoorTrigger _doorTrigger;
    private float _eventDelaySeconds = 2;
    private PickUp _pickUpPlayerRef;
    private GameObject _holder;
    private SquintUIPanel _squintUIRef;

    // Start is called before the first frame update
    void Awake()
    {
        _pickUpPlayerRef = FindObjectOfType<PickUp>();
        _holder = _pickUpPlayerRef._holder;

        _blurRef = FindObjectOfType<CameraBlur>();
        _doorTrigger = FindObjectOfType<DoorTrigger>();

        _groceryItemArray = FindObjectsOfType<ListItem>();
        _selfCheckoutArray = FindObjectsOfType<SelfCheckoutDoorTrigger>();

        _squintUIRef = FindObjectOfType<SquintUIPanel>();

        foreach (var item in _groceryItemArray)
        {
            _groceryItemList.Add(item);
        }
    }

    public void CollisionCheck(Collider other)
    {
        foreach (var item in _groceryItemList)
        {
            if (item.ItemName == other.name)
            {
                item.Collected();
                CollectedItem();

                _groceryItemList.Remove(item);

                //Debug.Log("COLLECTED -- " + other.name);

                Destroy(other.gameObject);

                break;
            }
        }
    }

    private void CollectedItem()
    {
        _amountGathered++;
        
        AudioHelper.PlayClip2D(_collectSFX, 1);
        
        //Debug.Log(_amountGathered);

        if(_amountGathered == 2)
        {
            StartCoroutine(GlassesEvent());
        }
        else if (_amountGathered >= 5)
        {
            Debug.Log("You Got All of Them!");
            _squintUIRef.SetObjectiveText("Go to self checkout");
            
            foreach (var item in _selfCheckoutArray)
            {
                item.Activate();
            }
        }
    }

    private IEnumerator GlassesEvent()
    {
        yield return new WaitForSeconds(_eventDelaySeconds);

        _blurRef.GlassesBreak();
        GameObject glasses = Instantiate(_glassesPrefab, _holder.transform.position, Quaternion.Euler(_holder.transform.rotation.eulerAngles + new Vector3(0,-90,0)));

        glasses.GetComponent<Rigidbody>().AddForce(Camera.main.transform.forward * 100);
        
        AudioHelper.PlayClip2D(_hitSFX, 1);
        //glasses.GetComponent<Rigidbody>().useGravity = false;
    }
}

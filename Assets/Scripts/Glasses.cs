using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glasses : MonoBehaviour
{
    private LayerMask _groundLayer;
    private GameObject _glassesNotBroken;
    private GameObject _glassesBroken;
    
    [SerializeField] private AudioClip _glassBreakSFX;

    private void Awake()
    {
        _groundLayer = LayerMask.NameToLayer("Ground");
        _glassesNotBroken = transform.GetChild(0).gameObject;
        _glassesBroken = transform.GetChild(1).gameObject;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == _groundLayer)
        {
            //Debug.Log("HIT GROUND");
            GetComponent<PickupInteractable>().SetTooltip("Broken Glasses");
            _glassesNotBroken.SetActive(false);
            _glassesBroken.SetActive(true);
            
            AudioHelper.PlayClip2D(_glassBreakSFX, 1f);
        }
    }
}

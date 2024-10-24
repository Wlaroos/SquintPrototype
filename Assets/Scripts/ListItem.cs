using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ListItem : MonoBehaviour
{
    [SerializeField] Sprite[] _spriteList;
    [SerializeField] string _itemName;

    public string ItemName => _itemName;

    private Image _image;
    bool _isCollected = false;

    private void Awake()
    {
        _image = GetComponent<Image>();

        if (_spriteList.Length > 1)
        {
            _image.sprite = _spriteList[0];
        }
    }

    public void Collected()
    {
        _isCollected = true;

        if (_spriteList.Length > 1)
        {
            _image.sprite = _spriteList[1];
        }
    }
}

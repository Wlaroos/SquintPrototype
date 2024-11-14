using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WorldText : MonoBehaviour
{
    [SerializeField] GameObject _text1;
    [SerializeField] GameObject _text2;
    [SerializeField] GameObject _cube;
    [SerializeField] bool _twoSided;

    private void Awake()
    {
        if(_twoSided)
        {
            _text2.gameObject.SetActive(true);
            Vector3 textpos = _text1.transform.localPosition;
            _text2.GetComponent<TextMeshPro>().text = _text1.GetComponent<TextMeshPro>().text;
            _text2.GetComponent<TextMeshPro>().margin = _text1.GetComponent<TextMeshPro>().margin;
            _text2.transform.localPosition = new Vector3(-textpos.x, textpos.y, _cube.transform.localScale.z + 0.01f);
            _text2.transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
    }
}

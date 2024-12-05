using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomTexturePicker : MonoBehaviour
{
    [SerializeField] private Texture[] _textures;
    private Renderer _renderer;
    
    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _renderer.material.mainTexture = _textures[Random.Range(0, _textures.Length)];
        
        var rotation = transform.localRotation;
        rotation = Quaternion.Euler(rotation.eulerAngles.x, rotation.eulerAngles.y + Random.Range(-15, 15), rotation.eulerAngles.z);
        transform.localRotation = rotation;
    }
}

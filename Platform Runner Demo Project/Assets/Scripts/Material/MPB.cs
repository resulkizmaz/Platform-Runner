using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MPB : MonoBehaviour
{
    protected Renderer _renderer;
    protected MaterialPropertyBlock _materialPropertyBlock;

    private void Start()
    {
        _renderer = GetComponent<Renderer>();
        _materialPropertyBlock = new MaterialPropertyBlock();
    }

      
}

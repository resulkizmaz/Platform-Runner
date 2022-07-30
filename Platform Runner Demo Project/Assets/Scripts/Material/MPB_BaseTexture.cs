using UnityEngine;

public class MPB_BaseTexture : MPB
{
    [SerializeField] private Texture2D baseTexture;

    private void Update()
    {
        if(baseTexture != null)
        {
            _materialPropertyBlock.SetTexture("_BaseMap", baseTexture);
            _renderer.SetPropertyBlock(_materialPropertyBlock);
        }
       
    }
}
/*
    Although we use only one material, this Material Property Block function lets us make bunch of different material without clonning it.
 */

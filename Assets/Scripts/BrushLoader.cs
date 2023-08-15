using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrushLoader : MonoBehaviour
{

    private void OnEnable()
    {
        OnTextureLoad();
    }

    private void OnTextureLoad()
    {
        Texture2D modifiedTexture = TextureManager.instance.DefaultTexture;
        MeshRenderer mr = GetComponent<MeshRenderer>();
        Material mat = new Material(Shader.Find("Standard"));
        mat.mainTexture = modifiedTexture;
        mr.material = mat;
    }
}

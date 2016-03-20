using UnityEngine;
using System.Collections;

public class SignHolder : MonoBehaviour
{
    public MeshRenderer signMeshRenderer;
    
    public void SetSign(Texture signTexture)
    {
        signMeshRenderer.material.mainTexture = signTexture;
    }
}

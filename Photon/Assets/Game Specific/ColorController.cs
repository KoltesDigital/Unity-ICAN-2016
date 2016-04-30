using UnityEngine;

public class ColorController : MonoBehaviour
{
    public Color color
    {
        get
        {
            return material.color;
        }
        set
        {
            material.color = value;
        }
    }

    Material material;

    void Awake()
    {
        material = GetComponent<MeshRenderer>().material;
    }
}

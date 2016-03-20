using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Material roadMaterial;
    Vector2 textureOffset;

    void Awake()
    {
        textureOffset = roadMaterial.mainTextureOffset;
    }

	public void Advance(float da)
    {
        textureOffset.y += Constants.CylinderAngleOffsetToTextureOffset(da);
        roadMaterial.mainTextureOffset = textureOffset;

    }
}

using UnityEngine;

public static class Constants
{
    public const float RoadRadius = 50.0f; // Cylinder radius
    public const float EdgeAngle = 1.08f; // Angle at cylinder edges
    public const float VMax = 0.25f; // UV maps from (0, 0) to (0, VMax)
    public const float XMax = 100.0f; // Road X axis in range (-XMax, XMax)

    public static int GetRoadTextureY(Material roadMaterial, float cylinderAngle)
    {
        // cylinderAngle in range (-EdgeAngle, EdgeAngle)
        // -> v in range (0, VMax)
        float v = ((cylinderAngle / EdgeAngle) + 1.0f) * 0.5f * VMax;

        // texture offset
        v += roadMaterial.mainTextureOffset.y;

        // texture wraps
        v = Mathf.Repeat(v, 1.0f);

        return Mathf.FloorToInt(v * roadMaterial.mainTexture.height);
    }

    public static float CylinderAngleOffsetToTextureOffset(float da)
    {
        // angle offset of 2*EdgeAngle -> texture offset of VMax
        return da / (2.0f * EdgeAngle) * VMax;
    }
}

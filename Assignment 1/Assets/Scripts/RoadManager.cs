using UnityEngine;
using System.Collections.Generic;

public class RoadManager : MonoBehaviour
{
    public Material roadMaterial;
    Vector2 textureOffset;

    void Awake()
    {
        textureOffset = roadMaterial.mainTextureOffset;
    }

    public void Advance(float da)
    {
        textureOffset.y = Mathf.Repeat(textureOffset.y + Constants.CylinderAngleOffsetToTextureOffset(da), 1.0f);
        roadMaterial.mainTextureOffset = textureOffset;
    }

    // return a list of positions along X axis in range (-XMax, XMax)
    public List<float> GetBorders(float cylinderAngle)
    {
        List<float> borders = new List<float>();

        int clusterPoints = 0;
        int clusterXSum = 0;

        int y = Constants.GetRoadTextureY(roadMaterial, cylinderAngle);
        Texture2D roadTexture = roadMaterial.mainTexture as Texture2D;
        for (int x = 0; x < roadTexture.width; ++x)
        {
            Color color = roadTexture.GetPixel(x, y);

            if (color.maxColorComponent < Constants.BlackThreshold)
            {
                // build clusters of black pixels
                ++clusterPoints;
                clusterXSum += x;
            }
            else if (clusterPoints > 0)
            {
                // cluster is over
                float center = (float)clusterXSum / clusterPoints;
                borders.Add((center * 2.0f / roadTexture.width - 1.0f) * Constants.XMax);
                clusterXSum = 0;
                clusterPoints = 0;
            }
        }

        // if we're still in a cluster
        if (clusterPoints > 0)
        {
            float center = (float)clusterXSum / clusterPoints;
            borders.Add((center * 2.0f / roadTexture.width - 1.0f) * Constants.XMax);
        }

        if (borders.Count != 2)
        {
            Debug.LogWarning("There are not exactly 2 borders");
        }

        return borders;
    }
}

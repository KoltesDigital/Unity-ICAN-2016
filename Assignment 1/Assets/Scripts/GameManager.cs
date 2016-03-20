using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public Material roadMaterial;
    Vector2 textureOffset;

    // already generate trees at startup
    // the same algorithm is used, but instead of adding
    // trees at EdgeAngle, the half-cylinder is covered
    // with finite steps
    public int TreeFillingStartSteps = 100;

    // when filling with trees at a given cylinder angle,
    // try this much before giving up
    public int TreeFillingAttempts = 100;

    public GameObject treePrefab;
    public float minTreeRadius = 5.0f;
    public float maxTreeRadius = 10.0f;
    float nextTreeRadius;

    List<RoadFollower> roadFollowers = new List<RoadFollower>();
    public Transform roadFollowersParent;

    void Awake()
    {
        textureOffset = roadMaterial.mainTextureOffset;
    }

    void Start()
    {
        GenerateNextTreeRadius();

        for (int i = 0; i < TreeFillingStartSteps; ++i)
        {
            float angle = (i * 2.0f / TreeFillingStartSteps - 1.0f) * Constants.EdgeAngle;
            FillWithTrees(angle);
        }
    }

	public void Advance(float da)
    {
        // offsets texture
        textureOffset.y = Mathf.Repeat(textureOffset.y + Constants.CylinderAngleOffsetToTextureOffset(da), 1.0f);
        roadMaterial.mainTextureOffset = textureOffset;

        FillWithTrees();

        // advances road followers, and remove them
        // when they are too far behind the car
        for (int i = roadFollowers.Count - 1; i >= 0; --i)
        {
            RoadFollower roadFollower = roadFollowers[i];
            roadFollower.AdvanceCylinderAngle(da);
            if (roadFollower.GetCylinderAngle() < -Constants.EdgeAngle)
            {
                Destroy(roadFollower.gameObject);
                roadFollowers.RemoveAt(i);
            }
        }
    }

    void FillSectionWithTrees(float cylinderAngle, float xMin, float xMax)
    {
        int attempt;
        do
        {
            for (attempt = 0; attempt < TreeFillingAttempts; ++attempt)
            {
                float x = Random.Range(xMin, xMax);

                // search whether the place is empty
                int i;
                for (i = 0; i < roadFollowers.Count; ++i)
                {
                    RoadFollower roadFollower = roadFollowers[i];

                    Vector2 offset;
                    offset.x = roadFollower.GetX() - x;
                    offset.y = (roadFollower.GetCylinderAngle() - cylinderAngle) * Constants.RoadRadius;
                    
                    if (offset.magnitude < roadFollower.radius + nextTreeRadius)
                    {
                        break;
                    }
                }

                // yay! the place is empty
                if (i == roadFollowers.Count)
                {
                    GameObject tree = Instantiate<GameObject>(treePrefab);
                    RoadFollower roadFollower = tree.GetComponent<RoadFollower>();
                    if (roadFollower)
                    {
                        roadFollower.transform.SetParent(roadFollowersParent, false);

                        roadFollower.SetCylinderAngle(cylinderAngle);
                        roadFollower.SetX(x);
                        roadFollower.radius = nextTreeRadius;
                        roadFollowers.Add(roadFollower);

                        GenerateNextTreeRadius();
                    }
                    else
                    {
                        Debug.LogError("Tree prefab has no RoadFollower component");
                    }

                    break;
                }
            }

            // a tree was planted: search again for a new tree
        } while (attempt < TreeFillingAttempts);
    }

    void FillWithTrees(float cylinderAngle = Constants.EdgeAngle)
    {
        List<float> borders = GetRoadBorders(roadMaterial, cylinderAngle);
        FillSectionWithTrees(cylinderAngle, -Constants.XMax, borders[0]);
        FillSectionWithTrees(cylinderAngle, borders[1], Constants.XMax);
    }

    void GenerateNextTreeRadius()
    {
        nextTreeRadius = Random.Range(minTreeRadius, maxTreeRadius);
    }

    // return a list of positions along X axis in range (-XMax, XMax)
    static List<float> GetRoadBorders(Material roadMaterial, float cylinderAngle)
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

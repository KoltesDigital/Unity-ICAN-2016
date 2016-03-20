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
    public int StartTreeFillingSteps = 100;

    public GameObject treePrefab;
    List<RoadFollower> roadFollowers = new List<RoadFollower>();
    public Transform roadFollowersParent;

    void Awake()
    {
        textureOffset = roadMaterial.mainTextureOffset;
    }

    void Start()
    {
        for (int i = 0; i < StartTreeFillingSteps; ++i)
        {
            float angle = (i * 2.0f / StartTreeFillingSteps - 1.0f) * Constants.EdgeAngle;
            FillWithTrees(angle);
        }
    }

	public void Advance(float da)
    {
        // offsets texture
        textureOffset.y += Constants.CylinderAngleOffsetToTextureOffset(da);
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

    void FillWithTrees(float cylinderAngle = Constants.EdgeAngle)
    {
        if (Random.value < 0.02)
        {
            GameObject tree = Instantiate<GameObject>(treePrefab);
            RoadFollower roadFollower = tree.GetComponent<RoadFollower>();
            if (roadFollower)
            {
                // just to test TODO
                roadFollower.SetCylinderAngle(cylinderAngle);
                roadFollower.SetX(Random.Range(-Constants.XMax, Constants.XMax));
                roadFollowers.Add(roadFollower);
                roadFollower.transform.SetParent(roadFollowersParent, false);
            }
            else
            {
                Debug.LogError("Tree prefab has no RoadFollower component");
            }
        }
    }
}

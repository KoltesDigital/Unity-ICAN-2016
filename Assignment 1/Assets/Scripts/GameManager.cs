using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(RoadManager))]
public class GameManager : MonoBehaviour
{
    [HideInInspector]
    public RoadManager roadManager;

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
    public float treeMarginToRoad = 5.0f;

    public GameObject signPrefab;
    public List<Texture> signTextures = new List<Texture>();
    DeckGenerator<Texture> signTextureGenerator = new DeckGenerator<Texture>();
    public float minSignInterval = 1.0f;
    public float maxSignInterval = 2.0f;
    float nextSignAngle = -Constants.EdgeAngle;

    List<RoadFollower> roadFollowers = new List<RoadFollower>();
    public Transform roadFollowersParent;
    
    void Awake()
    {
        roadManager = GetComponent<RoadManager>();
    }

    void Start()
    {
        signTextureGenerator.Add(signTextures);

        GenerateNextTreeRadius();

        for (int i = 0; i < TreeFillingStartSteps; ++i)
        {
            float angle = (i * 2.0f / TreeFillingStartSteps - 1.0f) * Constants.EdgeAngle;
            FillWithTrees(angle);
            FillWithSign(angle);
        }
    }

	public void Advance(float da)
    {
        // offsets texture
        roadManager.Advance(da);

        // add trees and sign
        FillWithTrees();

        nextSignAngle -= da;
        FillWithSign();

        // advances road followers, and remove them
        // when they are too far behind the car
        for (int i = roadFollowers.Count - 1; i >= 0; --i)
        {
            RoadFollower roadFollower = roadFollowers[i];
            roadFollower.cylinderAngle -= da;
            if (roadFollower.cylinderAngle < -Constants.EdgeAngle)
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
                    offset.x = roadFollower.x - x;
                    offset.y = (roadFollower.cylinderAngle - cylinderAngle) * Constants.RoadRadius;
                    
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

                        roadFollower.cylinderAngle = cylinderAngle;
                        roadFollower.x = x;
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
        List<float> borders = roadManager.GetBorders(cylinderAngle);
        FillSectionWithTrees(cylinderAngle, -Constants.XMax, borders[0] - treeMarginToRoad);
        FillSectionWithTrees(cylinderAngle, borders[1] + treeMarginToRoad, Constants.XMax);
    }
    
    void GenerateNextTreeRadius()
    {
        nextTreeRadius = Random.Range(minTreeRadius, maxTreeRadius);
    }

    void FillWithSign(float cylinderAngle = Constants.EdgeAngle)
    {
        if (nextSignAngle < cylinderAngle)
        {
            List<float> borders = roadManager.GetBorders(nextSignAngle);
            float x = borders[Random.Range(0, borders.Count)];

            GameObject sign = Instantiate<GameObject>(signPrefab);
            RoadFollower roadFollower = sign.GetComponent<RoadFollower>();
            SignHolder signHolder = sign.GetComponent<SignHolder>();
            if (roadFollower && signHolder)
            {
                roadFollower.transform.SetParent(roadFollowersParent, false);

                roadFollower.cylinderAngle = nextSignAngle;
                roadFollower.x = x;
                roadFollowers.Add(roadFollower);

                signHolder.SetSign(signTextureGenerator.Draw());
            }
            else
            {
                Debug.LogError("Sign prefab has no RoadFollower or SignHolder component");
            }

            nextSignAngle += Random.Range(minSignInterval, maxSignInterval);
        }
    }
}

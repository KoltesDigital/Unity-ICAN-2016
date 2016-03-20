using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(RoadFollower))]
public class PlayerController : MonoBehaviour
{
    public GameManager gameManager;
    RoadManager roadManager;

    public float cylinderAngle = -0.6f;
    public float maxTurnAngle = 0.5f;
    public float strafeRatio = 1.0f;

    public float accelerationRatio = 1.0f;
    public float damping = 1.0f;
    float speed = 0.0f;

    public float margin = 5.0f;

    RoadFollower roadFollower;
    
    void ResetToRoadCenter()
    {
        List<float> borders = roadManager.GetBorders(cylinderAngle);
        roadFollower.x = (borders[0] + borders[1]) * 0.5f;
        roadFollower.ResetRotation();
        speed = 0.0f;
    }

    void Awake()
    {
        roadFollower = GetComponent<RoadFollower>();
    }
    
    void Start ()
    {
        roadManager = gameManager.roadManager;
        
        roadFollower.cylinderAngle = cylinderAngle;
        ResetToRoadCenter();
    }
	
	void Update ()
    {
        // movement
        float horizontal = Mathf.Clamp(Input.GetAxis("Horizontal"), -speed, speed);
        roadFollower.rotationTargetAngle = horizontal * maxTurnAngle;
        roadFollower.x += horizontal * strafeRatio;

        // speed integration
        float vertical = Input.GetAxis("Vertical");
        float acceleration = vertical * accelerationRatio;
        speed += acceleration * Time.deltaTime;
        speed *= 1.0f / (1.0f + damping * Time.deltaTime);

        // checking for off road
        float x = roadFollower.x;
        List<float> borders = roadManager.GetBorders(cylinderAngle);
        if (x - margin < borders[0] || borders[1] < x + margin)
        {
            ResetToRoadCenter();
        }

        // this is the cylinder angle the car travels during this frame
        // in reality, the car doesn't move, all other objects move backwards
        float da = speed * Time.deltaTime;
        gameManager.Advance(da);
    }
}

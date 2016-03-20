using UnityEngine;
using System.Collections;

[RequireComponent(typeof(RoadFollower))]
public class PlayerController : MonoBehaviour {
    public GameManager gameManager;

    public float cylinderAngle = -0.6f;
    public float maxTurnAngle = 0.5f;
    public float strafeRatio = 1.0f;

    public float accelerationRatio = 1.0f;
    public float damping = 1.0f;
    float speed = 0.0f;

    RoadFollower roadFollower;

    void Awake()
    {
        roadFollower = GetComponent<RoadFollower>();
    }
    
    void Start ()
    {
        roadFollower.SetCylinderAngle(cylinderAngle);
    }
	
	void Update ()
    {
        float horizontal = Input.GetAxis("Horizontal");
        roadFollower.SetRotationAngle(horizontal * maxTurnAngle);
        roadFollower.AdvanceX(horizontal * strafeRatio);

        float vertical = Input.GetAxis("Vertical");
        float acceleration = vertical * accelerationRatio;
        speed += acceleration * Time.deltaTime;
        speed *= 1.0f / (1.0f + damping * Time.deltaTime);
        float da = speed * Time.deltaTime;
        gameManager.Advance(da);
    }
}

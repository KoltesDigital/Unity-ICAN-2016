using UnityEngine;
using System.Collections;

[RequireComponent(typeof(RoadFollower))]
public class PlayerController : MonoBehaviour {
    public float cylinderAngle = -0.6f;
    public float maxTurnAngle = 0.5f;
    public float strafeRatio = 5.0f;

    RoadFollower roadFollower;

    void Awake()
    {
        roadFollower = GetComponent<RoadFollower>();
    }

    // Use this for initialization
    void Start ()
    {
        roadFollower.SetCylinderAngle(cylinderAngle);
    }
	
	// Update is called once per frame
	void Update ()
    {
        float horizontal = Input.GetAxis("Horizontal");
        roadFollower.SetRotationAngle(horizontal * maxTurnAngle);
        roadFollower.AdvanceX(horizontal * strafeRatio);
    }
}

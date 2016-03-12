using UnityEngine;
using System.Collections;

public class RoadFollower : MonoBehaviour
{
    public float angle = 0.0f;

    const float RoadRadius = 50.0f;
    const float EdgeAngle = 1.08f;

	void Update()
    {
        Vector3 position = transform.localPosition;

        // snap object to the ground
        position.y = (Mathf.Cos(angle) - 1.0f) * RoadRadius;
        position.z = Mathf.Sin(angle) * RoadRadius;
        transform.localPosition = position;

        // orient object to the normal
        transform.localRotation = Quaternion.AngleAxis(Mathf.Rad2Deg * angle, Vector3.right);
    }
}

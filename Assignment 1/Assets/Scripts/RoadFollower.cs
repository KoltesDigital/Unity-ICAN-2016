using UnityEngine;
using System.Collections;

public class RoadFollower : MonoBehaviour
{
    // angle around the cylinder
    // this drives the object's YZ coordinates
    float cylinderAngle = 0.0f;

    // object rotation around its Z axis to simulate the car turning
    float rotationTargetAngle = 0.0f;
    float rotationCurrentAngle = 0.0f;
    public float rotationLerpRatio = 1.0f;

    // cache variable to assign to transform.localPosition
    Vector3 position;

    void Start()
    {
        position = transform.localPosition;
    }

    void Update()
    {
        rotationCurrentAngle = Mathf.Lerp(rotationCurrentAngle, rotationTargetAngle, rotationLerpRatio * Time.deltaTime);

        // orient object to the normal
        Quaternion rotation = Quaternion.AngleAxis(Mathf.Rad2Deg * cylinderAngle, Vector3.right) * Quaternion.AngleAxis(Mathf.Rad2Deg * rotationCurrentAngle, Vector3.up);
        transform.localRotation = rotation;
    }

    void UpdateYZPosition()
    {
        // snap object to the ground
        position.y = (Mathf.Cos(cylinderAngle) - 1.0f) * Constants.RoadRadius;
        position.z = Mathf.Sin(cylinderAngle) * Constants.RoadRadius;
        transform.localPosition = position;
    }

    public void SetX(float x)
    {
        position.x = x;
        transform.localPosition = position;
    }

    public void AdvanceX(float dx)
    {
        position.x += dx;
        transform.localPosition = position;
    }

    public void SetCylinderAngle(float angle)
    {
        cylinderAngle = angle;
        UpdateYZPosition();
    }

    public void AdvanceCylinderAngle(float da)
    {
        cylinderAngle += da;
        UpdateYZPosition();
    }

    public void SetRotationAngle(float angle)
    {
        rotationTargetAngle = angle;
    }
}

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

    // cache variable to assign to transform.localPosition
    Vector3 position;

    const float RoadRadius = 50.0f;
    const float EdgeAngle = 1.08f;
    public float LerpRatio = 1.0f;

    void Start()
    {
        position = transform.localPosition;
    }

    void Update()
    {
        rotationCurrentAngle = Mathf.Lerp(rotationCurrentAngle, rotationTargetAngle, LerpRatio * Time.deltaTime);

        // orient object to the normal
        Quaternion rotation = Quaternion.AngleAxis(Mathf.Rad2Deg * cylinderAngle, Vector3.right) * Quaternion.AngleAxis(Mathf.Rad2Deg * rotationCurrentAngle, Vector3.up);
        transform.localRotation = rotation;
    }

    void UpdateYZPosition()
    {
        // snap object to the ground
        position.y = (Mathf.Cos(cylinderAngle) - 1.0f) * RoadRadius;
        position.z = Mathf.Sin(cylinderAngle) * RoadRadius;
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

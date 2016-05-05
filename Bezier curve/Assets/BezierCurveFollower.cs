using UnityEngine;

[ExecuteInEditMode]
public class BezierCurveFollower : MonoBehaviour
{
    public BezierCurve curve;
    public float t = 0.0f;

    void Update()
    {
        if (curve)
            transform.position = curve.Interpolate(t);
    }
}

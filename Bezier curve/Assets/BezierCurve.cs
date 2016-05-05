using UnityEngine;
using System;

public class BezierCurve : MonoBehaviour
{
    public Vector3[] endPoints;
    public Vector3[] controlPoints;

    // Ensure that the curve has four points
    void OnValidate()
    {
        if (endPoints == null)
            endPoints = new Vector3[0];
        Array.Resize(ref endPoints, 2);

        if (controlPoints == null)
            controlPoints = new Vector3[0];
        Array.Resize(ref controlPoints, 2);
    }

    public Vector3 Interpolate(float t)
    {
        float T = 1 - t;
        return T * T * T * endPoints[0]
            + 3.0f * T * T * t * controlPoints[0]
            + 3.0f * T * t * t * controlPoints[1]
            + t * t * t * endPoints[1];
    }
}

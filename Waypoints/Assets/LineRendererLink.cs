using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class LineRendererLink : MonoBehaviour, Link
{
    public void Setup(Vector3 a, Vector3 b)
    {
        Vector3[] positions = new Vector3[2] { a, b };
        LineRenderer lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.SetVertexCount(2);
        lineRenderer.SetPositions(positions);
    }
}

using UnityEngine;

public class InstantiateLink : MonoBehaviour, Link
{
    public void Setup(Vector3 a, Vector3 b)
    {
        Vector3 offset = b - a;

        transform.position = Vector3.Lerp(a, b, 0.5f);

        Vector3 scale = transform.localScale;
        scale.x = offset.magnitude;
        transform.localScale = scale;

        float angle = Mathf.Atan2(offset.y, offset.x);
        transform.Rotate(Vector3.forward, angle * Mathf.Rad2Deg);
    }
}

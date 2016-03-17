using UnityEngine;

public class GoodShakerInvoke : MonoBehaviour
{
    public float IntervalMin;
    public float IntervalMax;
    public float SpringFactor;

    private Vector3 targetPosition;

    private void PickNewTarget()
    {
        targetPosition = Vector3.Scale(Random.insideUnitSphere, transform.localScale);
        Invoke("PickNewTarget", Random.Range(IntervalMin, IntervalMax));
    }

    void Start()
    {
        PickNewTarget();
	}

    void Update()
    {
        Vector3 currentPosition = transform.localPosition;
        Vector3 newPosition = Vector3.Lerp(currentPosition, targetPosition, SpringFactor);
        transform.localPosition = newPosition;
    }
}

using UnityEngine;

public class GoodShakerDistance : MonoBehaviour
{
    public float IntervalMin;
    public float IntervalMax;
    public float SpringFactor;
    public float ThresholdDistance;

    private Vector3 targetPosition;

    private void PickNewTarget()
    {
        targetPosition = Vector3.Scale(Random.insideUnitSphere, transform.localScale);
    }

    void Start()
    {
        PickNewTarget();
	}

    void Update()
    {
        Vector3 currentPosition = transform.localPosition;
        if (Vector3.Distance(targetPosition, currentPosition) <= ThresholdDistance)
        {
            PickNewTarget();
        }

        Vector3 newPosition = Vector3.Lerp(currentPosition, targetPosition, SpringFactor);
        transform.localPosition = newPosition;
    }
}

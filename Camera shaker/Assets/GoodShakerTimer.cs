using UnityEngine;

public class GoodShakerTimer : MonoBehaviour
{
    public float IntervalMin;
    public float IntervalMax;
    public float SpringFactor;

    private Vector3 targetPosition;
    private float timer;
    
    private void PickNewTarget()
    {
        targetPosition = Vector3.Scale(Random.insideUnitSphere, transform.localScale);
        timer = Random.Range(IntervalMin, IntervalMax);
    }

    void Start()
    {
        PickNewTarget();
	}

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            PickNewTarget();
        }

        Vector3 currentPosition = transform.localPosition;
        Vector3 newPosition = Vector3.Lerp(currentPosition, targetPosition, SpringFactor);
        transform.localPosition = newPosition;
    }
}

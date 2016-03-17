using UnityEngine;

public class BadShaker : MonoBehaviour
{
    Vector3 target;

    float timer;

    void PickNewTarget()
    {
        target = Random.insideUnitSphere;
        timer = Random.Range(1.0f, 2.0f);
    }

    void Start()
    {
        PickNewTarget();
    }

	void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0.0f)
        {
            PickNewTarget();
        }

        transform.localPosition = Vector3.Lerp(transform.localPosition, target, 1.0f * Time.deltaTime);
	}
}

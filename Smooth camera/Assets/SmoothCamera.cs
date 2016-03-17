using UnityEngine;
using System.Collections;

public class SmoothCamera : MonoBehaviour
{
    public float lerpRatio = 5.0f;
    public float moveFactor = 0.1f;

    private Vector3 target;
    
    void Start ()
    {
        target = transform.position;
	}
	
    void Update ()
    {
        target.x += Input.GetAxis("Mouse X") * moveFactor;
        target.y += Input.GetAxis("Mouse Y") * moveFactor;

        transform.position = Vector3.Lerp(transform.position, target, lerpRatio * Time.deltaTime);
    }
}

using UnityEngine;
using System.Collections;

public class Spring : MonoBehaviour
{
    public float mass = 1f;
    public float stiffness = 1f;
    public float damping = 0.5f;
    
    public Vector3 speed = Vector3.zero;
    
    void Update()
    {
        // Spring force
        Vector3 position = transform.localPosition;
        Vector3 forces = - position * stiffness;
        
        // Newton's second law
        // sum of forces = mass * acceleration
        Vector3 acceleration = forces / mass;

        // Euler method for speed integration 
        speed += acceleration * Time.deltaTime;

        // Damping
        speed *= 1.0f / (1.0f + Time.deltaTime * damping);

        // Position integration
        transform.localPosition += speed * Time.deltaTime;
    }
}

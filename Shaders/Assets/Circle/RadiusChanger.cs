using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class RadiusChanger : MonoBehaviour
{
    public float frequency = 2.0f;
    public float amplitudeMin = 0.2f;
    public float amplitudeMax = 0.4f;
    public float feather = 0.05f;

    Material material;

    void Start()
    {
        material = GetComponent<Renderer>().material;
    }

    void Update ()
    {
        float amplitudeCenter = (amplitudeMax + amplitudeMin) * 0.5f;
        float amplitudeDeviation = (amplitudeMax - amplitudeMin) * 0.5f;
        float radius = Mathf.Sin(2.0f * Mathf.PI * frequency * Time.time) * amplitudeDeviation + amplitudeCenter;

        material.SetFloat("_Radius", radius); // just in case you want to use it
        material.SetFloat("_InnerRadius", radius - feather * 0.5f);
        material.SetFloat("_OuterRadius", radius + feather * 0.5f);
    }
}

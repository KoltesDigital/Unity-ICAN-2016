using UnityEngine;
using System.Collections;

public class RandomColorBehavior : MonoBehaviour
{
    public float hueMin = 0.0f;
    public float hueMax = 1.0f;
    public float saturationMin = 0.7f;
    public float saturationMax = 1.0f;
    public float valueMin = 0.8f;
    public float valueMax = 0.9f;

    // Sets the object to a new random color
    private void PickColor()
    {
        // 1. warrior style
        float h = Random.value * (hueMax - hueMin) + hueMin;
        float s = Random.value * (saturationMax - saturationMin) + saturationMin;
        float v = Random.value * (valueMax - valueMin) + valueMin;
        Color newColor = Color.HSVToRGB(h, s, v);

        // 2. with lerps
        // float h = Mathf.Lerp(hueMin, hueMax, Random.value);
        // float s = Mathf.Lerp(saturationMin, saturationMax, Random.value);
        // float v = Mathf.Lerp(valueMin, valueMax, Random.value);
        // Color newColor = Color.HSVToRGB(h, s, v);

        // 3. specific to Unity
        // Color newColor = Random.ColorHSV(hueMin, hueMax, saturationMin, saturationMax, valueMin, valueMax);

        GetComponent<Renderer>().material.color = newColor;
    }
    
	void Start()
    {
        PickColor();
	}
	
	void Update()
    {
        if (Input.anyKeyDown)
            PickColor();
    }
}

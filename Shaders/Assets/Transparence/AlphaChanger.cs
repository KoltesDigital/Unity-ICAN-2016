using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(Renderer))]
public class AlphaChanger : MonoBehaviour
{
    public float frequency = 1.0f;
    public List<Texture> textures = new List<Texture>();

    Material material;
    float time = 0.0f;
    int textureIndex = 0;

	void Start()
    {
        material = GetComponent<Renderer>().material;
    }
	
	void Update()
    {
        time += Time.deltaTime;
        while (time >= frequency)
        {
            time -= frequency;
            textureIndex = (textureIndex + 1) % textures.Count;
            material.mainTexture = textures[textureIndex];
        }

        material.SetFloat("_AlphaOffset", -Mathf.Cos(2.0f * Mathf.PI * frequency * time));
	}
}

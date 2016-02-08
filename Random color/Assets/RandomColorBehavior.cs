using UnityEngine;
using System.Collections;

public class RandomColorBehavior : MonoBehaviour
{
    // Sets the object to a new random color
    private void PickColor()
    {
        float h = Random.value; // all hues
        float s = Random.value * 0.3f + 0.7f;
        float v = Random.value * 0.1f + 0.8f;
        Color newColor = HSVToRGB(h, s, v);
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

    // http://answers.unity3d.com/questions/701956/hsv-to-rgb-without-editorguiutilityhsvtorgb.html
    // Parameters in (0, 1)
    private static Color HSVToRGB(float H, float S, float V)
    {
        if (S == 0f)
            return new Color(V, V, V);
        else if (V == 0f)
            return Color.black;
        else
        {
            Color col = Color.black;
            float Hval = H * 6f;
            int sel = Mathf.FloorToInt(Hval);
            float mod = Hval - sel;
            float v1 = V * (1f - S);
            float v2 = V * (1f - S * mod);
            float v3 = V * (1f - S * (1f - mod));
            switch (sel + 1)
            {
                case 0:
                    col.r = V;
                    col.g = v1;
                    col.b = v2;
                    break;
                case 1:
                    col.r = V;
                    col.g = v3;
                    col.b = v1;
                    break;
                case 2:
                    col.r = v2;
                    col.g = V;
                    col.b = v1;
                    break;
                case 3:
                    col.r = v1;
                    col.g = V;
                    col.b = v3;
                    break;
                case 4:
                    col.r = v1;
                    col.g = v2;
                    col.b = V;
                    break;
                case 5:
                    col.r = v3;
                    col.g = v1;
                    col.b = V;
                    break;
                case 6:
                    col.r = V;
                    col.g = v1;
                    col.b = v2;
                    break;
                case 7:
                    col.r = V;
                    col.g = v3;
                    col.b = v1;
                    break;
            }
            col.r = Mathf.Clamp(col.r, 0f, 1f);
            col.g = Mathf.Clamp(col.g, 0f, 1f);
            col.b = Mathf.Clamp(col.b, 0f, 1f);
            return col;
        }
    }
}

using UnityEngine;
using UnityEngine.UI;

public class Lifebar : MonoBehaviour
{
    [SerializeField] Image avatarImage;
    [SerializeField] Image lifeImage;
    [SerializeField] Image manaImage;
    [SerializeField] Image[] flagImages;

    public Texture avatar
    {
        get
        {
            return avatarImage.mainTexture;
        }
        set
        {
            avatarImage.material.mainTexture = value;
        }
    }

    public float barLerpRatio = 5.0f;
    public float flagLerpRatio = 5.0f;

    public float lifeRatio = 1.0f;
    float smoothLifeRatio;

    public float manaRatio = 1.0f;
    float smoothManaRatio;

    public bool[] flags;
    float[] smoothFlagOpacities;

    void OnValidate()
    {
        if (flags.Length != flagImages.Length)
        {
            flags = new bool[flagImages.Length];
            smoothFlagOpacities = new float[flagImages.Length];
        }
        else if (smoothFlagOpacities == null)
            smoothFlagOpacities = new float[flagImages.Length];
    }

    void Start()
    {
        smoothLifeRatio = lifeRatio;
        smoothManaRatio = manaRatio;

        for (int i = 0; i < flags.Length; ++i)
        {
            smoothFlagOpacities[i] = flags[i] ? 1.0f : 0.0f;
            flagImages[i].material = new Material(flagImages[i].material);
        }
    }

    void Update()
    {
        smoothLifeRatio = Mathf.Lerp(smoothLifeRatio, lifeRatio, barLerpRatio * Time.deltaTime);
        lifeImage.material.SetFloat("_Ratio", smoothLifeRatio);

        smoothManaRatio = Mathf.Lerp(smoothManaRatio, manaRatio, barLerpRatio * Time.deltaTime);
        manaImage.material.SetFloat("_Ratio", smoothManaRatio);

        for (int i = 0; i < flags.Length; ++i)
        {
            smoothFlagOpacities[i] = Mathf.Lerp(smoothFlagOpacities[i], flags[i] ? 1.0f : 0.0f, flagLerpRatio * Time.deltaTime);
            flagImages[i].material.SetFloat("_Opacity", smoothFlagOpacities[i]);
        }
    }
}

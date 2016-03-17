using UnityEngine;
using System.Collections.Generic;

public class ElectronManager : MonoBehaviour
{
    public int count = 20;
    public GameObject prefab;

    public float repulsiveForceConstant = 1f;
    public float damping = 0.5f;

    private class Electron
    {
        public GameObject gameObject;
        public Vector2 acceleration;
        public Vector2 speed = new Vector2();

        public Vector2 position
        {
            get
            {
                return gameObject.transform.localPosition;
            }
            set
            {
                gameObject.transform.localPosition = value;
            }
        }
    }

    private List<Electron> electrons = new List<Electron>();
    private float limitX;
    private float limitY;

    void Start()
    {
        Camera camera = Camera.main;
        limitX = camera.orthographicSize * camera.aspect;
        limitY = camera.orthographicSize;

        for (int i = 0; i < count; ++i)
        {
            Electron electron = new Electron();
            electron.gameObject = Instantiate<GameObject>(prefab);
            electron.gameObject.transform.SetParent(transform, false);
            electrons.Add(electron);

            electron.position = new Vector2(
                (Random.value - 0.5f) * 2f * limitX,
                (Random.value - 0.5f) * 2f * limitY
            );
        }
	}

    private void Wrap(ref Vector2 vector)
    {
        vector.x = Mathf.Repeat(vector.x + limitX, 2.0f * limitX) - limitX;
        vector.y = Mathf.Repeat(vector.y + limitY, 2.0f * limitY) - limitY;
    }

	void Update()
    {
        foreach (Electron electron in electrons)
        {
            electron.acceleration = new Vector2();

            foreach (Electron otherElectron in electrons)
            {
                if (electron != otherElectron)
                {
                    Vector2 offset = electron.position - otherElectron.position;
                    Wrap(ref offset);
                    electron.acceleration += offset.normalized * 1.0f / offset.sqrMagnitude;
                }
            }

            electron.acceleration *= repulsiveForceConstant;
        }

        float halfDt = 0.5f * Time.deltaTime;

        foreach (Electron electron in electrons)
        {
            electron.speed += electron.acceleration * halfDt;
            electron.speed *= 1.0f / (1.0f + halfDt * damping);

            Vector2 position = electron.position;
            position += electron.speed * Time.deltaTime;
            Wrap(ref position);
            electron.position = position;

            electron.speed += electron.acceleration * halfDt;
            electron.speed *= 1.0f / (1.0f + halfDt * damping);
        }
    }
}

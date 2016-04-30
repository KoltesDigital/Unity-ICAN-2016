using UnityEngine;

[RequireComponent(typeof(ColorController))]
public class PlayerController : Photon.MonoBehaviour
{
    public float speed = 5.0f;
    public float taintRatio = 0.3f;

    ColorController colorController;

    void Awake()
    {
        colorController = GetComponent<ColorController>();
    }

    void Update()
    {
        if (photonView.isMine)
        {
            Vector3 translation = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0.0f);
            transform.Translate(translation * speed * Time.deltaTime);

            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    PlayerController player = hit.collider.GetComponent<PlayerController>();
                    if (player)
                    {
                        PhotonView view = player.GetComponent<PhotonView>();
                        if (view && !view.isMine)
                        {
                            // Use the PhotonView of the target cube! Not the one from the controlled cube!
                            view.RPC("TaintColor", view.owner, colorController.color);

                            // Local prediction
                            player.TaintColor(colorController.color);
                        }
                    }
                }
            }
        }
    }

    [PunRPC]
    void TaintColor(Color taintColor)
    {
        colorController.color = Color.Lerp(colorController.color, taintColor, taintRatio);
        if (photonView.isMine)
        {
            // Buffering all color changes does not scale, but this is only a simple example...
            photonView.RPC("SetColor", PhotonTargets.AllBuffered, colorController.color);
        }
    }

    [PunRPC]
    void SetColor(Color color)
    {
        colorController.color = color;
    }
}

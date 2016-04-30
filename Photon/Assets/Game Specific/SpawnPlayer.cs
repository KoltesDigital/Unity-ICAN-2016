using UnityEngine;

public class SpawnPlayer : Photon.PunBehaviour
{
    public GameObject playerPrefab;
    
    override public void OnJoinedRoom()
    {
        int[] viewIds = new int[]
        {
            PhotonNetwork.AllocateViewID(),
        };

        float hue = Random.value;
        Color color = Color.HSVToRGB(hue, 0.9f, 0.9f);

        photonView.RPC("SpawnOnNetwork", PhotonTargets.AllBuffered, viewIds, color);
    }

    [PunRPC]
    void SpawnOnNetwork(int[] viewIds, Color color)
    {
        GameObject newPlayer = Instantiate(playerPrefab) as GameObject;
        
        PhotonView[] views = newPlayer.GetComponentsInChildren<PhotonView>();
        if (views.Length == viewIds.Length)
        {
            for (int i = 0; i < viewIds.Length; ++i)
                views[i].viewID = viewIds[i];
        }
        else
            Debug.LogErrorFormat("Incorrect view id count received for instantiation: received {0}, must be {1}", viewIds.Length, views.Length);

        newPlayer.GetComponentInChildren<ColorController>().color = color;
    }
}

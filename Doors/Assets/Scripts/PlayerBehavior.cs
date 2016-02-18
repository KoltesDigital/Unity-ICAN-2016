using UnityEngine;
using System.Collections;

public class PlayerBehavior : MonoBehaviour
{
    public GameManager ParentGameManager;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Door")
        {
            ParentGameManager.EnterRoom();
        }
    }
}

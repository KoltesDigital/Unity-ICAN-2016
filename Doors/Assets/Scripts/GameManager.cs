using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public Transform LeftDoorTransform;
    public Transform CenterDoorTransform;
    public Transform RightDoorTransform;
    public Transform BackDoorTransform;

    public FirstPersonController PlayerController;
    public Transform SpawnTransform;

    public GameObject BackDoorPrefab;
    public GameObject WithDoorPrefab;
    public GameObject WithoutDoorPrefab;

    private DeckGenerator<Room> roomGenerator = new DeckGenerator<Room>();
    private List<GameObject> instantiatedWalls = new List<GameObject>();

	void Start()
    {
        roomGenerator.Add(new Room(true , false, false));
        roomGenerator.Add(new Room(false, true , false));
        roomGenerator.Add(new Room(false, false, true ));
        roomGenerator.Add(new Room(false, true , true ));
        roomGenerator.Add(new Room(true , false, true ));
        roomGenerator.Add(new Room(true , true , false));
        roomGenerator.Add(new Room(true , true , true ));

        EnterRoom();
	}
	
    private void DestroyInstantiatedWalls()
    {
        foreach (GameObject wall in instantiatedWalls)
        {
            Destroy(wall);
        }
        instantiatedWalls.Clear();
    }

    private void CreateWall(GameObject prefab, Transform parentTransform)
    {
        GameObject wall = Instantiate<GameObject>(prefab);
        wall.transform.SetParent(parentTransform, false);
        instantiatedWalls.Add(wall);
    }

    private void CreateDoorWall(bool withDoor, Transform parentTransform)
    {
        CreateWall(withDoor ? WithDoorPrefab : WithoutDoorPrefab, parentTransform);
    }

    public void EnterRoom()
    {
        DestroyInstantiatedWalls();

        Room room = roomGenerator.Draw();
        CreateDoorWall(room.IsLeftDoor(), LeftDoorTransform);
        CreateDoorWall(room.IsCenterDoor(), CenterDoorTransform);
        CreateDoorWall(room.IsRightDoor(), RightDoorTransform);
        CreateWall(BackDoorPrefab, BackDoorTransform);
        
        PlayerController.transform.position = SpawnTransform.position;
        PlayerController.SetRotation(SpawnTransform.rotation);
    }
}

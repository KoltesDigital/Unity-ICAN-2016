public class Room
{
    private bool hasLeftDoor;
    private bool hasCenterDoor;
    private bool hasRightDoor;

    public Room(bool withLeftDoor, bool withCenterDoor, bool withRightDoor)
    {
        hasLeftDoor = withLeftDoor;
        hasCenterDoor = withCenterDoor;
        hasRightDoor = withRightDoor;
    }

    public bool IsLeftDoor()
    {
        return hasLeftDoor;
    }

    public bool IsCenterDoor()
    {
        return hasCenterDoor;
    }

    public bool IsRightDoor()
    {
        return hasRightDoor;
    }
}

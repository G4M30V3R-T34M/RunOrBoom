using FeTo.ObjectPool;
using UnityEngine;

public partial class TerrainGenerator
{
    private void AddFloor(Vector2 roomPosition)
    {
        PoolableObject floor = _floors.GetNext();

        floor.transform.position = roomPosition;
        floor.gameObject.SetActive(true);
    }

    private void AddWall(Vector2 roomPosition, Vector2Int direction)
    {
        PoolableObject wall = _walls.GetNext();
        DoAddLimitElement(wall, roomPosition, direction);
    }

    private void AddCorner(Vector2 roomPosition, Vector2Int direction)
    {
        PoolableObject corner = _corners.GetNext();
        DoAddLimitElement(corner, roomPosition, direction);
    }

    private void AddDoor(Vector2 roomPosition, Vector2Int direction)
    {
        PoolableObject door = _doors.GetNext();
        DoAddLimitElement(door, roomPosition, direction);
    }

    private void DoAddLimitElement(
        PoolableObject poolableObject,
        Vector2 roomPosition,
        Vector2Int direction)
    {

        float xPosition = roomPosition.x + direction.x * (_terrainConfig.roomSize / 2);
        float yPosition = roomPosition.y + direction.y * (_terrainConfig.roomSize / 2);
        Vector2 position = new(xPosition, yPosition);

        float rotation = (direction == Vector2Int.right || direction == Vector2Int.left) ? 90 : 0;

        poolableObject.transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
        poolableObject.transform.position = position;
        poolableObject.transform.Rotate(new Vector3(0, 0, rotation));
        poolableObject.gameObject.SetActive(true);
    }

}

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

    private void AddWall(Vector2 roomPosition, Vector2 direction)
    {
        PoolableObject wall = _walls.GetNext();

        float xPosition = roomPosition.x + direction.x * (_terrainConfig.roomSize / 2);
        float yPosition = roomPosition.y + direction.y * (_terrainConfig.roomSize / 2);
        Vector2 wallPosition = new(xPosition, yPosition);

        float rotation = (direction == Vector2.right || direction == Vector2.left) ? 90 : 0;

        wall.transform.position = wallPosition;
        wall.transform.Rotate(new Vector3(0, 0, rotation));
        wall.gameObject.SetActive(true);
    }

    private void AddCorner(Vector2 roomPosition, Vector2 direction)
    {
        PoolableObject corner = _corners.GetNext();

        float xPosition = roomPosition.x + direction.x * (_terrainConfig.roomSize / 2);
        float yPosition = roomPosition.y + direction.y * (_terrainConfig.roomSize / 2);
        Vector2 cornerPosition = new(xPosition, yPosition);

        corner.transform.position = cornerPosition;
        corner.gameObject.SetActive(true);
    }

}

using FeTo.ObjectPool;
using System;
using UnityEngine;

public class RoomElementsBuilder
{
    private ObjectPool _enemies;
    private ObjectPool _obstacles;
    private ObjectPool _secrets;
    private TerrainConfig _terrainConfig;

    public RoomElementsBuilder(
        ObjectPool enemies,
        ObjectPool obstacles,
        ObjectPool secrests,
        TerrainConfig terrainConfig
        )
    {
        _enemies = enemies;
        _obstacles = obstacles;
        _secrets = secrests;
        _terrainConfig = terrainConfig;
    }

    public void Build(
        Vector2 roomPosition,
        RoomElement[,] roomElements)
    {
        for (int x = 0; x < roomElements.GetLength(0); x++)
        {
            for (int y = 0; y < roomElements.GetLength(1); y++)
            {
                if (roomElements[x, y] != RoomElement.EMPTY)
                {
                    Vector2Int roomElementIndex = new(x, y);
                    BuildRoomElement(roomPosition, roomElementIndex, roomElements[x, y]);
                }
            }
        }
    }

    private void BuildRoomElement(
        Vector2 roomPosition,
        Vector2Int roomElementIndex,
        RoomElement roomElement)
    {
        Vector2 elementPosition = GetElementPosition(roomPosition, roomElementIndex);
        ObjectPool pool = GetPoolForRoomElement(roomElement);
        AddRoomElement(elementPosition, pool);
    }

    private Vector2 GetElementPosition(
        Vector2 roomPosition,
        Vector2Int roomElementIndex)
    {
        // If bottom left was 0, 0
        float cellSize = _terrainConfig.roomSize / _terrainConfig.roomDivisions;
        float halfCell = cellSize / 2;

        float xPosition = cellSize * roomElementIndex.x + halfCell;
        float yPosition = cellSize * roomElementIndex.y + halfCell;

        // 0, 0 is actually the center of the room
        float deltaAdjustment = _terrainConfig.roomSize / 2;

        xPosition -= deltaAdjustment;
        yPosition -= deltaAdjustment;

        // Add room position
        return new Vector2(
            roomPosition.x + xPosition,
            roomPosition.y + yPosition);
    }

    private ObjectPool GetPoolForRoomElement(RoomElement roomElement)
        => roomElement switch
        {
            RoomElement.SECRET_CODE => _secrets,
            RoomElement.OBSTACLE => _obstacles,
            RoomElement.ENEMY => _enemies,
            _ => throw new Exception()
        };

    private void AddRoomElement(
        Vector2 elementPosition,
        ObjectPool pool)
    {
        PoolableObject poolableObject = pool.GetNext();
        poolableObject.gameObject.transform.position = elementPosition;
        poolableObject.gameObject.SetActive(true);
    }
}

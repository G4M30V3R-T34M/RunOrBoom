using FeTo.ObjectPool;
using UnityEngine;

public partial class TerrainBuilder
{
    private ObjectPool _floors;
    private ObjectPool _corners;
    private ObjectPool _walls;
    private ObjectPool _doors;
    private TerrainConfig _terrainConfig;

    public TerrainBuilder(
        ObjectPool floors,
        ObjectPool corners,
        ObjectPool walls,
        ObjectPool doors,
        TerrainConfig terrainConfig)
    {
        _floors = floors;
        _corners = corners;
        _walls = walls;
        _doors = doors;
        _terrainConfig = terrainConfig;
    }

    public void BuildRoom(
        Vector2Int roomIndex,
        Vector2 roomPosition,
        Vector2Int startingRoomIndex,
        bool[,] roomGrid)
    {
        AdjacentRooms adjacentRooms = new AdjacentRooms(roomIndex, roomGrid);

        AddFloor(roomPosition);

        // Up and Right, add doors if required
        // Down and Left, leave that logic to "the connected room"
        CheckLimitsPrimary(Vector2Int.up, roomPosition, adjacentRooms);
        CheckLimitsPrimary(Vector2Int.right, roomPosition, adjacentRooms);
        CheckLimitsSecondary(Vector2Int.down, roomPosition, adjacentRooms);
        CheckLimitsSecondary(Vector2Int.left, roomPosition, adjacentRooms);

        CheckCorners(roomPosition, adjacentRooms);
    }


    private void CheckLimitsPrimary(
        Vector2Int direction,
        Vector2 roomPosition,
        AdjacentRooms adjacentRooms)
    {
        if (adjacentRooms.HasRoomInDirection(direction))
        {
            if (ShouldAddDoor())
            {
                AddDoor(roomPosition, direction);
            } // Else : leave open, it's a big room
        }
        else
        {
            AddWall(roomPosition, direction);
        }
    }

    private bool ShouldAddDoor()
        => (Random.Range(0, 100) < _terrainConfig.doorChance);

    private void CheckLimitsSecondary(
        Vector2Int direction,
        Vector2 roomPosition,
        AdjacentRooms adjacentRooms)
    {
        if (adjacentRooms.HasRoomInDirection(direction))
        {
            // DO NOTHING
            // Already managed on the other room
        }
        else
        {
            AddWall(roomPosition, direction);
        }
    }

    private void CheckCorners(Vector2 roomPosition, AdjacentRooms adjacentRooms)
    {
        AddCorner(roomPosition, Vector2Int.up + Vector2Int.right);

        if (!adjacentRooms.Left)
        {
            AddCorner(roomPosition, Vector2Int.up + Vector2Int.left);
        }

        if (!adjacentRooms.Down)
        {
            AddCorner(roomPosition, Vector2Int.down + Vector2Int.right);
        }

        if (!adjacentRooms.Left && !adjacentRooms.Down && !adjacentRooms.DownLeft)
        {
            AddCorner(roomPosition, Vector2Int.down + Vector2Int.left);
        }
    }
}

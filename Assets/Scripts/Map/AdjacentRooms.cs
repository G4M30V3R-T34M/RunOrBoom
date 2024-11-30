using UnityEngine;

public class AdjacentRooms
{
    private readonly Vector2Int directionDownLeft = new(-1, -1);

    public bool Up { get; set; }
    public bool Right { get; set; }
    public bool Down { get; set; }
    public bool Left { get; set; }
    public bool DownLeft { get; set; }

    public AdjacentRooms(Vector2Int roomIndex, bool[,] roomGrid)
    {
        Up = HasRoomInDirection(roomIndex, Vector2Int.up, roomGrid);
        Right = HasRoomInDirection(roomIndex, Vector2Int.right, roomGrid);
        Down = HasRoomInDirection(roomIndex, Vector2Int.down, roomGrid);
        Left = HasRoomInDirection(roomIndex, Vector2Int.left, roomGrid);
        DownLeft = HasRoomInDirection(roomIndex, directionDownLeft, roomGrid);
    }

    private bool HasRoomInDirection(
        Vector2Int roomIndex,
        Vector2Int direction,
        bool[,] roomGrid)
    {
        if (IsOutOfGrid(roomIndex, direction, roomGrid))
        {
            return false;
        }

        int newX = roomIndex.x + direction.x;
        int newY = roomIndex.y + direction.y;
        return roomGrid[newX, newY];
    }

    private bool IsOutOfGrid(
        Vector2Int roomIndex,
        Vector2Int direction,
        bool[,] roomGrid)
        => direction == Vector2Int.up && roomIndex.y == roomGrid.GetLength(1) - 1
            || direction == Vector2Int.down && roomIndex.y == 0
            || direction == Vector2Int.right && roomIndex.x == roomGrid.GetLength(0) - 1
            || direction == Vector2Int.left && roomIndex.x == 0
            || direction == directionDownLeft && (roomIndex.y == 0 || roomIndex.x == 0);

    public bool HasRoomInDirection(Vector2Int direction)
    {
        if (direction == Vector2Int.up)
        {
            return Up;
        }
        if (direction == Vector2Int.right)
        {
            return Right;
        }
        if (direction == Vector2Int.down)
        {
            return Down;
        }
        if (direction == Vector2Int.left)
        {
            return Left;
        }
        if (direction == (Vector2Int.down + Vector2Int.left))
        {
            return DownLeft;
        }
        return false; // should never happen
    }
}

using UnityEngine;

public class AdjacentRooms
{
    public bool Up { get; set; }
    public bool Right { get; set; }
    public bool Down { get; set; }
    public bool Left { get; set; }
    public bool DownLeft { get; set; }

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

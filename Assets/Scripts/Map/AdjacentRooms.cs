using UnityEngine;

public class AdjacentRooms
{
    public bool Up { get; set; }
    public bool Right { get; set; }
    public bool Down { get; set; }
    public bool Left { get; set; }

    public bool HasRoomInDirection(Vector2 direction)
    {
        if (direction == Vector2.up)
        {
            return Up;
        }
        if (direction == Vector2.right)
        {
            return Right;
        }
        if (direction == Vector2.down)
        {
            return Down;
        }
        if (direction == Vector2.left)
        {
            return Left;
        }
        return false; // should never happen
    }
}

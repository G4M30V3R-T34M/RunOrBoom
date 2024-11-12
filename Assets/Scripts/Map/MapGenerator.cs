using UnityEngine;

public class MapGenerator
{
    private int _rooms = 0;

    public MapGenerator(int rooms)
    {
        _rooms = rooms;
    }

    public bool[][] Generate()
    {
        int gridSize = (int)Mathf.Ceil(_rooms + 1 / 2);
        bool[][] roomGrid = new bool[gridSize][];
        for (int i = 0; i < gridSize; i++)
        {
            roomGrid[i] = new bool[gridSize];
        }

        PlaceFirstRoom(gridSize, ref roomGrid);

        int placedRooms = 1;
        while (placedRooms < _rooms)
        {
            if (TryPlaceRoom(placedRooms, ref roomGrid))
            {
                placedRooms++;
            }
        }

        return roomGrid;
    }

    private static void PlaceFirstRoom(int gridSize, ref bool[][] roomGrid)
    {
        int xValue = Random.Range(0, gridSize);
        int yValue = Random.Range(0, gridSize);
        roomGrid[xValue][yValue] = true;
    }

    private bool TryPlaceRoom(int placedRooms, ref bool[][] roomGrid)
    {
        Vector2 existingRoom = GetRandomRoom(placedRooms, ref roomGrid);
        return TryAddAjdacentRoom(existingRoom, ref roomGrid);
    }
    private Vector2 GetRandomRoom(int placedRooms, ref bool[][] roomGrid)
    {
        int selectedRoom = Random.Range(0, placedRooms);

        for (int i = 0; i < roomGrid.Length; i++)
        {
            for (int j = 0; j < roomGrid[i].Length; j++)
            {
                if (roomGrid[i][j] && selectedRoom == 0)
                {
                    return new Vector2(i, j);
                }
                else
                {
                    selectedRoom--;
                }
            }
        }
        throw new RoomNotFoundException();
    }

    private bool TryAddAjdacentRoom(Vector2 existingRoom, ref bool[][] roomGrid)
    {
        int startingDirection = Random.Range(0, 4);
        int nextDirection = startingDirection;

        do
        {
            Vector2 direction = GetDirectionAsVector(nextDirection);
            if (IsValidDirection(direction, existingRoom, roomGrid))
            {
                PlaceAdjacentRoom(direction, existingRoom, ref roomGrid);
                return true;
            }

            nextDirection = (nextDirection + 1) % 4;
        } while (nextDirection != startingDirection);

        return false;
    }

    private Vector2 GetDirectionAsVector(int nextDirection) => nextDirection switch
    {
        0 => Vector2.up,
        1 => Vector2.right,
        2 => Vector2.down,
        3 => Vector2.left,
        _ => Vector2.zero, // should never happen
    };

    private bool IsValidDirection(Vector2 direction, Vector2 room, bool[][] roomGrid)
    {
        // out of bounds
        if (room.x == 0 && direction.x == -1 ||
            room.x == roomGrid.Length - 1 && direction.x == 1 ||
            room.y == 0 && direction.y == -1 ||
            room.y == roomGrid.Length - 1 && direction.y == 1)
        {
            return false;
        }

        // room already placed there
        int newRoomX = (int)(room.x + direction.x);
        int newRoomY = (int)(room.y + direction.y);
        return !(roomGrid[newRoomX][newRoomY]);
    }

    private void PlaceAdjacentRoom(Vector2 direction, Vector2 room, ref bool[][] roomGrid)
    {
        int newRoomX = (int)(room.x + direction.x);
        int newRoomY = (int)(room.y + direction.y);
        roomGrid[newRoomX][newRoomY] = true;
    }
}

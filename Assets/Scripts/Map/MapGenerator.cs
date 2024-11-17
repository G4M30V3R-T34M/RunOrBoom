using UnityEngine;

public class MapGenerator
{
    private int _rooms = 0;

    public MapGenerator(int rooms)
    {
        _rooms = rooms;
    }

    public bool[,] Generate()
    {
        int gridSize = (int)Mathf.Ceil(_rooms + 1 / 2);
        bool[,] roomGrid = new bool[gridSize, gridSize];

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

    private static void PlaceFirstRoom(int gridSize, ref bool[,] roomGrid)
    {
        int xValue = Random.Range(0, gridSize);
        int yValue = Random.Range(0, gridSize);
        roomGrid[xValue, yValue] = true;
    }

    private bool TryPlaceRoom(int placedRooms, ref bool[,] roomGrid)
    {
        Vector2Int existingRoom = GetRandomRoom(placedRooms, ref roomGrid);
        return TryAddAjdacentRoom(existingRoom, ref roomGrid);
    }

    private Vector2Int GetRandomRoom(int placedRooms, ref bool[,] roomGrid)
    {
        int selectedRoom = Random.Range(0, placedRooms);

        for (int i = 0; i < roomGrid.GetLength(0); i++)
        {
            for (int j = 0; j < roomGrid.GetLength(1); j++)
            {
                if (roomGrid[i, j])
                {
                    if (selectedRoom == 0)
                    {
                        return new Vector2Int(i, j);
                    }
                    else
                    {
                        selectedRoom--;
                    }
                }
            }
        }
        throw new RoomNotFoundException();
    }

    private bool TryAddAjdacentRoom(Vector2Int existingRoom, ref bool[,] roomGrid)
    {
        int startingDirection = Random.Range(0, 4);
        int nextDirection = startingDirection;

        do
        {
            Vector2Int direction = GetDirectionAsVector(nextDirection);
            if (IsValidDirection(direction, existingRoom, roomGrid))
            {
                PlaceAdjacentRoom(direction, existingRoom, ref roomGrid);
                return true;
            }

            nextDirection = (nextDirection + 1) % 4;
        } while (nextDirection != startingDirection);

        return false;
    }

    private Vector2Int GetDirectionAsVector(int nextDirection) => nextDirection switch
    {
        0 => Vector2Int.up,
        1 => Vector2Int.right,
        2 => Vector2Int.down,
        3 => Vector2Int.left,
        _ => Vector2Int.zero, // should never happen
    };

    private bool IsValidDirection(Vector2Int direction, Vector2Int room, bool[,] roomGrid)
    {
        // out of bounds
        if (room.x == 0 && direction.x == -1 ||
            room.x == roomGrid.GetLength(0) - 1 && direction.x == 1 ||
            room.y == 0 && direction.y == -1 ||
            room.y == roomGrid.GetLength(1) - 1 && direction.y == 1)
        {
            return false;
        }

        // room already placed there
        int newRoomX = (room.x + direction.x);
        int newRoomY = (room.y + direction.y);
        return !(roomGrid[newRoomX, newRoomY]);
    }

    private void PlaceAdjacentRoom(Vector2 direction, Vector2 room, ref bool[,] roomGrid)
    {
        int newRoomX = (int)(room.x + direction.x);
        int newRoomY = (int)(room.y + direction.y);
        roomGrid[newRoomX, newRoomY] = true;
    }
}

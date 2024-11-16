using FeTo.ObjectPool;
using UnityEngine;
using Random = UnityEngine.Random;

public partial class TerrainGenerator : MonoBehaviour
{
    [Header("Configuration")]
    [SerializeField] private TerrainConfig _terrainConfig;

    [Header("Map Elements")]
    [SerializeField] private ObjectPool _floors;
    [SerializeField] private ObjectPool _corners;
    [SerializeField] private ObjectPool _walls;
    [SerializeField] private ObjectPool _doors;
    [SerializeField] private ObjectPool _enemies;
    [SerializeField] private ObjectPool _obstacles; // tables
    [SerializeField] private ObjectPool _secretCodes;
    [SerializeField] private ObjectPool _weapons; // ¿Weapons on the floor?

    private bool[,] _roomGrid;
    private Vector2 _startingRoom;

    private int _placedSecredCodes = 0;
    private int _generatedRooms = 0;

    private void Awake()
    {
        MapGenerator _mapGenerator = new(_terrainConfig.numberOfRooms);
        _roomGrid = _mapGenerator.Generate();
        _startingRoom = SelectPlayerStartingRoom();
    }

    private void Start()
    {
        for (int x = 0; x < _roomGrid.GetLength(0); x++)
        {
            for (int y = 0; y < _roomGrid.GetLength(1); y++)
            {
                if (_roomGrid[x, y])
                {
                    GenerateRoom(new Vector2(x, y));
                }
            }
        }
    }

    private Vector2 SelectPlayerStartingRoom()
    {
        int roomIndex = Random.Range(0, _terrainConfig.numberOfRooms);
        for (int x = 0; x < _roomGrid.GetLength(0); x++)
        {
            for (int y = 0; y < _roomGrid.GetLength(1); y++)
            {
                if (_roomGrid[x, y])
                {
                    if (roomIndex == 0)
                    {
                        return new Vector2(x, y);
                    }
                    else
                    {
                        roomIndex--;
                    }
                }
            }
        }
        // Should hever happen
        return new Vector2(0, 0);
    }

    private void GenerateRoom(Vector2 roomIndex)
    {
        AdjacentRooms adjacentRooms = new()
        {
            Up = HasRoomInDirection(roomIndex, Vector2.up),
            Right = HasRoomInDirection(roomIndex, Vector2.right),
            Down = HasRoomInDirection(roomIndex, Vector2.down),
            Left = HasRoomInDirection(roomIndex, Vector2.left),
        };

        float xPosition = (_startingRoom.x - roomIndex.x) * _terrainConfig.roomSize;
        float yPosition = (_startingRoom.y - roomIndex.y) * _terrainConfig.roomSize;
        Vector2 roomPosition = new(xPosition, yPosition);

        AddFloor(roomPosition);

        // Up and Right, add doors if required
        // Down and Left, leave that logic to "the connected room"
        CheckLimitsPrimary(Vector2.up, roomPosition, adjacentRooms);
        CheckLimitsPrimary(Vector2.right, roomPosition, adjacentRooms);
        CheckLimitsSecondary(Vector2.down, roomPosition, adjacentRooms);
        CheckLimitsSecondary(Vector2.left, roomPosition, adjacentRooms);

        CheckCorners(roomPosition, adjacentRooms);

        GenerateRoomElements(roomPosition, roomIndex == _startingRoom);
    }

    private bool HasRoomInDirection(Vector2 roomIndex, Vector2 direction)
    {
        if (IsOutOfGrid(roomIndex, direction))
        {
            return false;
        }

        int newX = (int)(roomIndex.x + direction.x);
        int newY = (int)(roomIndex.y + direction.y);
        return _roomGrid[newX, newY];
    }

    private bool IsOutOfGrid(Vector2 roomIndex, Vector2 direction) =>
        direction == Vector2.up && (int)roomIndex.x == _roomGrid.GetLength(0) - 1 ||
        direction == Vector2.down && (int)roomIndex.x == 0 ||
        direction == Vector2.right && (int)roomIndex.y == _roomGrid.GetLength(1) - 1 ||
        direction == Vector2.left && (int)roomIndex.y == 0;

    private void CheckLimitsPrimary(
        Vector2 direction,
        Vector2 roomPosition,
        AdjacentRooms adjacentRooms)
    {
        if (adjacentRooms.HasRoomInDirection(direction))
        {
            if (ShouldAddDoor())
            {
                // Add Door
            } // Else : leave open, it's a big room
        }
        else
        {
            AddWall(roomPosition, Vector2.up);
        }
    }

    private bool ShouldAddDoor()
        => (Random.Range(0, 100) < _terrainConfig.doorChance);

    private void CheckLimitsSecondary(
        Vector2 direction,
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
            AddWall(roomPosition, Vector2.up);
        }
    }

    private void CheckCorners(Vector2 roomPosition, AdjacentRooms adjacentRooms)
    {
        AddCorner(roomPosition, Vector2.up + Vector2.right);

        if (!adjacentRooms.Left)
        {
            AddCorner(roomPosition, Vector2.up + Vector2.left);
        }

        if (!adjacentRooms.Down)
        {
            AddCorner(roomPosition, Vector2.down + Vector2.right);
        }

        if (!adjacentRooms.Left && !adjacentRooms.Down)
        {
            AddCorner(roomPosition, Vector2.down + Vector2.left);
        }
    }

    private void GenerateRoomElements(Vector2 roomPosition, bool isPlayerStartRoom)
    {
        // StartingRoom shouldn't have secrets nor enemies

        int[,] roomParts = new int[4, 4];

        // If should have secret
        // 

        // Obstacles
        // Secrets
        // Enemies

    }
}

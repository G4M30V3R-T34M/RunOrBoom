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

    private bool[,] _roomGrid;
    private Vector2 _startingRoom;
    //private List<(Vector2Int, Vector2Int)> _extraWalls;

    private int _placedSecredCodes = 0;
    private int _generatedRooms = 0;

    RoomElementsGenerator _roomElementsGenerator;
    MapGenerator _mapGenerator;

    private void Awake()
    {
        _roomElementsGenerator = new(_terrainConfig);
        _mapGenerator = new(_terrainConfig.numberOfRooms);
    }

    private void Start()
    {
        _roomGrid = _mapGenerator.Generate();
        _startingRoom = SelectPlayerStartingRoom();

        // Currently extra walls are not used
        /*
        WallGenerator wallGenerator = new(_roomGrid);
        _extraWalls = wallGenerator.GenerateWalls();
        */

        GenerateRooms();
    }

    private void GenerateRooms()
    {
        for (int x = 0; x < _roomGrid.GetLength(0); x++)
        {
            for (int y = 0; y < _roomGrid.GetLength(1); y++)
            {
                if (_roomGrid[x, y])
                {
                    GenerateRoom(new Vector2Int(x, y));
                }
            }
        }
    }

    private Vector2Int SelectPlayerStartingRoom()
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
                        return new Vector2Int(x, y);
                    }
                    else
                    {
                        roomIndex--;
                    }
                }
            }
        }
        // Should hever happen
        return new Vector2Int(-1, -1);
    }

    private void GenerateRoom(Vector2Int roomIndex)
    {
        AdjacentRooms adjacentRooms = new()
        {
            Up = HasRoomInDirection(roomIndex, Vector2Int.up),
            Right = HasRoomInDirection(roomIndex, Vector2Int.right),
            Down = HasRoomInDirection(roomIndex, Vector2Int.down),
            Left = HasRoomInDirection(roomIndex, Vector2Int.left),
            DownLeft = HasRoomInDirection(roomIndex, Vector2Int.down + Vector2Int.left)
        };

        float xPosition = (roomIndex.x - _startingRoom.x) * _terrainConfig.roomSize;
        float yPosition = (roomIndex.y - _startingRoom.y) * _terrainConfig.roomSize;
        Vector2 roomPosition = new(xPosition, yPosition);

        AddFloor(roomPosition);

        // Up and Right, add doors if required
        // Down and Left, leave that logic to "the connected room"
        CheckLimitsPrimary(Vector2Int.up, roomPosition, adjacentRooms);
        CheckLimitsPrimary(Vector2Int.right, roomPosition, adjacentRooms);
        CheckLimitsSecondary(Vector2Int.down, roomPosition, adjacentRooms);
        CheckLimitsSecondary(Vector2Int.left, roomPosition, adjacentRooms);

        CheckCorners(roomPosition, adjacentRooms);

        GenerateRoomElements(roomPosition, roomIndex == _startingRoom);
    }

    private bool HasRoomInDirection(Vector2Int roomIndex, Vector2Int direction)
    {
        if (IsOutOfGrid(roomIndex, direction))
        {
            return false;
        }

        int newX = roomIndex.x + direction.x;
        int newY = roomIndex.y + direction.y;
        return _roomGrid[newX, newY];
    }

    private readonly Vector2Int directionDownLeft = new(-1, -1);
    private bool IsOutOfGrid(Vector2Int roomIndex, Vector2Int direction)
        => direction == Vector2Int.up && roomIndex.y == _roomGrid.GetLength(1) - 1
            || direction == Vector2Int.down && roomIndex.y == 0
            || direction == Vector2Int.right && roomIndex.x == _roomGrid.GetLength(0) - 1
            || direction == Vector2Int.left && roomIndex.x == 0
            || direction == directionDownLeft && (roomIndex.y == 0 || roomIndex.x == 0);

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

    private void GenerateRoomElements(
        Vector2 roomPosition,
        bool isPlayerStartRoom)
        => _roomElementsGenerator.GenerateRoom(
            ref _placedSecredCodes,
            _generatedRooms,
            isPlayerStartRoom);
}

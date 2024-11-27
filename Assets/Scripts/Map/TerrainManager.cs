using FeTo.ObjectPool;
using UnityEngine;
using Random = UnityEngine.Random;

public class TerrainManager : MonoBehaviour
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
    private Vector2Int _startingRoom;
    //private List<(Vector2Int, Vector2Int)> _extraWalls;

    private int _placedSecredCodes = 0;
    private int _generatedRooms = 0;

    MapGenerator _mapGenerator;
    TerrainBuilder _terrainBuilder;
    RoomElementsGenerator _roomElementsGenerator;
    RoomElementsBuilder _roomElementsBuilder;

    private void Awake()
    {
        _mapGenerator = new(_terrainConfig.numberOfRooms);
        _terrainBuilder = new(_floors, _corners, _walls, _doors, _terrainConfig);
        _roomElementsGenerator = new(_terrainConfig);
        _roomElementsBuilder = new(_enemies, _obstacles, _secretCodes, _terrainConfig);
    }

    private void Start()
    {
        _roomGrid = _mapGenerator.Generate();
        _startingRoom = SelectPlayerStartingRoom();

        GenerateRooms();
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

    private void GenerateRoom(Vector2Int roomIndex)
    {
        Vector2 roomPosition = GetRoomPosition(ref roomIndex);

        _terrainBuilder.BuildRoom(
            roomIndex,
            roomPosition,
            _startingRoom,
            _roomGrid);

        RoomElement[,] roomElements = _roomElementsGenerator.Generate(
            ref _placedSecredCodes,
            _generatedRooms,
            roomIndex == _startingRoom);

        _roomElementsBuilder.Build(roomPosition, roomElements);

        _generatedRooms++;
    }

    private Vector2 GetRoomPosition(ref Vector2Int roomIndex)
    {
        float xPosition = (roomIndex.x - _startingRoom.x) * _terrainConfig.roomSize;
        float yPosition = (roomIndex.y - _startingRoom.y) * _terrainConfig.roomSize;
        return new(xPosition, yPosition);
    }
}

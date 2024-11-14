using FeTo.ObjectPool;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
    [Header("Configuration")]
    [SerializeField]
    private int _roomCount;

    [SerializeField]
    private int _secretCodesCount;

    [Header("Map Elements")]
    [SerializeField]
    private ObjectPool _floors;

    [SerializeField]
    private ObjectPool _walls;

    [SerializeField]
    private ObjectPool _doors;

    [SerializeField]
    private ObjectPool _enemies;

    [SerializeField]
    private ObjectPool _obstacles; // doors

    [SerializeField]
    private ObjectPool _secretCodes;

    [SerializeField]
    private ObjectPool _weapons; // ¿Weapons on the floor?

    private bool[,] _roomGrid;

    private void Awake()
    {
        MapGenerator _mapGenerator = new(_roomCount);
        _roomGrid = _mapGenerator.Generate();
    }

    private void Start()
    {
        for (int i = 0; i < _roomCount; i++)
        {
            for (int j = 0; j < _roomCount; j++)
            {
                if (_roomGrid[i, j])
                {
                    GenerateRoom(i, j);
                }
            }
        }
    }

    private void GenerateRoom(int i, int j)
    {
        // Generate room limits (up - right - down left)

        // Generate room elements
    }
}

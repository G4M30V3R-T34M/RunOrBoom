using FeTo.ObjectPool;
using UnityEngine;

public class RoomElementsGenerator
{
    private ObjectPool _enemies;
    private ObjectPool _obstacles;
    private ObjectPool _secretCodes;
    private TerrainConfig _terrainConfig;

    const int ROOM_SIZE = 4;

    public RoomElementsGenerator(
        ObjectPool enemies,
        ObjectPool obstacles,
        ObjectPool secretCodes,
        TerrainConfig terrainConfig)
    {
        _enemies = enemies;
        _obstacles = obstacles;
        _secretCodes = secretCodes;
        _terrainConfig = terrainConfig;
    }

    public void GenerateRooms(
        Vector2 roomPosition,
        ref int placedSecretCodes,
        int generatedRooms,
        bool isPlayerStartingRoom)
    {
        RoomElement[,] roomElements = new RoomElement[ROOM_SIZE, ROOM_SIZE];

        if (!isPlayerStartingRoom)
        {
            GenerateSecretCodes(roomPosition, generatedRooms, ref placedSecretCodes, ref roomElements);
            GenerateEnemies(roomPosition, ref roomElements);
        }

        GenerateObstacles(roomPosition, roomElements);
    }

    private void GenerateSecretCodes(
        Vector2 roomPosition,
        int generatedRooms,
        ref int placedSecretCodes,
        ref RoomElement[,] roomElements)
    {
        int remainingSecrets = _terrainConfig.numberSecrets - placedSecretCodes;
        int remainingRooms = _terrainConfig.numberOfRooms - generatedRooms;
        float secretChance = remainingSecrets / remainingRooms;

        if (Random.Range(0f, 1f) <= secretChance)
        {
            // TODO: PlaceSecret
            placedSecretCodes++;
        }
    }

    private void GenerateEnemies(
        Vector2 roomPosition,
        ref RoomElement[,] roomElements)
    {
        GenerateEnemy(roomPosition, ref roomElements);
        if (Random.Range(0, 100) < _terrainConfig.extraEnemyChance)
        {
            GenerateEnemy(roomPosition, ref roomElements);
        }
    }

    private void GenerateEnemy(
        Vector2 roomPosition,
        ref RoomElement[,] roomElements)
    {

    }

    private void GenerateObstacles(
        Vector2 roomPosition,
        RoomElement[,] roomElements)
    {
        if (Random.Range(0, 100) < _terrainConfig.obstacleChance)
        {
            Vector2 obstaclePosition = FindRandomFreeRoomPosition(roomElements);
            roomElements[(int)obstaclePosition.x, (int)obstaclePosition.y] = RoomElement.OBSTACLE;
            // TODO: ADD OBSTACLE
        }
    }

    private Vector2 FindRandomFreeRoomPosition(RoomElement[,] roomElements)
    {
        int startingX = Random.Range(0, roomElements.Length);
        int startingY = Random.Range(0, roomElements.Length);

        int x = startingX;
        int y = startingY;
        bool placeFound = false;

        do
        {
            if (roomElements[x, y] == RoomElement.EMPTY)
            {
                placeFound = true;
            }
            else
            {
                NextPosition(ref x, ref y, roomElements.Length);
            }
        } while (!placeFound && !(x == startingX && y == startingY));

        return new Vector2(x, y);
    }

    private void NextPosition(ref int x, ref int y, int length)
    {
        y++;
        if (y >= length)
        {
            y = 0;
            x++;
            x %= length;
        }
    }
}

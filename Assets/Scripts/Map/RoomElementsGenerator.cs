using UnityEngine;

public class RoomElementsGenerator
{
    private TerrainConfig _terrainConfig;

    public RoomElementsGenerator(
        TerrainConfig terrainConfig)
    {
        _terrainConfig = terrainConfig;
    }

    public RoomElement[,] GenerateRoom(
        ref int placedSecretCodes,
        int generatedRooms,
        bool isPlayerStartingRoom)
    {
        RoomElement[,] roomElements = new RoomElement[_terrainConfig.roomDivisions, _terrainConfig.roomDivisions];

        if (!isPlayerStartingRoom)
        {
            GenerateSecretCodes(generatedRooms, ref placedSecretCodes, ref roomElements);
            GenerateEnemies(ref roomElements);
        }

        GenerateObstacles(roomElements);

        return roomElements;
    }

    private void GenerateSecretCodes(
        int generatedRooms,
        ref int placedSecretCodes,
        ref RoomElement[,] roomElements)
    {
        int remainingSecrets = _terrainConfig.numberSecrets - placedSecretCodes;
        int remainingRooms = _terrainConfig.numberOfRooms - generatedRooms;
        float secretChance = remainingSecrets / remainingRooms;

        if (Random.Range(0f, 1f) <= secretChance)
        {
            Vector2Int coordinates = FindRandomFreeRoomPosition(roomElements);
            roomElements[coordinates.x, coordinates.y] = RoomElement.SECRET_CODE;
            placedSecretCodes++;
        }
    }

    private void GenerateEnemies(
        ref RoomElement[,] roomElements)
    {
        if (Random.Range(0, 100) < _terrainConfig.enemyChance)
        {
            GenerateEnemy(ref roomElements);

            if (Random.Range(0, 100) < _terrainConfig.extraEnemyChance)
            {
                GenerateEnemy(ref roomElements);
            }
        }
    }

    private void GenerateEnemy(
        ref RoomElement[,] roomElements)
    {
        Vector2Int enemyCoordinates = FindRandomFreeRoomPosition(roomElements);
        roomElements[enemyCoordinates.x, enemyCoordinates.y] = RoomElement.ENEMY;
    }

    private void GenerateObstacles(
        RoomElement[,] roomElements)
    {
        if (Random.Range(0, 100) < _terrainConfig.obstacleChance)
        {
            Vector2 obstaclePosition = FindRandomFreeRoomPosition(roomElements);
            roomElements[(int)obstaclePosition.x, (int)obstaclePosition.y] = RoomElement.OBSTACLE;
        }
    }

    private Vector2Int FindRandomFreeRoomPosition(RoomElement[,] roomElements)
    {
        int startingX = Random.Range(0, roomElements.GetLength(0));
        int startingY = Random.Range(0, roomElements.GetLength(1));

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
                NextPosition(ref x, ref y, roomElements.GetLength(0));
            }
        } while (!placeFound && !(x == startingX && y == startingY));

        return new Vector2Int(x, y);
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

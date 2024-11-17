using System.Collections.Generic;
using UnityEngine;

internal class WallGenerator
{
    private bool[,] roomGrid;

    public WallGenerator(bool[,] roomGrid)
    {
        this.roomGrid = roomGrid;
    }

    internal List<(Vector2Int, Vector2Int)> GenerateWalls()
    {
        // To develop in further iterations
        // Currently no extra walls will be added

        /*
        List<(Vector2, Vector2)> walls = new();
        bool wallAdded = false;
        do
        {
            wallAdded = TryAddWall(ref walls);
        } while (wallAdded);

        */
        return null;
    }

    private bool TryAddWall(ref List<(Vector2, Vector2)> walls)
    {
        // GetRandomRoomWith2+Adjacents
        // DoAddWall
        // IfAnyRoomIsIsolated
        // + RemoveLastAddedWall
        // ** Repeat until all rooms checked or a wall was added

        return false;
    }
}

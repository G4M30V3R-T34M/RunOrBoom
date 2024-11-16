using UnityEngine;

[CreateAssetMenu(fileName = "TerrainCongif", menuName = "Scriptable Objects/TerrainCongif")]
public class TerrainConfig : ScriptableObject
{
    [Header("General Configuration")]
    [Min(1)] public int numberOfRooms;
    [Min(1)] public int numberSecrets;

    [Header("Size")]
    public float roomSize;

    [Header("Random Generation")]
    [Range(0, 100)] public int doorChance;
    [Range(0, 100)] public int enemyChance;
    [Range(0, 100)] public int extraEnemyChance;
    [Range(0, 100)] public int obstacleChance;
}

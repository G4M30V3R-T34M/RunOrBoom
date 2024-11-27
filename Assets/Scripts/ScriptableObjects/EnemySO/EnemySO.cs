using UnityEngine;

[CreateAssetMenu(fileName = "EnemySO", menuName = "Scriptable Objects/EnemySO")]
public class EnemySO : ScriptableObject
{
    [Header("Enemy health")]
    public float health;

    [Header("Patrol settings")]
    public float rotationSpeed;

    [Header("Vision settings")]
    public float visionRange;
    public Layer[] detectionLayers;
    public Layer targetLayer;
}

using UnityEngine;

[CreateAssetMenu(fileName = "GunScriptableObject", menuName = "Scriptable Objects/GunScriptableObject")]
public class GunSO : ScriptableObject
{
    [Header("Time configurations")]
    public float shotCooldown;
    public float timeToAim;

    [Header("Distance configurations")]
    public float range;

    [Header("Layer configurations")]
    public Layer enemyLayer;
    public Layer[] collisionLayers;
    public int collisionLayerMask;

    [Header("Visual configurations")]
    public Sprite gunSprite;
}

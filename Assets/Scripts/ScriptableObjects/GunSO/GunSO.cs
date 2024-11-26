using UnityEngine;

[CreateAssetMenu(fileName = "GunScriptableObject", menuName = "Scriptable Objects/GunScriptableObject")]
public class GunSO : ScriptableObject
{
    public bool isDefault;
    [Header("Damage")]
    public float damage;

    [Header("Time configurations")]
    public float aimingTime;
    public float reactionTime;

    [Header("Distance configurations")]
    public float range;

    [Header("Layer configurations")]
    public Layer targetLayer;
    public Layer[] collisionLayers;
    public int collisionLayerMask;

    [Header("Visual configurations")]
    public Sprite gunSprite;
    public Sprite gunPickUpSprite;

    [Header("Ammunition configuration")]
    public int ammunition;
}

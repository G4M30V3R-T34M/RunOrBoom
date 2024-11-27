using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSO", menuName = "Scriptable Objects/PlayerSO")]
public class PlayerSO : ScriptableObject
{
    [Header("Player movement")]
    public float speed;

    [Header("Player slow down")]
    public float slowDownDuration;
    public float slowDownValue;
}

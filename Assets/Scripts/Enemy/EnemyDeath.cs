using UnityEngine;

public class EnemyDeath : MonoBehaviour
{
    public void SpawnDeadBody() => CorpseManager.Instance.SpawnCorpse(gameObject.transform.position);

    public void TryToSpawnLoot() => DropManager.Instance.TryToSpawnLoot(gameObject.transform.position);
}

using FeTo.ObjectPool;
using FeTo.Singleton;
using UnityEngine;

public class CorpseManager : Singleton<CorpseManager>
{
    [Header("Object Pool")]
    [SerializeField] ObjectPool corpsePool;
    public void SpawnCorpse(Vector2 spawnPosition)
    {
        EnemyCorpse enemyCorpse = (EnemyCorpse)corpsePool.GetNext();
        enemyCorpse.transform.position = spawnPosition;
        enemyCorpse.transform.rotation = Quaternion.Euler(0f, 0f, Random.Range(0f, 360f));
        enemyCorpse.gameObject.SetActive(true);
    }
}

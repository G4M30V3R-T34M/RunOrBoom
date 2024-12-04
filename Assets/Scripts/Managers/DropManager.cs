using FeTo.ObjectPool;
using FeTo.Singleton;
using UnityEngine;

public class DropManager : Singleton<DropManager>
{
    [Header("Loot settings")]
    [SerializeField] GunSO[] lootGuns;
    [SerializeField][Range(0, 100)] int spawnLootProbability;

    [Header("Object Pool")]
    [SerializeField] ObjectPool itemsPool;
    public void TryToSpawnLoot(Vector2 spawnPosition)
    {
        int randomProbability = Random.Range(0, 100);

        if (randomProbability > spawnLootProbability)
        {
            return;
        }

        SpawnLoot(spawnPosition);
    }

    private void SpawnLoot(Vector2 spawnPosition)
    {
        int randomIndex = Random.Range(0, lootGuns.Length);
        GunPickUp pickable = (GunPickUp)itemsPool.GetNext();
        pickable.gun = lootGuns[randomIndex];
        pickable.transform.position = spawnPosition;
        pickable.gameObject.SetActive(true);
    }
}

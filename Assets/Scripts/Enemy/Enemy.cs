using FeTo.ObjectPool;
using UnityEngine;

public class Enemy : PoolableObject
{
    [SerializeField] EnemySO enemySettings;
    private HealthManager healthManager;

    private void Awake() => healthManager = GetComponent<HealthManager>();

    private void OnEnable() => healthManager.SetUp(enemySettings.health);

    public void Die() => gameObject.SetActive(false);
}

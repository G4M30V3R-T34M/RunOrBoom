using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] EnemySO enemySettings;
    private HealthManager healthManager;

    private void Awake() => healthManager = GetComponent<HealthManager>();

    private void OnEnable() => healthManager.SetUp(enemySettings.health);

    public void Die() => gameObject.SetActive(false);
}

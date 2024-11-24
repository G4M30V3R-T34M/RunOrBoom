using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float health;
    private HealthManager healthManager;

    private void Awake() => healthManager = GetComponent<HealthManager>();

    private void OnEnable() => healthManager.SetUp(health);

    public void Die() => gameObject.SetActive(false);
}

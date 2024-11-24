using UnityEngine;

public abstract class HealthManager : MonoBehaviour
{

    protected float health;

    public void SetUp(float initialHealth) => health = initialHealth;

    public float GetCurrentHealth() => health;

    public abstract void TakeDamage(float damage);
}

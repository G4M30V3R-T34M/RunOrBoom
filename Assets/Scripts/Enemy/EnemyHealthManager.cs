using UnityEngine.Events;

public class EnemyHealthManager : HealthManager
{
    public UnityEvent NoHealth;

    public override void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0 && NoHealth != null)
        {
            NoHealth.Invoke();
        }
    }
}

using FeTo.SOArchitecture;
using UnityEngine.Events;

public class EnemyHealthManager : HealthManager
{
    public delegate void NoHealthAction();
    public UnityEvent NoHealth;

    public GameEvent enemyDead;

    public override void TakeDamage(float damage)
    {
        return;
        health -= damage;
        if (health <= 0 && NoHealth != null)
        {
            enemyDead.Raise();
            NoHealth.Invoke();
        }
    }
}

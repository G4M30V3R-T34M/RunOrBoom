using UnityEngine.Events;

public class PlayerHealthManager : HealthManager
{
    public delegate void DamageReveived();
    public UnityEvent damageReceived;

    public override void TakeDamage(float damage) => damageReceived.Invoke();
}

using System;

public interface IDamageable
{
    event Action OnDeath;
    void ApplyDamage(float value);
}

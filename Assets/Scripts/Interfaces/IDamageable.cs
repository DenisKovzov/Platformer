using System;

public interface IDamageable
{
    event Action OnDeath;
    event Action OnHealthChanged;
    void ApplyDamage(float value);

    float CurrentHealth {get;}
    float MaxHealth {get;}
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour, IDamageable
{
    public int CurrentHealth { get; private set; }

    [SerializeField] private HealthBar healthBar;
    public event Action OnEntityDeath;
    private int healthDefault;
    public void Die()
    {
        OnEntityDeath?.Invoke();
        gameObject.SetActive(false);
    }

    public virtual void GetDamaged(int _damage, GameObject _hitter, GameObject _owner)
    {

        CurrentHealth = Mathf.Max(0, CurrentHealth - _damage);

        healthBar.SetBarSize(CurrentHealth / (float)healthDefault);

        if (CurrentHealth <= 0) { Die(); }
    }

    public void GetHealed(int _heal)
    {
        CurrentHealth = Mathf.Min(healthDefault, CurrentHealth + _heal);
    }

    public void SetHealth(int _health = 0)
    {
        healthDefault = _health;
        CurrentHealth = healthDefault;
    }
}

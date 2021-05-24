using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthManager : MonoBehaviour, IDamageable
{
    public event Action OnPlayerDeath;
    public int CurrentPlayerHealth { get; private set; }
    private int healthDefault;
    public void Die()
    {
        OnPlayerDeath?.Invoke();
        SetHealth(healthDefault);
    }

    public void GetDamaged(int _damage, GameObject _hitter, GameObject _owner)
    {
        if (_hitter.CompareTag("Enemy")){ return; }
        if (_owner.CompareTag("Tower")) { return; }

        CurrentPlayerHealth = Mathf.Max(0, CurrentPlayerHealth - _damage);

        if(CurrentPlayerHealth <= 0) { Die(); }
    }

    public void GetHealed(int _heal)
    {
        CurrentPlayerHealth = Mathf.Min(healthDefault, CurrentPlayerHealth + _heal);
    }

    public void SetHealth(int _health)
    {
        healthDefault = _health;
        CurrentPlayerHealth = healthDefault;
    }
}

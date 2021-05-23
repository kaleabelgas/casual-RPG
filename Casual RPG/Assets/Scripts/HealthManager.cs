using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour, IDamageable
{
    public int Health { get; private set; }

    public event Action OnEntityDeath;
    private int healthDefault;
    public void Die()
    {
        OnEntityDeath?.Invoke();
        gameObject.SetActive(false);
    }

    public virtual void GetDamaged(int _damage, GameObject _hitter, GameObject _owner)
    {

        Health = Mathf.Max(0, Health - _damage);

        if (Health <= 0) { Die(); }
    }

    public void GetHealed(int _heal)
    {
        Health = Mathf.Min(healthDefault, Health + _heal);
    }

    public void SetHealth(int _health = 0)
    {
        healthDefault = _health;
        Health = healthDefault;
    }

    private void OnDisable()
    {

        if (gameObject.tag.Equals("Enemy")) { GameManager.RemoveEnemyFromList(gameObject); }
    }
}

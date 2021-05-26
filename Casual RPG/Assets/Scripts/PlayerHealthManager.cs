using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthManager : MonoBehaviour, IDamageable
{
    [SerializeField] private float invincibleTime;
    public event Action OnPlayerDeath;
    public int CurrentPlayerHealth { get; private set; }
    private int healthDefault;
    private float _invinciClock;

    private bool isDead = false;

    private void Update()
    {
        _invinciClock -= Time.deltaTime;
        //Debug.Log($"Is Invincible for {(int)_invinciClock} seconds");
    }
    public void Die()
    {
        if (isDead) { return; }
        OnPlayerDeath?.Invoke();
        isDead = true;
    }

    public void ResetHealth()
    {
        SetHealth(healthDefault);
        Debug.Log($"Resetting Health to {CurrentPlayerHealth}");
        _invinciClock = invincibleTime;
        isDead = false;
    }

    public void GetDamaged(int _damage, GameObject _hitter, GameObject _owner)
    {
        if (_hitter.CompareTag("Enemy")) { return; }
        if (_owner.CompareTag("Tower")) { return; }


        if (_invinciClock > 0) { return; }

        Debug.Log("Ouch! " + CurrentPlayerHealth);

        CurrentPlayerHealth = Mathf.Max(0, CurrentPlayerHealth - _damage);

        if (CurrentPlayerHealth <= 0) { Die(); }
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

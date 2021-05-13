using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour, IDamageable
{
    public int Health { get; private set; }

    private int healthDefault;
    public void Die()
    {
        Debug.Log("me ded");
    }

    public void GetDamaged(int _damage)
    {
        Debug.Log(_damage);
        Debug.Log(Health);
        Health = Mathf.Max(0, Health - _damage);

        if(Health <= 0) { Die(); }
    }

    public void GetHealed(int _heal)
    {
        throw new System.NotImplementedException();
    }

    public void SetHealth(int _health = 0)
    {
        healthDefault = _health;
        Health = healthDefault;
    }
}

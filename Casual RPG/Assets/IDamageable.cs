using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    void SetHealth(int _health);
    void GetDamaged(int _damage);
    void GetHealed(int _heal);
    void Die();
}

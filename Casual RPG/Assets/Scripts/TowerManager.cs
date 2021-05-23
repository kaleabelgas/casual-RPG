using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerManager : MonoBehaviour, IDamageable
{
    [SerializeField] private TowerSO[] towerUpgrades;
    private int currentTowerLevel;
    private TowerSO currentTower;

    public event Action OnTowerDeath;

    public float CurrentTowerHealth { get; private set; }

    private void Start()
    {
        currentTower = towerUpgrades[currentTowerLevel];
        SetHealth(currentTower.Health);
    }


    public void UpgradeTower()
    {
        currentTowerLevel++;
        currentTower = towerUpgrades[currentTowerLevel];
    }

    public void Die()
    {
        OnTowerDeath?.Invoke();
        Debug.Log("TOWER DESTROYED");
    }

    public void GetDamaged(int _damage, GameObject _hitter)
    {
        if (!_hitter.tag.Equals("Enemy")) { return; }
        CurrentTowerHealth = Mathf.Max(0, CurrentTowerHealth - _damage);
        if(CurrentTowerHealth <= 0) { Die(); }
        Debug.Log(CurrentTowerHealth);
    }

    public void GetHealed(int _heal)
    {
        CurrentTowerHealth = Mathf.Min(CurrentTowerHealth, currentTower.Health);
    }

    public void SetHealth(int _health)
    {
        CurrentTowerHealth = _health;
    }

}

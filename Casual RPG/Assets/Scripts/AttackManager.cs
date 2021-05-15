using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackManager : MonoBehaviour
{
    private SwordManager sword;
    private GunManager gun;


    private void Awake()
    {
        sword = GetComponentInChildren<SwordManager>();
        gun = GetComponentInChildren<GunManager>();
    }
    public void SwordAttack()
    {
        sword.Attack();
    }
    public void GunAttack()
    {
        gun.Shoot();
    }
}

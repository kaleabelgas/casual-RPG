using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackManager : MonoBehaviour
{
    private IWeapon sword;
    private GunManager gun;


    private void Awake()
    {
        sword = GetComponentInChildren<IWeapon>();
        gun = GetComponent<GunManager>();
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

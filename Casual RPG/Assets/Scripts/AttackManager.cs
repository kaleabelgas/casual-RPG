using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackManager : MonoBehaviour
{
    private IWeapon weapon;

    private void Awake()
    {
        weapon = GetComponentInChildren<IWeapon>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) { AttackWithWeapon(); }
    }

    private void AttackWithWeapon()
    {
        weapon.Attack();
    }
}

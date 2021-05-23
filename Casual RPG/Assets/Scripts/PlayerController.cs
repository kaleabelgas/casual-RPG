using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Entity
{
    private IMovement movement;
    [SerializeField] private WeaponHolder weaponHolder;

    protected override void Awake()
    {
        base.Awake();
        movement = GetComponent<IMovement>();
    }
    protected override void Start()
    {
        base.Start();
        movement.SetMoveSpeed(thisEntity.MoveSpeed);
    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.E)) { weaponHolder.Equip(); }
        if (Input.GetKeyDown(KeyCode.Q)) { weaponHolder.DropCurrentWeapon(); }
        if (Input.GetKeyDown(KeyCode.R)) { weaponHolder.GiveWeaponToTower(); }
        if (Input.GetKey(KeyCode.Space)) { weaponHolder.UseWeapon(); }
    }
}

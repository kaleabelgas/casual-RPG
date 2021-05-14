using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Entity
{
    [SerializeField] private AttackManager attackManager;
    private IMovement movement;

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
        if (Input.GetMouseButton(0)) { attackManager.SwordAttack(); }
        if (Input.GetMouseButton(1)) { attackManager.GunAttack(); }
    }
}

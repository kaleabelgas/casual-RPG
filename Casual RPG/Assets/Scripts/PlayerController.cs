using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Entity
{
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
}

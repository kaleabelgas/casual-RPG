using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTransform : MonoBehaviour, IMovement
{
    private float moveSpeed;
    private Vector2 movementDirection;
    private Transform thisTransform;

    private void Awake()
    {
        thisTransform = gameObject.transform;
    }

    public void SetMovement(Vector2 _direction)
    {
        movementDirection = _direction;
    }

    public void SetMoveSpeed(float _moveSpeed)
    {
        moveSpeed = _moveSpeed;
    }

    private void Move()
    {
        transform.Translate(movementDirection * moveSpeed * Time.deltaTime);
    }

    private void Update()
    {
        Move();
    }
}

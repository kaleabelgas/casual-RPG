using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMovement
{
    void SetMovement(Vector2 _direction);
    void SetMoveSpeed(float _moveSpeed);
}

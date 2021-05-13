using UnityEngine;

public class MovePhysics : MonoBehaviour, IMovement
{
    [SerializeField] private Rigidbody2D rb2D;

    private float moveSpeed;
    private Vector2 movementDirection;
    private bool isFacingRight = true;

    public void SetMoveSpeed(float _moveSpeed)
    {
        moveSpeed = _moveSpeed;
        Debug.Log(moveSpeed);
    }
    public void SetMovement(Vector2 _direction)
    {
        movementDirection = _direction;
    }
    private void Move()
    {
        rb2D.MovePosition(rb2D.position + movementDirection * moveSpeed * Time.deltaTime);
        Debug.Log(moveSpeed);

        if (movementDirection.x < 0 && isFacingRight)
            Flip();
        else if (movementDirection.x > 0 && !isFacingRight)
            Flip();

    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        transform.Rotate(0, 180, 0);
    }

    private void FixedUpdate()
    {
        Move();
    }
}

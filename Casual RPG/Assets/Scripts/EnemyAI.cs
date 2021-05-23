using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : Entity
{
    private IMovement movement;
    [SerializeField] private WeaponHolder weaponHolder;
    protected override void Awake()
    {
        base.Awake();
        movement = GetComponent<IMovement>();
    }

    protected override void Start() => base.Start();

    protected void OnEnable()
    {
        movement.SetMoveSpeed(thisEntity.MoveSpeed);
        movement.SetMovement((Vector2.zero - (Vector2)transform.position).normalized);
        Debug.Log("Position " + transform.position, this);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(this.gameObject.tag)) { return; }
        if (other.gameObject.CompareTag("Player")) { return; }
        IDamageable toDamage = other.gameObject.GetComponent<IDamageable>();
        if (toDamage == null) { return; }

        toDamage.GetDamaged(15, this.gameObject);
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        movement.SetMoveSpeed(0);
        movement.SetMovement(Vector2.zero);
    }
}

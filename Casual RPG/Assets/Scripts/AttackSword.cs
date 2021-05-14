using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSword : MonoBehaviour, IWeapon
{
    [SerializeField] private Animator animator;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackRadius;
    [SerializeField] private LayerMask enemyMask;
    [SerializeField] private float timerDefault;

    private Collider2D[] hits;
    private float timer;

    private void Update()
    {
        timer = Mathf.Max(0, timer - Time.deltaTime);
    }

    public void Attack()
    {
        if (timer > 0) { return; }

        animator.Play("Sword Hit 1");

        hits = Physics2D.OverlapCircleAll(attackPoint.position, attackRadius, enemyMask);
        foreach(Collider2D hit in hits)
        {
            //hit.GetComponent<>
            Debug.Log(hit.gameObject.name, this);
            IDamageable toDamage = hit.GetComponent<IDamageable>();
            if (toDamage != null) { toDamage.GetDamaged(1); }
        }

        timer = timerDefault;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
    }
}

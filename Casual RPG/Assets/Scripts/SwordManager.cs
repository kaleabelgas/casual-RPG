using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordManager : MonoBehaviour
{
    [SerializeField] private Transform attackPoint;
    [SerializeField] private SwordSO swordSO;

    private Collider2D[] hits;
    private float timer;

    private void Update()
    {
        timer = Mathf.Max(0, timer - Time.deltaTime);
    }

    public void Attack()
    {
        if (timer > 0) { return; }

        hits = Physics2D.OverlapCircleAll(attackPoint.position, swordSO.AttackRadius, swordSO.Mask);
        foreach(Collider2D hit in hits)
        {
            //hit.GetComponent<>
            //Debug.Log(hit.gameObject.name, this);
            IDamageable toDamage = hit.GetComponent<IDamageable>();
            if (toDamage != null) { toDamage.GetDamaged(swordSO.Damage); }



        }

        timer = swordSO.AttackSpeed;
    }
}

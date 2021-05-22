using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordManager : MonoBehaviour, IPickupable, IWeapon
{
    [SerializeField] private Transform attackPoint;
    [SerializeField] private SwordSO swordSO;

    private Collider2D[] hits;
    private float timer;
    private bool isEquipped;

    private void Update()
    {
        timer = Mathf.Max(0, timer - Time.deltaTime);
    }

    public void Attack()
    {
        if (timer > 0) { return; }
        if (!isEquipped) { return; }

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

    public void PickUp(Transform _parent)
    {
        transform.SetParent(_parent);
        transform.position = _parent.position;
        transform.rotation = _parent.rotation;
        isEquipped = true;
    }

    public void Drop()
    {
        transform.SetParent(null);
        isEquipped = false;
    }
}

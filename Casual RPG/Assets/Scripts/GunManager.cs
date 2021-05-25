using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GunManager : MonoBehaviour, IWeapon

{
    [SerializeField] private GunSO gunSO;
    private ObjectPooler objectPooler;

    private float attackTimer;
    private Vector3 point;
    private Vector2 direction;

    private void Start()
    {
        objectPooler = ObjectPooler.Instance;
    }
    private void Update()
    {
        attackTimer -= Time.deltaTime;
    }

    public void Attack()
    {
        if (attackTimer > 0) { return; }

        for (int i = 0; i < gunSO.AttackPoints; i++)
        {
            var angle = Mathf.Deg2Rad * (2 * gunSO.Arc * i - gunSO.Arc * gunSO.AttackPoints + gunSO.Arc + 0 * gunSO.AttackPoints) / (2 * gunSO.AttackPoints);
            point = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * gunSO.Radius;
            point = Quaternion.LookRotation(transform.forward, transform.up) * point;

            direction = point.normalized;
            point += transform.position;
            //Debug.Log("shooting", this);

            //Debug.Log(direction);
            GameObject toShoot = objectPooler.SpawnFromPool(gunSO.BulletUsed, point, Quaternion.identity);
            if (toShoot == null) { return; }
            BulletManager bm = toShoot.GetComponent<BulletManager>();
            //Debug.Log(transform.parent.parent.gameObject.name);
            bm.SetOwner(transform.parent.parent.gameObject);
            bm.SetBulletDirection(direction);
        }

        attackTimer = gunSO.AttackSpeed;
    }

}

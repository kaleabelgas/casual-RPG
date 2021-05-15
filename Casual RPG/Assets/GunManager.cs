using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunManager : MonoBehaviour
{
    [SerializeField] private GunSO gunSO;
    // temp
    [SerializeField] private GameObject bullet;

    private float attackTimer;
    private Vector3 point;
    private Vector2 direction;

    private void Update()
    {
        attackTimer -= Time.deltaTime;
    }

    public void Shoot()
    {
        if (attackTimer > 0) { return; }

        for (int i = 0; i < gunSO.AttackPoints; i++)
        {
            var angle = Mathf.Deg2Rad * (2 * gunSO.Arc * i - gunSO.Arc * gunSO.AttackPoints + gunSO.Arc + 0 * gunSO.AttackPoints) / (2 * gunSO.AttackPoints);
            point = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * gunSO.Radius;
            point = Quaternion.LookRotation(transform.forward, transform.up) * point;

            direction = point.normalized;
            point += transform.position;
            Debug.Log("shooting", this);

            //Debug.Log(direction);
            GameObject toShoot = Instantiate(bullet, point, Quaternion.identity);
            BulletManager bm = toShoot.GetComponent<BulletManager>();
            bm.SetOwner(transform.parent.gameObject);
            bm.SetBulletDirection(direction);
        }

        attackTimer = gunSO.AttackSpeed;
    }
}

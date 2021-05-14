using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunManager : MonoBehaviour
{
    [SerializeField] private GunSO gunSO;
    // temp
    [SerializeField] private GameObject bullet;

    private float attackSpeed;
    private float attackTimer;
    private int numSides;
    private float arc;
    private float radius;
    private Vector3 point;
    private Vector2 direction;

    private void Start()
    {
        attackSpeed = gunSO.AttackSpeed;
        numSides = gunSO.AttackPoints;
        arc = gunSO.Arc;
        radius = gunSO.Radius;
    }
    private void Update()
    {
        attackTimer -= Time.deltaTime;
    }

    public void Shoot()
    {
        if (attackTimer > 0) { return; }

        for (int i = 0; i < numSides; i++)
        {
            var angle = Mathf.Deg2Rad * (2 * arc * i - arc * numSides + arc + 180 * numSides) / (2 * numSides);
            point = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * radius;
            point = Quaternion.LookRotation(transform.forward, transform.up) * point;

            direction = point.normalized;
            point += transform.position;
            Debug.Log("shooting", this);

            GameObject toShoot = Instantiate(bullet, point, Quaternion.identity);
            BulletManager bm = toShoot.GetComponent<BulletManager>();
            bm.SetOwner(this.gameObject);
            bm.SetBulletDirection(direction);
        }

        attackTimer = attackSpeed;
    }
}

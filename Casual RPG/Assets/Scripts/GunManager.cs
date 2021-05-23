using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunManager : MonoBehaviour, IPickupable, IWeapon

{
    [SerializeField] private GunSO gunSO;

    [SerializeField] private bool isFacingEnemy;
    [SerializeField] private float enemySearchDelay;
    [SerializeField] private string targetTag;

    GameObject _target = null;

    private float _enemySearchDelay;
    private ObjectPooler objectPooler;

    private float attackTimer;
    private bool isEquipped;
    private Vector3 point;
    private Vector2 direction;

    private void Start()
    {
        objectPooler = ObjectPooler.Instance;
        _enemySearchDelay = enemySearchDelay;
    }
    private void Update()
    {
        attackTimer -= Time.deltaTime;
        if (isFacingEnemy && isEquipped) { FaceEnemy(); }
    }

    private void FaceEnemy()
    {
        _enemySearchDelay -= Time.deltaTime;
        if (_enemySearchDelay <= 0)
        {
            _target = LookForClosestEnemy();
            _enemySearchDelay = enemySearchDelay;
        }

        if (_target == null)
        {
            return;
        }
        //Debug.Log(_target.name, _target);

        Vector2 lookDir = _target.transform.position - transform.position;

        lookDir.Normalize();
        //Debug.Log(lookDir);
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, angle);
    }

    private GameObject LookForClosestEnemy()
    {
        GameObject _closestEnemy = null;
        List<GameObject> _enemies = GameManager.GetAllActiveEnemies();

        if (_enemies == null) { return null; }
        float _closestDistance = 100;
        float _distanceToEnemy;

        foreach (GameObject _enemy in _enemies)
        {
            _distanceToEnemy = Vector2.Distance(_enemy.transform.position, transform.position);

            if (_distanceToEnemy < _closestDistance)
            {
                _closestDistance = _distanceToEnemy;
                _closestEnemy = _enemy;
            }
        }

        return _closestEnemy;
    }

    public void Attack()
    {
        if (!isEquipped) { return; }
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
            bm.SetOwner(transform.parent.gameObject);
            bm.SetBulletDirection(direction);
        }

        attackTimer = gunSO.AttackSpeed;
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHolder : MonoBehaviour
{

    private Weapon currentWeapon;

    [SerializeField] private float enemySearchDelay;
    [SerializeField] bool shouldFaceEnemy;
    [SerializeField] private bool isAcceptingWeapon = true;
    private GameObject _target;
    private float _enemySearchDelay;




    private void Update()
    {
        if (shouldFaceEnemy) { FaceEnemy(); }
    }
    public void UseWeapon()
    {
        if (currentWeapon == null)
        {
            Debug.LogWarning("Current Weapon is Null", this);
            return;
        }
        currentWeapon.Attack();

    }

    public bool IsHoldingWeapon()
    {
        return currentWeapon != null;
    }

    public void GiveWeaponToTower()
    {
        Collider2D[] towers = Physics2D.OverlapCircleAll(transform.position, 2);

        foreach (Collider2D tower in towers)
        {
            if (!tower.gameObject.CompareTag("Tower")) { continue; }

            WeaponHolder weaponHolderTower = tower.gameObject.GetComponentInChildren<WeaponHolder>();
            weaponHolderTower.ReceiveWeapon(currentWeapon);
            break;
        }
    }
    public void ReceiveWeapon(Weapon _weapon)
    {
        if (_weapon == null) { return; }
        if (!isAcceptingWeapon) { return; }
        if (currentWeapon != null) { DropCurrentWeapon(); }
        _weapon.PickUp(transform);

        currentWeapon = _weapon;
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
        List<GameObject> _enemies = GameManager.Instance.GetAllObjectsAsList(GameManager.ObjectLists.enemy);

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

    public void Equip()
    {
        Collider2D[] _weapons = Physics2D.OverlapCircleAll(transform.position, 2f);

        foreach (Collider2D _weapon in _weapons)
        {
            Weapon _weaponOnGround = _weapon.gameObject.GetComponent<Weapon>();

            if (_weaponOnGround == null) { continue; }
            if (currentWeapon != null && _weaponOnGround.gameObject.Equals(currentWeapon.gameObject)) { continue; }

            if (currentWeapon != null) { DropCurrentWeapon(); }

            _weaponOnGround.PickUp(transform);
            currentWeapon = _weaponOnGround;

            break;
        }
    }
    public void DropCurrentWeapon()
    {
        if (transform.childCount <= 0) { return; }
        currentWeapon.Drop();
        currentWeapon = null;

        //Debug.Log("drop");
    }
}

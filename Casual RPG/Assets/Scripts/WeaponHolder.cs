using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHolder : MonoBehaviour
{
    public bool IsHoldingWeapon { get; private set; }

    private IPickupable heldItem;
    private GameObject heldItemObject;
    private IWeapon currentWeapon;

    [SerializeField] private float enemySearchDelay;
    [SerializeField] bool shouldFaceEnemy;
    private GameObject _target;
    private float _enemySearchDelay;

    private void Awake()
    {
        if(transform.childCount == 0) { return; }
        currentWeapon = transform.GetChild(0).gameObject.GetComponent<IWeapon>();
        heldItem = transform.GetChild(0).gameObject.GetComponent<IPickupable>();
        Debug.Log(transform.GetChild(0).name);
    }
    private void Start()
    {
        if (transform.childCount == 0) { return; }
        heldItemObject = transform.GetChild(0).gameObject;
        if(transform.childCount > 0) { IsHoldingWeapon = true; }
        if(heldItem == null) { throw new System.Exception("HELDTHING IS NULL"); }
        heldItem.PickUp(transform);
    }

    private void Update()
    {
        if (shouldFaceEnemy) { FaceEnemy(); }
    }
    public void UseWeapon()
    {
        if (currentWeapon == null) { return; }
        currentWeapon.Attack();
    }

    public void GiveWeaponToTower()
    {
        Collider2D[] towers = Physics2D.OverlapCircleAll(transform.position, 2);

        foreach(Collider2D tower in towers)
        {
            if (!tower.gameObject.CompareTag("Tower")) { continue; }
            IsHoldingWeapon = false;

            WeaponHolder weaponHolderTower = tower.gameObject.GetComponentInChildren<WeaponHolder>();
            weaponHolderTower.ReceiveWeapon(heldItemObject);
            heldItemObject = null;
            currentWeapon = null;
            heldItem = null;
            break;
        }
    }
    public void ReceiveWeapon(GameObject _weapon)
    {
        if(_weapon == null) { return; }
        DropCurrentWeapon();
        _weapon.transform.SetParent(transform);
        _weapon.transform.position = transform.position;
        _weapon.transform.rotation = transform.rotation;
        heldItemObject = _weapon;
        heldItem = _weapon.GetComponent<IPickupable>();
        currentWeapon = _weapon.GetComponent<IWeapon>();

        IsHoldingWeapon = true;
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

    public void Equip()
    {
        Collider2D[] _weapons = Physics2D.OverlapCircleAll(transform.position, 2f);

        foreach (Collider2D _weapon in _weapons)
        {
            IPickupable thingOnGround = _weapon.gameObject.GetComponent<IPickupable>();

            if (thingOnGround == null) { continue; }
            if (_weapon.gameObject == heldItemObject) { continue; }


            if (IsHoldingWeapon) { DropCurrentWeapon(); }

            heldItem = thingOnGround;
            heldItem.PickUp(transform);
            heldItemObject = _weapon.gameObject;

            currentWeapon = _weapon.gameObject.GetComponent<IWeapon>();
            IsHoldingWeapon = true;

            break;
        }
    }
    public void DropCurrentWeapon()
    {
        if (transform.childCount <= 0) { return; }
        IsHoldingWeapon = false;

        heldItem.Drop();
        heldItemObject = null;
        currentWeapon = null;
        heldItem = null;
        Debug.Log("drop");
    }
}

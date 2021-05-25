using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : Entity
{
    private IMovement movement;
    [SerializeField] private WeaponHolder weaponHolder;
    [SerializeField] private float threshold = .1f;
    [SerializeField] private GameObject weaponToUse;

    private TransformPoints waypoints;

    private int _currentWaypoint;
    private bool hasReachedEnd;
    protected override void Awake()
    {
        base.Awake();
        movement = GetComponent<IMovement>();
        healthManager.OnEntityDeath += weaponHolder.DropCurrentWeapon;
    }

    protected override void Start() => base.Start();

    protected void OnEnable()
    {
        movement.SetMoveSpeed(thisEntity.MoveSpeed);
        if (weaponHolder.IsHoldingWeapon()) { return; }
        GameObject _weapon = Instantiate(weaponToUse, weaponHolder.transform);
        weaponHolder.ReceiveWeapon(_weapon.GetComponent<Weapon>());
    }
    private void Update()
    {
        weaponHolder.UseWeapon();

        if (!hasReachedEnd) { LookForWaypoints(); }

    }

    public void SetWayPoints(TransformPoints transformPoints)
    {
        waypoints = transformPoints;
        _currentWaypoint = 0;
    }

    private void LookForWaypoints()
    {
        if (Vector3.Distance(waypoints.PointPositions[_currentWaypoint], transform.position) <= threshold)
        {
            UpdateDirection();
            //Debug.Log("Reached Waypoint");
            return;
        }
        Vector2 targetDirection = waypoints.PointPositions[_currentWaypoint] - (Vector2)transform.position;
        targetDirection.Normalize();
        movement.SetMovement(targetDirection);
    }

    private void UpdateDirection()
    {
        Vector2 targetDirection = waypoints.PointPositions[_currentWaypoint] - (Vector2) transform.position;
        targetDirection.Normalize();
        //Debug.Log($"Target Direction {targetDirection}");
        movement.SetMovement(targetDirection);
        _currentWaypoint++;

        hasReachedEnd = _currentWaypoint + 1 > waypoints.PointPositions.Length;
        if (hasReachedEnd) { movement.SetMovement(Vector2.zero); }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(this.gameObject.tag)) { return; }
        if (other.gameObject.CompareTag("Player")) { return; }
        IDamageable toDamage = other.gameObject.GetComponent<IDamageable>();
        if (toDamage == null) { return; }

        toDamage.GetDamaged(15, this.gameObject, this.gameObject);
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        movement.SetMoveSpeed(0);
        movement.SetMovement(Vector2.zero);
        waypoints = null;
        GameManager.Instance.RemoveObjectFromList(GameManager.ObjectLists.enemy, gameObject);
    }
}

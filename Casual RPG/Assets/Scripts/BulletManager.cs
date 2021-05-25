using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    [SerializeField] private BulletSO bulletSO;
    [SerializeField] private IMovement movement;

    private const float lifetimeconst = 2f;
    private float lifetime;
    private GameObject owner;

    private Vector2 lastDirection;
    private void Awake()
    {
        movement = GetComponent<IMovement>();
    }
    private void Start()
    {
        gameObject.tag = bulletSO.Tag;
    }
    private void OnEnable()
    {
        movement.SetMoveSpeed(bulletSO.Speed);
        GameManager.Instance.AddObjectToList(GameManager.ObjectLists.bullet, gameObject);
        lifetime = lifetimeconst;
    }

    private void Update()
    {
        lifetime -= Time.deltaTime;

        if (lifetime <= 0) { gameObject.SetActive(false); }
    }

    public void SetBulletDirection(Vector2 _direction)
    {
        movement.SetMovement(_direction);
        lastDirection = _direction;

        if (bulletSO.IsHoming && !owner.CompareTag("Enemy")) { StartCoroutine(LookForEnemy()); }
    }

    public void SetOwner(GameObject _owner)
    {
        owner = _owner;
        //Debug.Log(owner, this);
    }


    private IEnumerator LookForEnemy()
    {
        Vector2 _directionOfTarget = lastDirection;
        Transform _target;

        while (true)
        {
            _target = GameManager.Instance.GetClosestObject(GameManager.ObjectLists.enemy, transform, bulletSO.SearchRadius);
            if (_target == null) { yield break; }
            _directionOfTarget += (Vector2)(_target.position - transform.position) * bulletSO.Accuracy;

            _directionOfTarget.Normalize();
            movement.SetMovement(_directionOfTarget);
            lastDirection = _directionOfTarget;
            yield return null;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(owner.tag))
        {
            return;
        }

        IDamageable toDamage = other.gameObject.GetComponent<IDamageable>();
        if (toDamage == null)
        {
            gameObject.SetActive(false);
            return;
        }

        //Debug.Log($"Hitting {other.gameObject.name} with bullet from {owner.name}");
        toDamage.GetDamaged(bulletSO.Damage, this.gameObject, owner);
        //Debug.Log(other.gameObject.name);
        gameObject.SetActive(false);

    }
    private void OnDisable()
    {
        movement.SetMoveSpeed(0);
        //owner = gameObject;
        GameManager.Instance.RemoveObjectFromList(GameManager.ObjectLists.bullet, gameObject);
    }
}

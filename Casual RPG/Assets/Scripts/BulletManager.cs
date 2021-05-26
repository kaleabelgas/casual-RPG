using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    [SerializeField] private BulletSO bulletSO;
    [SerializeField] private IMovement movement;

    private const float lifetimeconst = 4f;
    private float lifetime;
    private GameObject owner;

    private Vector2 lastDirection;
    private void Awake()
    {
        movement = GetComponent<IMovement>();
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

        float angle = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg - 90;

        Debug.Log(angle);

        transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, angle);

        if (bulletSO.IsHoming) { StartCoroutine(LookForEnemy()); }
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
        GameManager.ObjectLists targetType = owner.CompareTag("Player") || owner.CompareTag("Tower") ? GameManager.ObjectLists.enemy : GameManager.ObjectLists.player;
        //Debug.Log($"Heading to {targetType}");

        while (true)
        {
            if (GameManager.Instance.CheckObjectsInList(targetType))
            {

                _target = GameManager.Instance.GetClosestObject(targetType, gameObject.transform, bulletSO.SearchRadius);
                if (_target != null)
                {
                    _directionOfTarget += (Vector2)(_target.position - transform.position) * bulletSO.Accuracy;
                    _directionOfTarget.Normalize();
                }
                movement.SetMovement(_directionOfTarget);

                lastDirection = _directionOfTarget; float angle = Mathf.Atan2(_directionOfTarget.y, _directionOfTarget.x) * Mathf.Rad2Deg - 90;

                transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, angle);
                yield return new WaitForSeconds(Random.Range(0f, 0.1f));
            }
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

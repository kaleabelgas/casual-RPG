using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    [SerializeField] private BulletSO bulletSO;
    [SerializeField] private IMovement movement;

    private GameObject owner;
    private WaitForSeconds searchDelay;

    private Vector2 lastDirection;
    private void Awake()
    {
        movement = GetComponent<IMovement>();
    }
    private void Start()
    {
        gameObject.tag = bulletSO.Tag;
        searchDelay = new WaitForSeconds(bulletSO.SearchDelay);
    }
    private void OnEnable()
    {
        movement.SetMoveSpeed(bulletSO.Speed);
    }

    public void SetBulletDirection(Vector2 _direction)
    {
        movement.SetMovement(_direction);
        lastDirection = _direction;

        if (bulletSO.IsHoming) { StartCoroutine(LookForEnemy(bulletSO.Target)); }
    }

    public void SetOwner(GameObject _owner)
    {
        owner = _owner;
        Debug.Log(owner, this);
    }


    private IEnumerator LookForEnemy(string _tag)
    {
        Collider2D[] _targets;
        List<GameObject> _targetObjects = new List<GameObject>();
        Transform _targetTransform = null;
        float _distanceToTarget = 100;
        float _distance;
        Vector2 _directionOfTarget = lastDirection;

        while (true)
        {
            yield return searchDelay;

            _targetObjects.Clear();

            _targets = Physics2D.OverlapCircleAll(transform.position, bulletSO.SearchRadius);

            foreach (Collider2D _target in _targets)
            {
                if (_target.gameObject.CompareTag(bulletSO.Target) && !_targetObjects.Contains(_target.gameObject))
                {
                    _targetObjects.Add(_target.gameObject);
                    Debug.Log("Enemy Found");
                }
            }

            if (_targetObjects != null)
            {
                for (int i = 0; i < _targetObjects.Count; i++)
                {
                    _distance = Vector2.Distance(_targetObjects[i].transform.position, transform.position);

                    if (_distance < _distanceToTarget)
                    {
                        _distanceToTarget = _distance;
                        _targetTransform = _targetObjects[i].transform;

                        _directionOfTarget += (Vector2) (_targetTransform.position - transform.position) * bulletSO.Accuracy;
                        //_directionOfTarget += lastDirection * bulletSO.Accuracy;
                    }
                    if (_targetTransform == null) { yield break; }
                }
            }
            _directionOfTarget.Normalize();
            movement.SetMovement(_directionOfTarget);
            lastDirection = _directionOfTarget;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(owner.tag)) { return; }

        IDamageable toDamage = other.gameObject.GetComponent<IDamageable>();
        if (toDamage == null) { return; }

        toDamage.GetDamaged(bulletSO.Damage);
        Debug.Log(other.gameObject.name);

        gameObject.SetActive(false);
    }

    private void OnBecameInvisible()
    {
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        movement.SetMoveSpeed(0);
        owner = gameObject;
    }
}

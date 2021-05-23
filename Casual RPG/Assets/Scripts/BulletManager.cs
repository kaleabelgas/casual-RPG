using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    [SerializeField] private BulletSO bulletSO;
    [SerializeField] private IMovement movement;

    private const float lifetimeconst = 2;
    private float lifetime;
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
        GameManager.AddBulletToList(this.gameObject);
        lifetime = lifetimeconst;
    }

    private void Update()
    {
        lifetime -= Time.deltaTime;

        if(lifetime <= 0) { gameObject.SetActive(false); }
    }

    public void SetBulletDirection(Vector2 _direction)
    {
        movement.SetMovement(_direction);
        lastDirection = _direction;

        if (bulletSO.IsHoming && !owner.CompareTag("Enemy")) { StartCoroutine(LookForEnemy(bulletSO.Target)); }
    }

    public void SetOwner(GameObject _owner)
    {
        owner = _owner;
        //Debug.Log(owner, this);
    }


    private IEnumerator LookForEnemy(string _tag)
    {
        List<GameObject> _targetObjects = GameManager.GetAllActiveEnemies();
        Transform _targetTransform = null;
        float _distanceToTarget = bulletSO.SearchRadius;
        float _distance;
        Vector2 _directionOfTarget = lastDirection;

        while (true)
        {
            yield return searchDelay;

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
        Debug.Log(other.gameObject.name);
        gameObject.SetActive(false);

    }
    private void OnDisable()
    {
        movement.SetMoveSpeed(0);
        //owner = gameObject;
        GameManager.RemoveBulletFromList(this.gameObject);
    }
}

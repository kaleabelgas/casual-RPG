using System.Collections;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    [SerializeField] private BulletSO bulletSO;
    [SerializeField] private IMovement movement;

    private GameObject owner;

    private int damage;
    private Vector2 direction;
    private float searchRadius;

    private WaitForSeconds searchDelay;
    private Transform target;
    private Transform myTransform;
    private void Awake()
    {
        movement = GetComponent<IMovement>();
    }
    private void Start()
    {
        gameObject.tag = bulletSO.Tag;
        damage = bulletSO.Damage;
        myTransform = transform;
        searchDelay = new WaitForSeconds(bulletSO.SearchDelay);
        searchRadius = bulletSO.SearchRadius;
    }
    private void OnEnable()
    {
        movement.SetMoveSpeed(bulletSO.Speed);
        movement.SetMovement(direction);
    }

    public void SetBulletDirection(Vector2 _direction)
    {
        direction = _direction;

        if (bulletSO.IsHoming) { StartCoroutine(LookForEnemy(bulletSO.Target)); }
    }

    public void SetOwner(GameObject _owner)
    {
        owner = _owner;
    }


    private IEnumerator LookForEnemy(string _tag)
    {
        Collider2D[] _targets;
        float _distanceToTarget = 100;
        float _distance;
        Vector2 _directionOfTarget;
        
        while (true)
        {
            _targets = Physics2D.OverlapCircleAll(transform.position, searchRadius);
            Debug.Log(_targets.Length, this);

            if (_targets == null) { yield return searchDelay; }

            for(int i = 0; i < _targets.Length; i++)
            {
                _distance = Vector2.Distance(_targets[i].transform.position, transform.position);

                if (_distance < _distanceToTarget)
                {
                    Debug.Log("Enemy found", this);
                    _distanceToTarget = _distance;
                    target = _targets[i].transform;
                }
            }

            if(target == null) { yield return searchDelay; }

            _directionOfTarget = target.position - transform.position;
            _directionOfTarget.Normalize();

            movement.SetMovement(_directionOfTarget);
            yield return searchDelay;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(owner.tag)) { return; }

        IDamageable toDamage = other.gameObject.GetComponent<IDamageable>();
        if(toDamage == null) { return; }

        toDamage.GetDamaged(damage);
        Debug.Log(other.gameObject.name);
    }

    private void OnDisable()
    {
        movement.SetMoveSpeed(0);
        movement.SetMovement(Vector2.zero);
        owner = gameObject;
    }
}

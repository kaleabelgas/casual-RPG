using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour, IPickupable, IWeapon
{
    [SerializeField] private GunSO gunSO;
    private float despawnClock;
    private bool isEquipped;

    private IWeapon weapon;

    private void Start()
    {
        weapon = GetComponent<IWeapon>();
        despawnClock = gunSO.DespawnTime;
    }

    private void Update()
    {
        if (!isEquipped) { despawnClock -= Time.deltaTime; }
        if (despawnClock <= 0) { Destroy(gameObject, 1); }
    }

    public void Attack()
    {
        if(!isEquipped) { return; }
        weapon.Attack();
    }


    public void PickUp(Transform _parent)
    {
        transform.SetParent(_parent);
        transform.position = _parent.position;
        transform.rotation = _parent.rotation;
        isEquipped = true;
        despawnClock = gunSO.DespawnTime;
    }


    public void Drop()
    {
        Transform owner = transform.parent.parent;
        transform.SetParent(null);
        transform.position = (Vector2)transform.position + Vector2.down;
        isEquipped = false;

        if (owner.CompareTag("Player")) { return; }

        float randomNumber = Random.Range(0, 100);
        if (randomNumber > gunSO.ChanceToDrop) { Destroy(gameObject); }
    }
}

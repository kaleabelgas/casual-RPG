using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour, IPickupable, IWeapon
{
    [SerializeField] private GunSO gunSO;
    [SerializeField] private SpriteRenderer weaponRenderer;
    [SerializeField] private float despawnTime;
    private float despawnClock;
    private bool isEquipped;

    private IWeapon weapon;

    private const int fiveSeconds = 5;

    private Color weaponColor;

    private void Start()
    {
        weapon = GetComponent<IWeapon>();
        despawnClock = despawnTime;
        weaponColor = weaponRenderer.color;

    }

    private void Update()
    {
        if (!isEquipped) { despawnClock -= Time.deltaTime; }
        if (despawnClock <= fiveSeconds)
        {
            weaponColor.a = Mathf.Max(0, weaponColor.a - Time.deltaTime);
            weaponRenderer.color = weaponColor;
        }
        if (despawnClock <= 0) { Destroy(gameObject, .01f); }
    }

    public void Attack()
    {
        if (!isEquipped) { return; }
        weapon.Attack();
    }


    public void PickUp(Transform _parent)
    {
        transform.SetParent(_parent);
        transform.position = _parent.position;
        transform.rotation = _parent.rotation;
        isEquipped = true;
        despawnClock = despawnTime;

        weaponColor = weaponRenderer.color;

        weaponColor.a = 255;
        weaponRenderer.color = weaponColor;
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

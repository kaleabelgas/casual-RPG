using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Weapon : MonoBehaviour, IWeapon, IPointerClickHandler
{
    [SerializeField] private GunSO gunSO;
    [SerializeField] private SpriteRenderer weaponRenderer;
    [SerializeField] private float despawnTime;
    [SerializeField] private float enlargeAmount = 2;
    private float despawnClock;
    private bool isEquipped;

    private IWeapon weapon;

    private const int fiveSeconds = 5;
    private const float overFive = .20f;
    private Vector3 defaultScale;

    private Color weaponColor;

    private void Start()
    {
        weapon = GetComponent<IWeapon>();
        despawnClock = despawnTime;
        weaponColor = weaponRenderer.color;
        defaultScale = transform.localScale;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button.Equals(PointerEventData.InputButton.Right))
        {
            Debug.Log($"Clicked {gameObject.name}");
        }
    }
    private void OnMouseEnter()
    {
        transform.localScale = defaultScale * enlargeAmount;

    }
    private void OnMouseExit()
    {
        transform.localScale = defaultScale;
    }

    private void Update()
    {
        if (!isEquipped) { despawnClock -= Time.deltaTime; }
        if (despawnClock <= fiveSeconds)
        {
            weaponColor.a = Mathf.Max(0, weaponColor.a - (Time.deltaTime * overFive));
            weaponRenderer.color = weaponColor;
        }
        if (despawnClock <= 0) { Destroy(gameObject); }
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

        weaponColor.a = 1;
        weaponRenderer.color = weaponColor;
    }


    public void Drop()
    {
        Transform owner = transform.parent.parent;
        transform.SetParent(null);
        transform.position = (Vector2)transform.position + Vector2.down;
        isEquipped = false;

        if (owner.CompareTag("Player")) { return; }
        if(gunSO == null) { return; }

        float randomNumber = Random.Range(0, 100);
        if (randomNumber > gunSO.ChanceToDrop) { Destroy(gameObject); }
    }

}

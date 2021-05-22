using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHolder : MonoBehaviour
{
    private bool isHoldingWeapon;
    private IPickupable heldThing;
    private GameObject heldThingObject;
    private IWeapon currentWeapon;

    private void Start()
    {
        currentWeapon = transform.GetChild(0).gameObject.GetComponent<IWeapon>();
        heldThing = transform.GetChild(0).gameObject.GetComponent<IPickupable>();
        heldThingObject = transform.GetChild(0).gameObject;
        if(transform.childCount > 0) { isHoldingWeapon = true; }
        heldThing.PickUp(transform);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) { Equip(); }
        if (Input.GetKeyDown(KeyCode.Q)) { Drop(); }

        if (Input.GetKey(KeyCode.Space) && currentWeapon != null) { currentWeapon.Attack(); }
    }
    private void Equip()
    {
        Collider2D[] _weapons = Physics2D.OverlapCircleAll(transform.position, 1f);

        foreach (Collider2D _weapon in _weapons)
        {
            IPickupable thingOnGround = _weapon.gameObject.GetComponent<IPickupable>();

            if (thingOnGround == null) { continue; }
            if (_weapon.gameObject == heldThingObject) { continue; }


            if (isHoldingWeapon) { Drop(); }

            heldThing = thingOnGround;
            heldThing.PickUp(transform);
            heldThingObject = _weapon.gameObject;

            currentWeapon = _weapon.gameObject.GetComponent<IWeapon>();
            isHoldingWeapon = true;

            break;
        }
    }
    private void Drop()
    {
        if (transform.childCount <= 0) { return; }
        isHoldingWeapon = false;

        heldThing.Drop();
        heldThingObject = null;
        currentWeapon = null;
        heldThing = null;
        Debug.Log("drop");
    }
}

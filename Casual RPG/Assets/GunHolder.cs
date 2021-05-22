using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunHolder : MonoBehaviour
{
    private bool isHoldingGun;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) { Equip(); }
        if (Input.GetKeyDown(KeyCode.Q)) { Drop(); }
    }
    private void Equip()
    {
        Collider2D[] guns = Physics2D.OverlapCircleAll(transform.position, 1f);

        foreach (Collider2D gun in guns)
        {
            IPickupable pickup = gun.gameObject.GetComponent<IPickupable>();
            Debug.Log(gun.name, gun.gameObject);
            if (pickup != null && gun.gameObject.CompareTag("Gun"))
            {
                Debug.Log("Pickup");
                if (isHoldingGun) { Drop(); }
                pickup.PickUp(transform);
                isHoldingGun = true;
                break;
            }
        }
    }
    private void Drop()
    {
        if(transform.childCount <= 0) { return; }
        isHoldingGun = false;
        IPickupable currentGun = transform.GetChild(0).GetComponent<IPickupable>();
        currentGun.Drop();
        Debug.Log("drop");
    }
}

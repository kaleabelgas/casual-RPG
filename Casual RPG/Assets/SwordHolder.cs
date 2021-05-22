using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordHolder : MonoBehaviour
{
    private bool isHoldingSword;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) { Equip(); }
        if (Input.GetKeyDown(KeyCode.Q)) { Drop(); }
    }
    private void Equip()
    {
        Collider2D[] swords = Physics2D.OverlapCircleAll(transform.position, 1f);

        foreach (Collider2D sword in swords)
        {
            IPickupable pickup = sword.gameObject.GetComponent<IPickupable>();
            if (pickup != null && sword.gameObject.CompareTag("Sword"))
            {
                Debug.Log("Pickup");
                if (isHoldingSword) { Drop(); }
                pickup.PickUp(transform);
                isHoldingSword = true;
                break;
            }
        }
    }
    private void Drop()
    {
        if(transform.childCount <= 0) { return; }
        isHoldingSword = false;
        IPickupable currentGun = transform.GetChild(0).GetComponent<IPickupable>();
        currentGun.Drop();
        Debug.Log("drop");
    }
}

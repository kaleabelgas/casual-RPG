using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPickupable
{
    public void PickUp(Transform _parent);
    public void Drop();
}

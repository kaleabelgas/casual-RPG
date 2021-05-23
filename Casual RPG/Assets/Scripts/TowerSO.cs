using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Tower", menuName = "Tower")]
public class TowerSO : ScriptableObject
{
    public int Health;
    public GameObject Weapon;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Gun", menuName = "Gun")]
public class GunSO : ScriptableObject
{
    public string Tag;
    public float AttackSpeed;
    public int AttackPoints;
    public float Arc = 1;
    public float Radius = 1;
}

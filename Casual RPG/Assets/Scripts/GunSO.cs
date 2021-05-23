using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Gun", menuName = "Gun")]
public class GunSO : ScriptableObject
{
    public string Tag;
    public string BulletUsed;
    [Range(0.01f, 2)]
    public float AttackSpeed;
    public int AttackPoints;
    [Range(1, 360)]
    public float Arc = 1;
    public float Radius = 1;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Sword", menuName = "Sword")]
public class SwordSO : ScriptableObject
{
    public LayerMask Mask;
    public int Damage;
    public float AttackSpeed;
    public float AttackRadius;
}

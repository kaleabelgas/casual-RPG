using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Bullet", menuName = "Bullet")]
public class BulletSO : ScriptableObject
{
    public int Damage;
    public float Speed;
    public bool IsHoming;
    public float SearchRadius;
    [Range(0, 1)]
    public float Accuracy;
}

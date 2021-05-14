using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Bullet", menuName = "Bullet")]
public class BulletSO : ScriptableObject
{
    public string Tag;
    public int Damage;
    public float Speed;
    public bool IsHoming;
    public string Target;
    public float SearchDelay;
    public float SearchRadius;
}

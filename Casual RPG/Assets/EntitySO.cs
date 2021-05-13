using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Entity", menuName = "Entity")]
public class EntitySO : ScriptableObject
{
    public string Tag;
    public int Health;
    public float MoveSpeed;
}

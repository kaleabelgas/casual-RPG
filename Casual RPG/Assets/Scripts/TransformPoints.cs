using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Transform Points", menuName = "Transform Points")]
public class TransformPoints : ScriptableObject
{
    public Vector2[] PointPositions;
}

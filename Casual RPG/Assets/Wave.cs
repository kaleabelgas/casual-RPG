using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct EnemyContainer
{
    public string Prefab;
    public int min;
    public int max;
}

[CreateAssetMenu(fileName = "New Wave", menuName = "Wave")]
public class Wave : ScriptableObject
{
    [SerializeField] private EnemyContainer[] prefabs;
    public float waveTime;
    public Vector2[] spawnPoints;

    public List<GameObject> SpawnEnemies()
    {
        var toReturn = new List<GameObject>();
        foreach (var enemy in prefabs)
        {
            var numEnemies = Random.Range(enemy.min, enemy.max);
            for (int i = 0; i < numEnemies; i++)
            {
                toReturn.Add(ObjectPooler.Instance.SpawnFromPool(enemy.Prefab, Vector2.zero, Quaternion.identity));
            }
        }
        return toReturn;
    }
}

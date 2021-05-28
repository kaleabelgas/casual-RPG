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

[CreateAssetMenu(fileName = "Wave", menuName = "Wave")]
public class Wave : ScriptableObject
{
    [SerializeField] private EnemyContainer[] prefabs;
    public float TimeBeforeWave;

    public List<GameObject> SpawnEnemies()
    {
        var toReturn = new List<GameObject>();
        foreach (var enemy in prefabs)
        {
            var numEnemies = Random.Range(enemy.min, enemy.max);
            for (int i = 0; i < numEnemies; i++)
            {
                GameObject _toReturn = ObjectPooler.Instance.SpawnFromPool(enemy.Prefab, Vector2.down * 100, Quaternion.identity);
                _toReturn.SetActive(false);
                toReturn.Add(_toReturn);
            }
        }
        return toReturn;
    }
}

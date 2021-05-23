using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject objectPrefab;
        public int poolSize;
    }

    #region Singleton

    public static ObjectPooler Instance;

    private void Awake()
    {
        Instance = this;
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.poolSize; i++)
            {
                GameObject objectToPool = Instantiate(pool.objectPrefab, new Vector3(0, -100), Quaternion.identity);
                objectToPool.SetActive(false);
                objectPool.Enqueue(objectToPool);
            }

            poolDictionary.Add(pool.tag, objectPool);
        }
    }

    #endregion

    public Dictionary<string, Queue<GameObject>> poolDictionary;
    public List<Pool> pools;

    public GameObject SpawnFromPool(string tag, Vector2 position, Quaternion rotation)
    {
        if (!poolDictionary.ContainsKey(tag))
            return null;

        GameObject objectToSpawn = poolDictionary[tag].Dequeue();

        if(objectToSpawn != null && !objectToSpawn.activeInHierarchy)
        {

            objectToSpawn.SetActive(true);
            objectToSpawn.transform.position = position;
            objectToSpawn.transform.rotation = rotation;

            poolDictionary[tag].Enqueue(objectToSpawn);

            //Debug.Log("Pooler Used");

            return objectToSpawn;
        }
        else
        {
            foreach (Pool pool in pools)
            {
                if (pool.tag == tag)
                {
                    pool.poolSize++;
                    objectToSpawn = Instantiate(pool.objectPrefab, position, rotation);
                    poolDictionary[tag].Enqueue(objectToSpawn);
                    return objectToSpawn;
                }
            }
        }
        return null;
    }
}

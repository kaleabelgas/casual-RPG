using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField] private Wave[] waves;
    [SerializeField] private float timeBetweenEnemySpawn;
    private int _currentWave;
    private float _timeToNextWave;

    private void Start()
    {
        StartCoroutine(StartWave());
    }
    private IEnumerator StartWave()
    {
        while (true)
        {

            if (_currentWave + 1 > waves.Length) { yield break; }
            _timeToNextWave = waves[_currentWave].TimeBeforeWave;

            yield return new WaitForSeconds(_timeToNextWave);



            for (int i = 0; i < waves[_currentWave].spawnPoints.Length; i++)
            {
                List<GameObject> enemies = waves[_currentWave].SpawnEnemies();
                foreach (GameObject enemy in enemies)
                {
                    enemy.SetActive(true);
                    enemy.transform.position = waves[_currentWave].spawnPoints[i];
                    GameManager.AddEnemyToList(enemy);
                    yield return new WaitForSeconds(timeBetweenEnemySpawn);

                    //Debug.Log("Setting Position for " + enemy.name + waves[_currentWave].spawnPoints[i], enemy);
                }
            }

            _currentWave++;
        }

    }
}

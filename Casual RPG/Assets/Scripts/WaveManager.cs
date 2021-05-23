using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField] private Wave[] waves;
    [SerializeField] private float timeBetweenEnemySpawn;
    [SerializeField] private TransformPoints wayPoints;
    private int _currentWave;
    private float _timeToNextWave;

    private void Start()
    {
        StartCoroutine(StartWave());
    }

    [ContextMenu("Restart Wave")]
    private void RestartWaves()
    {
        StartCoroutine(StartWave());
    }

    private IEnumerator StartWave()
    {
        _currentWave = 0;
        while (true)
        {

            if (_currentWave + 1 > waves.Length) { yield break; }
            _timeToNextWave = waves[_currentWave].TimeBeforeWave;

            yield return new WaitForSeconds(_timeToNextWave);


            List<GameObject> enemies = waves[_currentWave].SpawnEnemies();
            foreach (GameObject enemy in enemies)
            {
                enemy.transform.position = transform.position;
                enemy.GetComponent<EnemyAI>().SetWayPoints(wayPoints);
                enemy.SetActive(true);
                GameManager.AddEnemyToList(enemy);
                yield return new WaitForSeconds(timeBetweenEnemySpawn);
            }

            _currentWave++;
        }

    }
}

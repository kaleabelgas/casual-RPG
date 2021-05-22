using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField] private Wave[] waves;
    private int _currentWave;
    private float _timeToNextWave;

    private void Update()
    {
        _timeToNextWave -= Time.deltaTime;

        if (_timeToNextWave <= 0) { StartWave(); }
    }
    private void StartWave()
    {
        if (_currentWave + 1 > waves.Length) { return; }
        _timeToNextWave = waves[_currentWave].waveTime;


        for (int i = 0; i < waves[_currentWave].spawnPoints.Length; i++)
        {
            List<GameObject> enemies = waves[_currentWave].SpawnEnemies();
            foreach (GameObject enemy in enemies)
            {
                enemy.transform.position = waves[_currentWave].spawnPoints[i];
                GameManager.AddEnemyToList(enemy);
                //Debug.Log("Setting Position for " + enemy.name + waves[_currentWave].spawnPoints[i], enemy);
            }
        }

        _currentWave++;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField] private Wave[] waves;
    private int _currentWave;

    private void Start()
    {
        StartCoroutine(StartWave());
    }

    private IEnumerator StartWave()
    {
        while (true)
        {
            Debug.Log($"Spawning {_currentWave}");
            if (_currentWave + 1 > waves.Length) { yield break; }
            for (int i = 0; i < waves[_currentWave].spawnPoints.Length; i++)
            {
                List<GameObject> enemies = waves[_currentWave].SpawnEnemies();
                foreach (GameObject enemy in enemies)
                {
                    enemy.transform.position = waves[_currentWave].spawnPoints[i];
                    Debug.Log("Setting Position for " + enemy.name + waves[_currentWave].spawnPoints[i], enemy);
                }
            }
            yield return new WaitForSeconds(waves[_currentWave].waveTime);
            ++_currentWave;

        }
    }
}

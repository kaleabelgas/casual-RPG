using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField] private Wave[] waves;
    [SerializeField] private float timeBetweenEnemySpawn;
    [SerializeField] private TransformPoints wayPoints;
    private int _currentWave;
    public float TimeToNextWave { get; private set; }

    private bool _wavesOngoing;

    private void Start()
    {
        TimeToNextWave = waves[_currentWave].TimeBeforeWave;
        GameManager.Instance.AddObjectToList(GameManager.ObjectLists.waves, gameObject);
    }

    [ContextMenu("Restart Wave")]
    private void RestartWaves()
    {
        _currentWave = 0;
        StartWave();
    }

    private void Update()
    {
        TimeToNextWave -= Time.deltaTime;
        if (_wavesOngoing) { StartWave(); }
        
    }

    private void StartWave()
    {
        if(TimeToNextWave > 0) { return; }

        StartCoroutine(SpawnWave());
        _currentWave++;

        if (_currentWave + 1 > waves.Length)
        {
            GameManager.Instance.StartWinGame();
            GameManager.Instance.RemoveObjectFromList(GameManager.ObjectLists.waves, gameObject);
            _wavesOngoing = false;
            return;
        }

        TimeToNextWave = waves[_currentWave].TimeBeforeWave;
    }

    private IEnumerator SpawnWave()
    {

        List<GameObject> enemies = waves[_currentWave].SpawnEnemies();
        if(enemies == null) { throw new System.Exception("ERROR: NO ENEMIES IN WAVE"); }
        foreach (GameObject enemy in enemies)
        {
            enemy.transform.position = transform.position;
            enemy.GetComponent<EnemyAI>().SetWayPoints(wayPoints);
            enemy.SetActive(true);
            GameManager.Instance.AddObjectToList(GameManager.ObjectLists.enemy, enemy);
            yield return new WaitForSeconds(timeBetweenEnemySpawn);
        }
    }
}

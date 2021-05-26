using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIGameScene : MonoBehaviour
{
    [SerializeField] private WaveManager waveManager;
    [SerializeField] private TextMeshProUGUI currentWaveText;
    [SerializeField] private TextMeshProUGUI timeToNextWaveText;

    private int _currentWave;

    private void Start()
    {
        waveManager.OnWaveIterate += UpdateCurrentLevelText;
    }

    private void Update()
    {
        timeToNextWaveText.text = $"Time Until Next Wave: {(int)waveManager.TimeToNextWave}";
    }

    private void UpdateCurrentLevelText()
    {
        _currentWave++;
        currentWaveText.text = $"Current Wave: {_currentWave}";
    }
}

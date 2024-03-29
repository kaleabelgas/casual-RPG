using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class UIGameScene : MonoBehaviour
{
    [SerializeField] private WaveManager waveManager;
    [SerializeField] private TextMeshProUGUI currentWaveText;
    [SerializeField] private TextMeshProUGUI timeToNextWaveText;
    [SerializeField] private TextMeshProUGUI timeUntilRespawn;

    [Header("Game Panels")]
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject gameOverMenu;
    [SerializeField] private GameObject gameWinMenu;
    [SerializeField] private GameObject lastWinMenu;
    [SerializeField] private GameObject respawnMenu;

    private PlayerHealthManager playerHealth;
    private PlayerController playerController;

    private int _currentWave;
    private bool isPaused;
    private bool isCurrentlyDead;

    bool _changeScenePressed = false;

    private void Awake()
    {
        playerHealth = FindObjectOfType<PlayerHealthManager>();
        playerController = FindObjectOfType<PlayerController>();
        waveManager = FindObjectOfType<WaveManager>();
    }
    private void Start()
    {
        waveManager.OnWaveIterate += UpdateCurrentLevelText;
        currentWaveText.text = $"Current Wave: 1";
        playerHealth.OnPlayerDeath += BringUpRespawnPanel;
    }

    private void Update()
    {
        timeToNextWaveText.text = $"Time Until Next Wave: {Mathf.CeilToInt(waveManager.TimeToNextWave)}";

        if (isCurrentlyDead)
        {
            timeUntilRespawn.text = $"Respawning in {Mathf.CeilToInt(playerController.RespawnClock)}";

            if (playerController.RespawnClock <= 0)
            {
                isCurrentlyDead = false;
                respawnMenu.SetActive(false);
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape)) { Pause(); }
    }

    private void BringUpRespawnPanel()
    {
        isCurrentlyDead = true;
        respawnMenu.SetActive(true);
    }

    private void UpdateCurrentLevelText()
    {
        _currentWave++;
        currentWaveText.text = $"Current Wave: {_currentWave}";
    }

    public void GoToNextLevel()
    {
        StartCoroutine(LoadAsync(SceneManager.GetActiveScene().buildIndex + 1));
    }

    public void GoToMainMenu()
    {
        StartCoroutine(LoadAsync(0));
    }

    public void Pause()
    {
        isPaused = !isPaused;
        if (isPaused)
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            pauseMenu.SetActive(false);
            Time.timeScale = 1;
        }

    }

    public void EnableGameOverScreen()
    {
        gameOverMenu.SetActive(true);
    }

    public void EnableWinGameScreen()
    {
        if (SceneManager.GetActiveScene().buildIndex + 3 > SceneManager.sceneCountInBuildSettings)
        {
            EnableLastWinScreen();
            return;
        }

        gameWinMenu.SetActive(true);
    }

    public void EnableLastWinScreen()
    {
        lastWinMenu.SetActive(true);
    }

    public void RestartLevel()
    {
        StartCoroutine(LoadAsync(SceneManager.GetActiveScene().buildIndex));
    }

    private IEnumerator LoadAsync(int _index)
    {
        if (_changeScenePressed) { yield break; }
        AudioManager.instance.Stop("music");

        _changeScenePressed = true;
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(_index);

        while (!asyncOperation.isDone)
        {
            yield return null;
        }
    }
}

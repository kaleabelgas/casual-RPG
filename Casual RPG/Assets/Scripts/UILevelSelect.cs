using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UILevelSelect : MonoBehaviour
{
    [SerializeField] private Button[] levelButtons;

    private bool _hasSelected;

    private void Start()
    {
        int levelReached = PlayerPrefs.GetInt("LASTLEVELREACHED", 1);

        for (int i = 0; i < levelButtons.Length; i++)
        {
            if(i + 1> levelReached)
            {
                levelButtons[i].interactable = false;
            }
        }
    }

    public void OpenLevel(int _level)
    {
        StartCoroutine(LoadAsync(_level));
    }

    private IEnumerator LoadAsync(int _level)
    {
        if (_hasSelected) { yield break; }
        AudioManager.instance.Stop("mainmenu");
        _hasSelected = true;
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(_level);
        while (!asyncOperation.isDone)
        {
            yield return null;

        }
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
}

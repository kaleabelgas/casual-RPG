using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UILevelSelect : MonoBehaviour
{
    [SerializeField] private Button[] levelButtons;

    private void Start()
    {
        int levelReached = PlayerPrefs.GetInt("HIGHESTLEVELREACHED", 1);

        for (int i = 0; i < levelButtons.Length; i++)
        {
            if(i > 0)
            {
                levelButtons[i].interactable = false;
            }
        }
    }

    public void OpenLevel(int _level)
    {
        SceneManager.LoadScene(_level);
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
}

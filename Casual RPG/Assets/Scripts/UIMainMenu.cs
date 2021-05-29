using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIMainMenu : MonoBehaviour
{
    private void Start()
    {
        if (AudioManager.instance.IsPlaying("mainmenu")) { return; }
        AudioManager.instance.Play("mainmenu");
    }
    public void OpenLevelSelect()
    {
        SceneManager.LoadScene("Level Select");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DialogueManager : MonoBehaviour
{
    [SerializeField] private GameObject[] dialogues;

    private int _currentDialogueIndex;

    private void Start()
    {
        Time.timeScale = 0f;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return)) { ShowNextMessage(); }
    }

    private void ShowNextMessage()
    {
        _currentDialogueIndex++;


        for (int i = 0; i < dialogues.Length; i++)
        {
            if(_currentDialogueIndex + 1 > dialogues.Length)
            {
                GameManager.Instance.StartWaveSpawn();
                gameObject.SetActive(false);
            }

            if(i == _currentDialogueIndex)
            {
                dialogues[i].SetActive(true);
            }
            else
            {
                dialogues[i].SetActive(false);
            }
        }
    }
}

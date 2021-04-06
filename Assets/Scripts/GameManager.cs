using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private bool _isGameOver;

    [SerializeField]
    private GameObject _pauseMenuPanel;

    private void Update()
    {
        //if R key was pressed
        if (Input.GetKeyDown(KeyCode.R) && _isGameOver == true)
        {
            SceneManager.LoadScene(1); //restart current game scene
        }

        //if P key was pressed
        if (Input.GetKeyDown(KeyCode.P))
        {
            _pauseMenuPanel.SetActive(true); //pause the game and show Pause Menu Panel
            Time.timeScale = 0;
        }
    }

    //resume game
    public void ResumeGame()
    {
        _pauseMenuPanel.SetActive(false);
        Time.timeScale = 1.0f;
    }

    public void GameOver()
    {
        _isGameOver = true;
    }
}



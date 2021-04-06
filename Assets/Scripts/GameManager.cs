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

        if (Input.GetKeyDown(KeyCode.P))
        {
            _pauseMenuPanel.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void GameOver()
    {
        _isGameOver = true;
    }
}



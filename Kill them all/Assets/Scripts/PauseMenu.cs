using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject inputSystem;
    public GameObject pauseMenuUI;
    private bool isGamePaused = false;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(isGamePaused == true)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        isGamePaused = false;
        inputSystem.SetActive(true);
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
    }

    private void Pause()
    {
        isGamePaused = true;
        inputSystem.SetActive(false);
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
    }

    public void HomeButton()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("StartMenu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}

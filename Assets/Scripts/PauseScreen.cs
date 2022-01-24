using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseScreen : MonoBehaviour
{
    public static bool isPaused = false;

    public GameObject pauseScreenUI;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
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
        pauseScreenUI.SetActive(false);
        Time.timeScale = 1;
        isPaused = false;
    }

    void Pause()
    {
        pauseScreenUI.SetActive(true);
        Time.timeScale = 0;
        isPaused = true;
    }

    public void MainMenu()
    {
        //Debug.Log("goto menu");
        //Time.timeScale = 1;
        SceneManager.LoadScene("MainMenuScreen");
    }

    public void QuitGame()
    {
        //Debug.Log("quit");
        Application.Quit();
    }
}

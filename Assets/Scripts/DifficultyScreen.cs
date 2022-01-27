using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DifficultyScreen : MonoBehaviour
{
    public GameObject OpeningScreen;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Back();
        }
    }

    public void Back()
    {
        OpeningScreen.SetActive(true);
        this.gameObject.SetActive(false);
    }

    public void EasyButton()
    {
        GameStateController.difficultySetting = 0;
        SceneManager.LoadScene("GameScene");
    }

    public void MediumButton()
    {
        GameStateController.difficultySetting = 1;
        SceneManager.LoadScene("GameScene");
    }

    public void HardButton()
    {
        GameStateController.difficultySetting = 2;
        SceneManager.LoadScene("GameScene");
    }
}

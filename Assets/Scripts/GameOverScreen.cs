using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    public Text distanceText;

    public void Setup(int score)
    {
        gameObject.SetActive(true);
        distanceText.text = "Distance reached: \n" + score.ToString() + " meters";
    }

    public void RestartButton()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void MainMenuButton()
    {
        SceneManager.LoadScene("MainMenuScreen");
    }
}

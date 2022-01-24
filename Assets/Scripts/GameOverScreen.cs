using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverScreen : MonoBehaviour
{
    public TextMeshProUGUI distanceText;

    public void Setup(int score)
    {
        Time.timeScale = 0;
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

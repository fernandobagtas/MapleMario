using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpeningScreen : MonoBehaviour
{
    public GameObject TutorialScreen;
    public GameObject DifficultyScreen;

    private void Awake()
    {
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Play()
    {
        DifficultyScreen.SetActive(true);
        this.gameObject.SetActive(false);
        //SceneManager.LoadScene("GameScene");
    }

    public void HowTo()
    {
        TutorialScreen.SetActive(true);
        this.gameObject.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialScreen : MonoBehaviour
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

}

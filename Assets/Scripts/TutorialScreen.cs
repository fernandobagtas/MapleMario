using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialScreen : MonoBehaviour
{
    public GameObject OpeningScreen;

    public void Back()
    {
        OpeningScreen.SetActive(true);
        this.gameObject.SetActive(false);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{
    public void PressPlay()
    {
        SceneManager.LoadScene("MainScene");
    }


    public void PressExit()
    {
        Application.Quit();
    }

    public void PressCredits()
    {

    }
}

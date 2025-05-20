using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{

    private void Awake()
    {

    }

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

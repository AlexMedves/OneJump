using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{
    public SaveSystem saveSystem;
    string path;

    private void Awake()
    {
        saveSystem = this.GetComponent<SaveSystem>();
        path = saveSystem.GetFilePath("SaveData.oj");
    }

    public void PressPlay()
    {
        if (!File.Exists(path))
        {
            SceneManager.LoadScene("TutorialScene");
        }
        else
        {
            SceneManager.LoadScene("MainScene");

        }
    }


    public void PressExit()
    {
        Application.Quit();
    }

    public void PressCredits()
    {

    }
}

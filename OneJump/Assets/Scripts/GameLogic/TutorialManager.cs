using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class TutorialManager : MonoBehaviour
{
    GameManager gameManager;

    public GameObject[] tutorialPopUps;
    private int currentPopUpID;

    public int numberOfTaps;

    public float waitTimer = 2f;

    private bool isAllowedToSkip = true;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        GameManager.isInTutorial = true;
    }


    private void Update()
    {

        for(int i = 0; i < tutorialPopUps.Length; i++)
        {
            if(i == currentPopUpID)
            {
                tutorialPopUps[i].SetActive(true);
                
            }
            else
            {
                tutorialPopUps[i].SetActive(false);
            }
        }

        Skipper();


        if(currentPopUpID == 2)
        {
            gameManager.mineral1Text.gameObject.SetActive(true);
            gameManager.mineral2Text.gameObject.SetActive(true);
            gameManager.mineral3Text.gameObject.SetActive(true);
        }

        if(currentPopUpID == 3)
        {
            if (!GameManager.isInTutorial)
            {
                currentPopUpID++;
                numberOfTaps = 0;
            }
        }



    }


    public void tapToSkip() //it's in the name
    {
        numberOfTaps++;
    }

    private void Skipper()
    {
        if (isAllowedToSkip)
        {
            if (numberOfTaps == 1)
            {
                TypeWriter.isSkipping = true;
            }
            if (numberOfTaps >= 2)
            {
                currentPopUpID++;
                numberOfTaps = 0;
            }
        }
    }
}

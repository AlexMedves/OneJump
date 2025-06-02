using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class TutorialManager : MonoBehaviour
{
    GameManager gameManager;

    public GameObject[] tutorialPopUps;
   [SerializeField] private int currentPopUpID;

    public int numberOfTaps;


    GameObject clickButton;
    GameObject tutBotten;

    [SerializeField] GameObject upgradeButton1;
    [SerializeField] GameObject upgradeButton2;
    [SerializeField] GameObject upgradeButton3;


    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        GameManager.IsInTutorial = true;
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

        if(clickButton == null)
        {
            clickButton = GameObject.Find("AddMoneyButton");
        }
        if(tutBotten == null)
        {
            tutBotten = GameObject.Find("TutorialButton");
        }

        Skipper();

        switch (currentPopUpID)
        {
            case 0:
                break;
            case 1:
                break;
            case 2:
                gameManager.mineral1Text.gameObject.SetActive(true);
                gameManager.mineral2Text.gameObject.SetActive(true);
                gameManager.mineral3Text.gameObject.SetActive(true);
                break;
            case 3:
                clickButton.SetActive(false);
                gameManager.mineralButton1.SetActive(true);
                gameManager.mineralButton2.SetActive(true);
                gameManager.mineralButton3.SetActive(true);


                tutBotten.SetActive(false);

                if (!GameManager.IsInTutorial)
                {
                    currentPopUpID++;
                    numberOfTaps = 0;
                }
                break;
            case 4:
                if (gameManager.mineral1Amount > 0 || gameManager.mineral2Amount > 0 || gameManager.mineral3Amount > 0)
                {
                    currentPopUpID++;
                }
                break;
            case 5:
                if (gameManager.mineral1Amount >= 3 || gameManager.mineral2Amount >= 3 || gameManager.mineral3Amount >= 3)
                {
                    currentPopUpID++;
                }
                break;
            case 6:
                clickButton.SetActive(false);
                tutBotten.SetActive(true);

                upgradeButton1.SetActive(true);
                upgradeButton2.SetActive(true);
                upgradeButton3.SetActive(true);
                currentPopUpID++;
                break;
            case 7:
                clickButton.SetActive(true);
                tutBotten.SetActive(false);

                if(gameManager.mineral1Amount >= 20 || gameManager.mineral2Amount >=20 || gameManager.mineral3Amount >=20)
                {
                    currentPopUpID++;
                    clickButton.SetActive(false);
                }
                break;
            case 8:

                Planet planetData;
                planetData = GameManager.currentSelectedPlanet.GetComponent<Planet>();
                if(planetData.mineral1UpgradeLvl > 1 || planetData.mineral2UpgradeLvl > 1 || planetData.mineral3UpgradeLvl > 1)
                {
                    currentPopUpID++;
                }
                break;
        }

    }


    public void TapToSkip() //it's in the name
    {
        numberOfTaps++;
    }

    private void Skipper()
    {
        //If you click the screen and the text isn't complete, the text skips
        if(numberOfTaps == 1 && !TypeWriter.IsTextComplete)
        {
            TypeWriter.isSkipping = true;
            numberOfTaps = 0;
        }
        else if(numberOfTaps == 1 && TypeWriter.IsTextComplete)
        {
            currentPopUpID++;
            numberOfTaps = 0;
        }
        //else if you click and the text is full, then it goes next pop up.
    }
}

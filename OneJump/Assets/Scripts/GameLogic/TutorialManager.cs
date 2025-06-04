using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour
{
    GameManager gameManager;
    Planet planetObject;

    public GameObject[] tutorialPopUps;
   [SerializeField] private int currentPopUpID;

    public int numberOfTaps;


    [SerializeField] private GameObject clickButton;
    [SerializeField] private GameObject tutBotten;

    [SerializeField] GameObject upgradeButton1;
    [SerializeField] GameObject upgradeButton2;
    [SerializeField] GameObject upgradeButton3;


    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        GameManager.IsInTutorial = true;
        UpgradeManager.isInTutorial = true;
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
        planetObject = GameManager.currentSelectedPlanet.GetComponent<Planet>();

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

                gameManager.relumText.gameObject.SetActive(true);
                gameManager.kupruText.gameObject.SetActive(true);
                gameManager.trevleockText.gameObject.SetActive(true);
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
                clickButton.SetActive(true);
                if (gameManager.mineral1Amount > 0 || gameManager.mineral2Amount > 0 || gameManager.mineral3Amount > 0)
                {
                    currentPopUpID++;
                }
                break;
            case 5:
                if (gameManager.mineral1Amount >= 10 || gameManager.mineral2Amount >= 10 || gameManager.mineral3Amount >= 10)
                {
                    currentPopUpID++;
                }
                break;
            case 6:
                clickButton.SetActive(false);
                tutBotten.SetActive(true);
                UpgradeManager.isInTutorial = false;
                upgradeButton1.SetActive(true);
                upgradeButton2.SetActive(true);
                upgradeButton3.SetActive(true);
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
                clickButton.SetActive(false);
                if(planetObject.mineral1UpgradeLvl > 1 || planetObject.mineral2UpgradeLvl > 1 || planetObject.mineral3UpgradeLvl > 1)
                {
                    currentPopUpID++;
                    tutBotten.SetActive(true);
                }
                break;
            case 9:
                break;
            case 10:
                tutBotten.SetActive(false);
                upgradeButton1.SetActive(false);
                upgradeButton2.SetActive(false);
                upgradeButton3.SetActive(false);

                gameManager.mineral1Text.gameObject.SetActive(false);
                gameManager.relumText.gameObject.SetActive(false);
                gameManager.mineral2Text.gameObject.SetActive(false);
                gameManager.kupruText.gameObject.SetActive(false);
                gameManager.mineral3Text.gameObject.SetActive(false);
                gameManager.trevleockText.gameObject.SetActive(false);

                if(planetObject.planetSpinSpeed != 1500)
                {
                    planetObject.planetSpinSpeed++;

                    if(planetObject.mineral1UpgradeLvl != 50)
                    {
                        planetObject.mineral1UpgradeLvl++;
                        planetObject.mineral1MadePerSecond += 2 + planetObject.mineral1UpgradeLvl;
                    }
                    if (planetObject.mineral2UpgradeLvl != 50)
                    {
                        planetObject.mineral2UpgradeLvl++;
                        planetObject.mineral2MadePerSecond += 2 + planetObject.mineral2UpgradeLvl;
                    }
                    if (planetObject.mineral3UpgradeLvl != 50)
                    {
                        planetObject.mineral3UpgradeLvl++;
                        planetObject.mineral3MadePerSecond += 2 + planetObject.mineral3UpgradeLvl;
                    }
                }

                if(planetObject.planetSpinSpeed == 1500)
                {
                    currentPopUpID++;
                }
                break;
            case 11:
                if(planetObject.planetSpinSpeed > 13)
                {
                    planetObject.planetSpinSpeed--;
                }
                if(planetObject.planetSpinSpeed == 13)
                {
                    tutBotten.SetActive(true);
                    upgradeButton1.SetActive(true);
                    upgradeButton2.SetActive(true);
                    upgradeButton3.SetActive(true);

                    gameManager.mineral1Text.gameObject.SetActive(true);
                    gameManager.relumText.gameObject.SetActive(true);
                    gameManager.mineral2Text.gameObject.SetActive(true);
                    gameManager.kupruText.gameObject.SetActive(true);
                    gameManager.mineral3Text.gameObject.SetActive(true);
                    gameManager.trevleockText.gameObject.SetActive(true);
                    currentPopUpID++;
                }
                break;
            case 12:
                break;
            case 13:
                tutBotten.SetActive(false);
                GameManager.automatedLeftRightButtons = true;

                if(GameManager.currentPlanetIndex > 0)
                {
                    tutBotten.SetActive(true);
                    currentPopUpID++;
                }
                break;
            case 14:
                GameManager.automatedLeftRightButtons = false;
                tutBotten.SetActive(true);
                break;
            case 15:
                tutBotten.SetActive(false);
                if (GameManager.currentPlanetIndex == 1 && planetObject.isPlanetUnlocked)
                {
                    GameManager.automatedLeftRightButtons = true;
                    currentPopUpID++;

                }
                break;
            case 16:
                if (GameManager.currentPlanetIndex == 0)
                {
                    currentPopUpID++;
                }
                break;
            case 17:
                tutBotten.SetActive(true);
                clickButton.SetActive(false);
                break;
            case 18:
                tutBotten.SetActive(false);
                clickButton.SetActive(false);
                GameManager.automatedLeftRightButtons = true;
                GameManager.IsInTutorial = false;
                //Debug.Log("Loaded MainScene / Come back to this and delete before build");
                SceneManager.LoadScene("MainScene");
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

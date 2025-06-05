using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeManager : MonoBehaviour
{
    [SerializeField] private GameObject upgrade1Button;
    [SerializeField] private GameObject upgrade2Button;
    [SerializeField] private GameObject upgrade3Button;

    [SerializeField] TMP_Text upgrade1PriceText;
    [SerializeField] TMP_Text upgrade2PriceText; //Set these in the tutorial scene cause they been de-referenced.
    [SerializeField] TMP_Text upgrade3PriceText;

    [SerializeField] TMP_Text upgrade1Level;
    [SerializeField] TMP_Text upgrade2Level;
    [SerializeField] TMP_Text upgrade3Level;

    [SerializeField] TMP_Text mineral1MadePerSecond;
    [SerializeField] TMP_Text mineral2MadePerSecond;
    [SerializeField] TMP_Text mineral3MadePerSecond;

    [SerializeField] TMP_Text mineral1Completed;
    [SerializeField] TMP_Text mineral2Completed;
    [SerializeField] TMP_Text mineral3Completed;

    [SerializeField] TMP_Text planetPriceText;
    private int planetPrice;

    static public bool isInTutorial;

    Planet planetComponent;
    GameManager gameManager;

    [SerializeField] private GameObject unlockPlanetButton;
    [SerializeField] private GameObject clickerButton;


    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        InvokeRepeating(nameof(GetCurrentPlanetStats), 0.2f, 0.3f);
    }
    // Update is called once per frame
    void Update()
    {
        if (planetComponent != null)
        {
            upgrade1PriceText.SetText($"Price: {planetComponent.planetUpgradePrice[0]} Relum, {planetComponent.upgr1Incr} Kupru."); //Needs this much x material, this much y material, this much z material.
            upgrade2PriceText.SetText($"Price: {planetComponent.planetUpgradePrice[1]} Kupru, {planetComponent.upgr2Incr} Relum.");
            upgrade3PriceText.SetText($"Price: {planetComponent.planetUpgradePrice[2]} Trevleock, {planetComponent.upgr3Incr} Relum."); //this can have another material if needed.

            upgrade1Level.SetText($"Level: {planetComponent.mineral1UpgradeLvl}");
            upgrade2Level.SetText($"Level: {planetComponent.mineral2UpgradeLvl}");
            upgrade3Level.SetText($"Level: {planetComponent.mineral3UpgradeLvl}");

            mineral1MadePerSecond.SetText($"Relum/s: {planetComponent.mineral1MadePerSecond}");
            mineral2MadePerSecond.SetText($"Kupru/s: {planetComponent.mineral2MadePerSecond}");
            mineral3MadePerSecond.SetText($"Trevleock/s: {planetComponent.mineral3MadePerSecond}");

            planetPrice = planetComponent.planetResearchValue;
            planetPriceText.SetText($"Unlock : {planetComponent.planetName} for {planetPrice} ?");

            CheckUpgradeStats();

            if (!isInTutorial)
            {
                if (planetComponent.isPlanetUnlocked)
                {
                    unlockPlanetButton.SetActive(false);
                    clickerButton.SetActive(true);
                    upgrade1Button.SetActive(true);
                    upgrade2Button.SetActive(true);
                    upgrade3Button.SetActive(true);
                }
                else
                {
                    unlockPlanetButton.SetActive(true);
                    clickerButton.SetActive(false);
                    upgrade1Button.SetActive(false);
                    upgrade2Button.SetActive(false);
                    upgrade3Button.SetActive(false);
                }
            }
        }

    }

    private void CheckUpgradeStats() //Rewrite this, it is horrible performance wise. Please.
    {
        //Upgrade1
        if(planetComponent.mineral1UpgradeLvl != 50 && gameManager.mineral1Amount >= planetComponent.planetUpgradePrice[0] && gameManager.mineral2Amount >= planetComponent.upgr1Incr)
        {
            mineral1Completed.gameObject.SetActive(false);
            upgrade1PriceText.gameObject.SetActive(true);
            upgrade1Button.GetComponent<Image>().color = new Color(0.3243f, 1, 0.8546f);
        }
        else if (planetComponent.mineral1UpgradeLvl == 50)
        {
            upgrade1Button.GetComponent<Image>().color = new Color(0.9731f, 0.4952f, 1);
            mineral1Completed.gameObject.SetActive(true);
            upgrade1PriceText.gameObject.SetActive(false);
        }
        else
        {
            mineral1Completed.gameObject.SetActive(false);
            upgrade1PriceText.gameObject.SetActive(true);
            upgrade1Button.GetComponent<Image>().color = new Color(1, 0, 0);
        }
        //Upgrade1

        //Upgrade2
        if(planetComponent.mineral2UpgradeLvl != 50 && gameManager.mineral2Amount >= planetComponent.planetUpgradePrice[1] && gameManager.mineral1Amount >= planetComponent.upgr2Incr)
        {
            mineral2Completed.gameObject.SetActive(false);
            upgrade2PriceText.gameObject.SetActive(true);
            upgrade2Button.GetComponent<Image>().color = new Color(0.3243f, 1, 0.8546f);
        }
        else if(planetComponent.mineral2UpgradeLvl == 50)
        {
            upgrade2Button.GetComponent<Image>().color = new Color(0.9731f, 0.4952f, 1);
            mineral2Completed.gameObject.SetActive(true);
            upgrade2PriceText.gameObject.SetActive(false);
        }
        else
        {
            mineral2Completed.gameObject.SetActive(false);
            upgrade2PriceText.gameObject.SetActive(true);
            upgrade2Button.GetComponent<Image>().color = new Color(1, 0, 0);
        }
        //Upgrade2

        //Upgrade3
        if(planetComponent.mineral3UpgradeLvl != 50 && gameManager.mineral3Amount >= planetComponent.planetUpgradePrice[2] && gameManager.mineral1Amount >= planetComponent.upgr3Incr && gameManager.mineral2Amount >= planetComponent.upgr3Incr2)
        {
            mineral3Completed.gameObject.SetActive(false);
            upgrade3PriceText.gameObject.SetActive(true);
            upgrade3Button.GetComponent<Image>().color = new Color(0.3243f, 1, 0.8546f);
        }
        else if(planetComponent.mineral3UpgradeLvl == 50)
        {
            upgrade3Button.GetComponent<Image>().color = new Color(0.9731f, 0.4952f, 1);
            mineral3Completed.gameObject.SetActive(true);
            upgrade3PriceText.gameObject.SetActive(false);
        }
        else
        {
            mineral3Completed.gameObject.SetActive(false);
            upgrade3PriceText.gameObject.SetActive(true);
            upgrade3Button.GetComponent<Image>().color = new Color(1, 0, 0);
        }
        //Upgrade3
    }

    public void PressUpgrade1()
    {
        if (planetComponent.isPlanetUnlocked && planetComponent.mineral1UpgradeLvl < 50)
        {
            if (gameManager.mineral1Amount >= planetComponent.planetUpgradePrice[0] && gameManager.mineral2Amount >= planetComponent.upgr1Incr)
            {
                planetComponent.mineral1MadePerSecond += 2 + planetComponent.mineral1UpgradeLvl; // make this much more per second.
                gameManager.mineral1Amount -= planetComponent.planetUpgradePrice[0];

                gameManager.mineral2Amount -= planetComponent.upgr1Incr;

                planetComponent.upgr1Incr += 20;

                planetComponent.planetUpgradePrice[0] = planetComponent.planetUpgradePrice[0] + (int)Mathf.Ceil(planetComponent.mineral1UpgradeLvl / 2);
                planetComponent.mineral1UpgradeLvl++;
            }
        }
    }

    public void PressUpgrade2()
    {
        if (planetComponent.isPlanetUnlocked && planetComponent.mineral2UpgradeLvl < 50)
        {
            if (gameManager.mineral2Amount >= planetComponent.planetUpgradePrice[1] && gameManager.mineral1Amount >= planetComponent.upgr2Incr)
            {
                planetComponent.mineral2MadePerSecond += 2 + planetComponent.mineral2UpgradeLvl; //This needs to be an incremental value

                gameManager.mineral2Amount -= planetComponent.planetUpgradePrice[1];
                gameManager.mineral1Amount -= planetComponent.upgr2Incr;
                planetComponent.upgr2Incr += 20;

                planetComponent.planetUpgradePrice[1] = planetComponent.planetUpgradePrice[1] + (int)Mathf.Ceil(planetComponent.mineral2UpgradeLvl / 2);
                planetComponent.mineral2UpgradeLvl++;
            }
        }
    }

    public void PressUpgrade3()
    {
        if (planetComponent.isPlanetUnlocked && planetComponent.mineral3UpgradeLvl < 50)
        {
            if (gameManager.mineral3Amount >= planetComponent.planetUpgradePrice[2] && gameManager.mineral1Amount >= planetComponent.upgr3Incr) //&& gameManager.mineral2Amount >= planetComponent.upgr3Incr2)
            {
                planetComponent.mineral3MadePerSecond += 2 + planetComponent.mineral3UpgradeLvl; //This needs to be an incremental value

                gameManager.mineral3Amount -= planetComponent.planetUpgradePrice[2];
                gameManager.mineral1Amount -= planetComponent.upgr3Incr;
                //gameManager.mineral2Amount -= planetComponent.upgr3Incr2;

                planetComponent.upgr3Incr += 20;
                planetComponent.upgr3Incr2 += 20;

                planetComponent.planetUpgradePrice[2] = planetComponent.planetUpgradePrice[2] + (int)Mathf.Ceil(planetComponent.mineral2UpgradeLvl / 2);
                planetComponent.mineral3UpgradeLvl++;
            }
        }
    }

    public void PressPurchasePlanet() //Purchase Planet
    {
        if (gameManager.mineral1Amount >= planetPrice && gameManager.mineral2Amount >= planetPrice && gameManager.mineral3Amount >= planetPrice)
        {

            gameManager.mineral1Amount -= planetPrice;
            gameManager.mineral2Amount -= planetPrice;
            gameManager.mineral3Amount -= planetPrice;

            planetComponent.isPlanetUnlocked = true;

        }
    }

    private void GetCurrentPlanetStats()
    {
        if (GameManager.currentSelectedPlanet != null)
        {
            planetComponent = GameManager.currentSelectedPlanet.GetComponent<Planet>(); //Gets the planet component, put it in a function to call invoke every few seconds.
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeManager : MonoBehaviour
{
    [SerializeField] TMP_Text upgrade1;
    [SerializeField] TMP_Text upgrade2;
    [SerializeField] TMP_Text upgrade3;

    [SerializeField] TMP_Text upgrade1Level;
    [SerializeField] TMP_Text upgrade2Level;
    [SerializeField] TMP_Text upgrade3Level;

    [SerializeField] TMP_Text planetPriceText;
    private int planetPrice;

    

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
            upgrade1.SetText($"Price : {planetComponent.planetUpgradePrice[0]} Relum, {planetComponent.upgr1Incr} Kupru."); //Needs this much x material, this much y material, this much z material.
            upgrade2.SetText($"Price : {planetComponent.planetUpgradePrice[1]} Kupru, {planetComponent.upgr2Incr} Relum.");
            upgrade3.SetText($"Price : {planetComponent.planetUpgradePrice[2]} Trevleock, {planetComponent.upgr3Incr} Relum, {planetComponent.upgr3Incr2} Kupru");

            upgrade1Level.SetText($"Level : {planetComponent.mineral1UpgradeLvl}");
            upgrade2Level.SetText($"Level : {planetComponent.mineral2UpgradeLvl}");
            upgrade3Level.SetText($"Level : {planetComponent.mineral3UpgradeLvl}");

            planetPrice = planetComponent.planetResearchValue;
            planetPriceText.SetText($"Unlock : {planetComponent.planetName}? Price: {planetPrice} of each resource.");
            

            if(!planetComponent.isPlanetUnlocked)
            {
                unlockPlanetButton.SetActive(true);
                clickerButton.SetActive(false);
            }
            else
            {
                unlockPlanetButton.SetActive(false);
                clickerButton.SetActive(true);
            }
        }

    }

    public void PressUpgrade1()
    {
        if (planetComponent.isPlanetUnlocked && planetComponent.mineral1UpgradeLvl < 50)
        {
            if (gameManager.mineral1Amount >= planetComponent.planetUpgradePrice[0] && gameManager.mineral2Amount >= planetComponent.upgr1Incr)
            {
                //planetComponent.mineral1MadePerSecond += 2 + planetComponent.mineral1UpgradeLvl; // make this much more per second.
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
                /*planetComponent.mineral2MadePerSecond += 2 + planetComponent.mineral2UpgradeLvl;*/ //This needs to be an incremental value

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
            if (gameManager.mineral3Amount >= planetComponent.planetUpgradePrice[2] && gameManager.mineral1Amount >= planetComponent.upgr3Incr && gameManager.mineral2Amount >= planetComponent.upgr3Incr2)
            {
                /*planetComponent.mineral3MadePerSecond += 2 + planetComponent.mineral3UpgradeLvl;*/ //This needs to be an incremental value

                gameManager.mineral3Amount -= planetComponent.planetUpgradePrice[2];
                gameManager.mineral1Amount -= planetComponent.upgr3Incr;
                gameManager.mineral2Amount -= planetComponent.upgr3Incr2;

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

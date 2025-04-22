using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    [SerializeField] TMP_Text upgrade1;
    [SerializeField] TMP_Text upgrade2;
    [SerializeField] TMP_Text upgrade3;

    [SerializeField] TMP_Text upgrade1Level;
    [SerializeField] TMP_Text upgrade2Level;
    [SerializeField] TMP_Text upgrade3Level;

    Planet planetComponent;
    GameManager gameManager;


    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        InvokeRepeating(nameof(GetCurrentPlanetStats), 1f, 1f);
    }

    // Update is called once per frame
    void Update()
    {

        if(planetComponent != null)
        {
            upgrade1.SetText("Upgrade1 : " + planetComponent.planetUpgrades[0]);
            upgrade2.SetText("Upgrade2 : " + planetComponent.planetUpgrades[1]);
            upgrade3.SetText("Upgrade3 : " + planetComponent.planetUpgrades[2]);

            upgrade1Level.SetText($"Level :  {planetComponent.mineral1UpgradeLvl}");
            upgrade2Level.SetText($"Level :  {planetComponent.mineral2UpgradeLvl}");
            upgrade3Level.SetText($"Level :  {planetComponent.mineral3UpgradeLvl}");
        }

    }

    public void PressUpgrade1()
    {
        if(GameManager.mineral1Value >= planetComponent.planetUpgrades[0])
        {
            planetComponent.mineral1MadePerSecond += 20;
            GameManager.mineral1Value -= planetComponent.planetUpgrades[0];

            planetComponent.planetUpgrades[0] = planetComponent.planetUpgrades[0] + (int)Mathf.Ceil(planetComponent.mineral1UpgradeLvl / 2);
            //Debug.Log($"{planetComponent.planetName}Upgrade1 Level: {planetComponent.mineral1UpgradeLvl}");
            planetComponent.mineral1UpgradeLvl++;
        } 
    }

    public void PressUpgrade2()
    {
        if (GameManager.mineral2Value >= planetComponent.planetUpgrades[1])
        {
            planetComponent.mineral2MadePerSecond += 20;
            GameManager.mineral2Value -= planetComponent.planetUpgrades[1];

            planetComponent.planetUpgrades[1] = planetComponent.planetUpgrades[1] + (int)Mathf.Ceil(planetComponent.mineral2UpgradeLvl / 2);
            //Debug.Log($"{planetComponent.planetName}Upgrade1 Level: {planetComponent.mineral1UpgradeLvl}");
            planetComponent.mineral2UpgradeLvl++;
        }
    }

    private void GetCurrentPlanetStats()
    {
        if (GameManager.currentSelectedPlanet != null)
        {
            planetComponent = GameManager.currentSelectedPlanet.GetComponent<Planet>(); //Gets he planet component, put it in a function to call invoke every few seconds.
        }
    }
}

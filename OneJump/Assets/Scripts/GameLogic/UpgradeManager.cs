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

    private int upgr1Incr = 50;

    private int upgr2Incr = 50;

    private int upgr3Incr = 50;
    private int upgr3Incr2 = 50;

    Planet planetComponent;
    GameManager gameManager;


    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        InvokeRepeating(nameof(GetCurrentPlanetStats), 0.2f, 1f);
    }

    // Update is called once per frame
    void Update()
    {

        if(planetComponent != null)
        {
            upgrade1.SetText($"Price : {planetComponent.planetUpgrades[0]} iron, {upgr1Incr} copper.");
            upgrade2.SetText($"Price : {planetComponent.planetUpgrades[1]} copper, {upgr2Incr} iron.");
            upgrade3.SetText($"Price : {planetComponent.planetUpgrades[2]} teralium, {upgr3Incr} iron, {upgr3Incr2} copper");

            upgrade1Level.SetText($"Level : {planetComponent.mineral1UpgradeLvl}");
            upgrade2Level.SetText($"Level : {planetComponent.mineral2UpgradeLvl}");
            upgrade3Level.SetText($"Level : {planetComponent.mineral3UpgradeLvl}");
        }

    }

    public void PressUpgrade1()
    {
        if(GameManager.mineral1Value >= planetComponent.planetUpgrades[0] && GameManager.mineral2Value >= upgr1Incr) //Second value has to increment as well.
        {
            planetComponent.mineral1MadePerSecond += 2 + planetComponent.mineral1UpgradeLvl; //This needs to be an incremental value
            GameManager.mineral1Value -= planetComponent.planetUpgrades[0];

            GameManager.mineral2Value -= upgr1Incr;

            upgr1Incr += 20;

            planetComponent.planetUpgrades[0] = planetComponent.planetUpgrades[0] + (int)Mathf.Ceil(planetComponent.mineral1UpgradeLvl / 2);
            planetComponent.mineral1UpgradeLvl++;
        } 
    }

    public void PressUpgrade2()
    {
        if (GameManager.mineral2Value >= planetComponent.planetUpgrades[1] && GameManager.mineral1Value >= upgr2Incr)
        {
            planetComponent.mineral2MadePerSecond += 2 + planetComponent.mineral2UpgradeLvl; //This needs to be an incremental value
            GameManager.mineral2Value -= planetComponent.planetUpgrades[1];

            GameManager.mineral1Value -= upgr2Incr;
            upgr2Incr += 20;

            planetComponent.planetUpgrades[1] = planetComponent.planetUpgrades[1] + (int)Mathf.Ceil(planetComponent.mineral2UpgradeLvl / 2);
            planetComponent.mineral2UpgradeLvl++;
        }
    }

    public void PressUpgrade3()
    {
        if (GameManager.mineral3Value >= planetComponent.planetUpgrades[2] && GameManager.mineral1Value >= upgr3Incr && GameManager.mineral2Value >= upgr3Incr2)
        {
            planetComponent.mineral3MadePerSecond += 2 + planetComponent.mineral2UpgradeLvl; //This needs to be an incremental value
            GameManager.mineral3Value -= planetComponent.planetUpgrades[2];

            GameManager.mineral1Value -= upgr3Incr;
            GameManager.mineral2Value -= upgr3Incr2;

            upgr3Incr += 20;
            upgr3Incr2 += 20;

            planetComponent.planetUpgrades[2] = planetComponent.planetUpgrades[2] + (int)Mathf.Ceil(planetComponent.mineral2UpgradeLvl / 2);
            planetComponent.mineral3UpgradeLvl++;
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

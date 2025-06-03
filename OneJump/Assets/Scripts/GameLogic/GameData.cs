using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData
{
    //Only one instance
    //In game manager
    public int[] resources;
    public int currentSelectedPlanetIndex;

    //Varies per planet
    //In planet script
    public int[] planetUpgradeLevels;
    public bool isItUnlocked;
    public float[] planetPosition;
    public float[] planetRotation;


    public GameData(Planet[] planet, GameManager gameManager)
    {
        #region Game Manager things
        resources = new int[3];
        resources[0] = gameManager.mineral1Amount;
        resources[1] = gameManager.mineral2Amount;
        resources[2] = gameManager.mineral3Amount;

        currentSelectedPlanetIndex = GameManager.currentPlanetIndex;
        #endregion

        #region Planet data things
        for (int i =0; i< gameManager.planetObjects.Length - 1; i++)
        {
            planetUpgradeLevels = new int[3];
            planetUpgradeLevels[0] = planet[i].mineral1UpgradeLvl;
            planetUpgradeLevels[1] = planet[i].mineral2UpgradeLvl;
            planetUpgradeLevels[2] = planet[i].mineral3UpgradeLvl;

            isItUnlocked = planet[i].isPlanetUnlocked;

            planetPosition = new float[3];
            planetPosition[0] = planet[i].transform.position.x;
            planetPosition[1] = planet[i].transform.position.y;
            planetPosition[2] = planet[i].transform.position.z;

            planetRotation = new float[1];
            planetRotation[0] = planet[i].transform.rotation.y;

        }

        #endregion
    }

}

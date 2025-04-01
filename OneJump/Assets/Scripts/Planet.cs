using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

public class Planet : MonoBehaviour
{
    [Header("Game Manager Things")]
    [SerializeField] static protected GameObject gameManager;

    [Header("Planet Features")]
    [SerializeField] private float realPlanetSpinSpeed = 0f;
    [SerializeField] private float planetSpinSpeed = 0f;
    [SerializeField] private int planetResearchValue = 0;
    private int moneyMadePerSecond = 0;

    [SerializeField] private bool isRealRotationActive = false;

    [Header("Planet Upgrades Values")]
    [SerializeField] float[] planetUpgrades = new float[4];



    #region Planet Basic Values
    //I don't know if this section is even worth existing yet, but we figure it out.
    protected float PlanetSpeed
    {
        get { return planetSpinSpeed; }
        set { planetSpinSpeed = value; }
    }
    protected float RealPlanetSpeed
    {
        get { return realPlanetSpinSpeed; }
        set { realPlanetSpinSpeed = value; }
    }
    protected bool RealRotationStatus
    {
        get { return isRealRotationActive; }
        set { isRealRotationActive = value; }
    }
    protected int PlanetValue
    {
        get { return planetResearchValue; }
        set { planetResearchValue = value; }
    }

    protected int MoneyMadePerSecond
    {
        get { return moneyMadePerSecond; }
        set { moneyMadePerSecond = value; }
    }
    #endregion

    //Set the upgrade value of that index as the requested value.
    protected float SetPlanetUpgradeValue( int planetUpgradeIndex, float planetUpgradeValue)
    {
        planetUpgrades[planetUpgradeIndex] = planetUpgradeValue;
        return planetUpgrades[planetUpgradeIndex];
    }
    //Return what the upgrade value of that index is.
    protected float GetPlanetUpgradeValue( int planetUpgradeIndex)
    {
        return planetUpgrades[planetUpgradeIndex];
    }

    protected void SpinThePlanet(Rigidbody planetRb)
    {
        if (Input.GetKeyDown(KeyCode.V) && RealRotationStatus == false)
        {
            RealRotationStatus = true;
        }
        else if (Input.GetKeyDown(KeyCode.V) && RealRotationStatus == true)
        {
            RealRotationStatus = false;
        }
        if (RealRotationStatus)
        {
            planetRb.transform.Rotate(0, RealPlanetSpeed * Time.deltaTime, 0);
        }
        else
        {
            planetRb.transform.Rotate(0, PlanetSpeed * Time.deltaTime, 0);
        }
    }


}
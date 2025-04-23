using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

public class Planet : MonoBehaviour
{
    [Header("Game Manager Things")]
    protected GameManager gameManagerScript;

    [Header("Planet Features")]
    public string planetName;
    [SerializeField] public bool isPlanetUnlocked = false;

    [SerializeField] private float realPlanetSpinSpeed = 0f;
    [SerializeField] private float planetSpinSpeed = 0f;
    [SerializeField] public int planetResearchValue = 0;
    public int mineral1MadePerSecond = 0;
    public int mineral2MadePerSecond = 0;
    public int mineral3MadePerSecond = 0;

    private Rigidbody planetRigidBody;

    static public bool isRealRotationActive = false;

    [Header("Planet Upgrades Values")]
    public int[] planetUpgradePrice = new int[3];
    public int mineral1UpgradeLvl = 1;
    public int mineral2UpgradeLvl = 1;
    public int mineral3UpgradeLvl = 1;


    private void Awake()
    {
        planetRigidBody = this.GetComponent<Rigidbody>();
        gameManagerScript = FindObjectOfType<GameManager>(true);

        gameManagerScript.gatherResources += GatherResources; //Subscribing to function
    }

    private void Update()
    {
        SpinThePlanet(planetRigidBody);


    }

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

    //protected int MoneyMadePerSecond
    //{
    //    get { return moneyMadePerSecond; }
    //    set { moneyMadePerSecond = value; }
    //}

    protected float SetPlanetUpgradeValue(int planetUpgradeIndex, int planetUpgradeValue)
    {
        planetUpgradePrice[planetUpgradeIndex] = planetUpgradeValue;
        return planetUpgradePrice[planetUpgradeIndex];
    }
    //Return what the upgrade value of that index is.
    protected float GetPlanetUpgradeValue(int planetUpgradeIndex)
    {
        return planetUpgradePrice[planetUpgradeIndex];
    }


    #endregion

    protected void SpinThePlanet(Rigidbody planetRb) //Takes rigidbody and spins planet
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

    protected void GatherResources()
    {
        if (!isPlanetUnlocked) return;

        gameManagerScript.AddMoneyPerSecond(mineral1MadePerSecond, mineral2MadePerSecond, mineral3MadePerSecond);
    }
}
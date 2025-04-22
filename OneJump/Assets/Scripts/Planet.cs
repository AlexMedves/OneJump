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
    [SerializeField] private bool isPlanetUnlocked = false;
    [SerializeField] private float realPlanetSpinSpeed = 0f;
    [SerializeField] private float planetSpinSpeed = 0f;
    [SerializeField] private int planetResearchValue = 0;
    public int mineral1MadePerSecond = 0;
    public int mineral2MadePerSecond = 0;
    public int mineral3MadePerSecond = 0;



    private float gameTimer = 0f;
    private readonly float gameTimerDelay = 1f;


    private Rigidbody planetRigidBody;

    [SerializeField] static private bool isRealRotationActive = false;

    [Header("Planet Upgrades Values")]
    public int[] planetUpgradePrice = new int[4];
    public int mineral1UpgradeLvl = 1;
    public int mineral2UpgradeLvl = 1;
    public int mineral3UpgradeLvl = 1;


    private void Awake()
    {
        planetRigidBody = this.GetComponent<Rigidbody>();
        gameManagerScript = FindObjectOfType<GameManager>(true);
    }

    private void Update()
    {
        gameTimer += Time.deltaTime;

        if (isPlanetUnlocked) //This might not be needed, as locked planets won't have any value of money to give you anyway.
        {
            if (gameTimer > gameTimerDelay)
            {
                gameTimer = 0f;
                gameManagerScript.AddMoneyPerSecond(mineral1MadePerSecond, mineral2MadePerSecond, mineral3MadePerSecond);

            }
        }
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


}
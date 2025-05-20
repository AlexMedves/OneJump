using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

[System.Serializable]
public class PlanetSaveData
{
    public string planetName;
    public bool isPlanetUnlocked;//

    public int mineral1MadePerSecond;//
    public int mineral2MadePerSecond;//
    public int mineral3MadePerSecond;//

    public int[] planetUpgradePrice;//
    public int mineral1UpgradeLvl;//
    public int mineral2UpgradeLvl;//
    public int mineral3UpgradeLvl;//
}


public class Planet : MonoBehaviour
{
    [Header("Game Manager Things")]
    protected GameManager gameManagerScript;

    [Header("Planet Features")]
    public string planetName;
    [SerializeField] public bool isPlanetUnlocked = false;//

    [SerializeField] private float realPlanetSpinSpeed = 0f;
    [SerializeField] private float planetSpinSpeed = 0f;
    [SerializeField] public int planetResearchValue = 0;
    public int mineral1MadePerSecond = 0;//
    public int mineral2MadePerSecond = 0;//
    public int mineral3MadePerSecond = 0;//

    private Rigidbody planetRigidBody;

    static public bool isRealRotationActive = false;

    [Header("Planet Upgrades Values")]
    public int[] planetUpgradePrice = new int[3];//
    public int mineral1UpgradeLvl = 1;//
    public int mineral2UpgradeLvl = 1;//
    public int mineral3UpgradeLvl = 1;//
    

    private void Awake()
    {
        planetRigidBody = this.GetComponent<Rigidbody>();
        gameManagerScript = FindObjectOfType<GameManager>(true);

        gameManagerScript.gatherResources += GatherResources; //Subscribing to function

        PickTheName();
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

    protected void PickTheName()
    {
        var prefixList = new List<string> {
            "Tren", "Vart", "Luk", "Pot", "Jhin", "Gig", "Gag", "Uda", "Ari", "Zad", "Zag", "Mau", "Sama", "Ongo", "Muxi", "Llaya", "Gia", "Callu", "Tung"
        };
        var suffixList = new List<string> { 
            "altur", "car", "not", "leo", "ban", "gof", "lea", "phus", "rah", "nov", "rro", "nus", "des", "tung", "iri", "potamus", "gawa", "ter", "nana", "shan"  
        };

        int randomPrefix = Random.Range(0, prefixList.Count);
        int randomSuffix = Random.Range(0, suffixList.Count);

        int cogPlanet = Random.Range(0, 80000);

        if (planetName == "")
        {
            if (cogPlanet == 67389)
            {
                planetName = "Cozy Orangutan Planet";
            }
            else
            {
                planetName = prefixList[randomPrefix] + suffixList[randomSuffix];
                prefixList.RemoveAt(randomPrefix);//Maybe not needed, but it will stop repetition in names. Probably won't work if the name list is not hard coded.
            }
            //Debug.Log($"Object Name: {this.name}, Number: {randomPrefix}, Prefix: {prefixList[randomPrefix]}; Number: {randomSuffix}, Suffix: {suffixList[randomSuffix]}");
        }
    }

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


    public PlanetSaveData SaveState()
    {
        return new PlanetSaveData()
        {
            planetName = this.planetName,
            isPlanetUnlocked = this.isPlanetUnlocked,
            mineral1MadePerSecond = this.mineral1MadePerSecond,
            mineral2MadePerSecond = this.mineral2MadePerSecond,
            mineral3MadePerSecond = this.mineral3MadePerSecond,

            mineral1UpgradeLvl = this.mineral1UpgradeLvl,
            mineral2UpgradeLvl = this.mineral2UpgradeLvl,
            mineral3UpgradeLvl = this.mineral3UpgradeLvl,

            planetUpgradePrice = this.planetUpgradePrice,
        };
    }

    public void LoadPlanetData(PlanetSaveData data)
    {
        planetName = data.planetName;
        isPlanetUnlocked = data.isPlanetUnlocked;
        mineral1MadePerSecond = data.mineral1MadePerSecond;
        mineral2MadePerSecond = data.mineral2MadePerSecond;
        mineral3MadePerSecond = data.mineral3MadePerSecond;
        
        mineral1UpgradeLvl = data.mineral1UpgradeLvl;
        mineral2UpgradeLvl = data.mineral2UpgradeLvl;
        mineral3UpgradeLvl = data.mineral3UpgradeLvl;

        planetUpgradePrice = data.planetUpgradePrice;
    }
}
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


    public int[] planetUpgradePrice;//
    public int mineral1UpgradeLvl;//
    public int mineral2UpgradeLvl;//
    public int mineral3UpgradeLvl;//


    //Increment values for the upgrades. After purchasing said upgrade, value goes up.
    public int upgr1Incr;
    public int upgr2Incr; //These might need to be changed.
    public int upgr3Incr;
    public int upgr3Incr2;
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

    private Rigidbody planetRigidBody;

    static public bool isRealRotationActive = false;

    [Header("Planet Upgrades Values")]
    public int[] planetUpgradePrice = new int[3];//
    public int mineral1UpgradeLvl;//
    public int mineral2UpgradeLvl;//
    public int mineral3UpgradeLvl;//

    //Increment values for the upgrades. After purchasing said upgrade, value goes up.
    public int upgr1Incr;
    public int upgr2Incr; //These might need to be changed.
    public int upgr3Incr;
    public int upgr3Incr2;


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
        if (Input.GetKeyDown(KeyCode.V) && isRealRotationActive == false)
        {
            isRealRotationActive = true;
        }
        else if (Input.GetKeyDown(KeyCode.V) && isRealRotationActive == true)
        {
            isRealRotationActive = false;
        }

        if (isRealRotationActive)
        {
            planetRb.transform.Rotate(0, realPlanetSpinSpeed * Time.deltaTime, 0);
        }
        else
        {
            planetRb.transform.Rotate(0, planetSpinSpeed * Time.deltaTime, 0);
        }
    }

    protected void GatherResources()
    {
        if (!isPlanetUnlocked) return;

        gameManagerScript.AddMoneyPerSecond(mineral1UpgradeLvl, mineral2UpgradeLvl, mineral3UpgradeLvl);
    }


    public PlanetSaveData SaveState()
    {
        return new PlanetSaveData()
        {
            planetName = this.planetName,
            isPlanetUnlocked = this.isPlanetUnlocked,

            mineral1UpgradeLvl = this.mineral1UpgradeLvl,
            mineral2UpgradeLvl = this.mineral2UpgradeLvl,
            mineral3UpgradeLvl = this.mineral3UpgradeLvl,

            planetUpgradePrice = this.planetUpgradePrice,

            upgr1Incr = this.upgr1Incr,
            upgr2Incr = this.upgr2Incr,
            upgr3Incr = this.upgr3Incr,
            upgr3Incr2 = this.upgr3Incr2,
        };
    }

    public void LoadPlanetData(PlanetSaveData data)
    {
        planetName = data.planetName;
        isPlanetUnlocked = data.isPlanetUnlocked;

        mineral1UpgradeLvl = data.mineral1UpgradeLvl;
        mineral2UpgradeLvl = data.mineral2UpgradeLvl;
        mineral3UpgradeLvl = data.mineral3UpgradeLvl;

        if (mineral1UpgradeLvl > 50)
        {
            mineral1UpgradeLvl = 50;
        }
        if (mineral2UpgradeLvl > 50)
        {
            mineral2UpgradeLvl = 50;
        }
        if (mineral3UpgradeLvl > 50)
        {
            mineral3UpgradeLvl = 50;
        }

        planetUpgradePrice = data.planetUpgradePrice;

        upgr1Incr = data.upgr1Incr;
        upgr2Incr = data.upgr2Incr;
        upgr3Incr = data.upgr3Incr;
        upgr3Incr2 = data.upgr3Incr2;
    }
}
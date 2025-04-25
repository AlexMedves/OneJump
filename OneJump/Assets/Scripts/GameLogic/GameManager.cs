using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{

     public int mineral1Amount = 0;
     public int mineral2Amount = 0;
     public int mineral3Amount = 0;

    //static private int actualMoney = moneyValue;

    static public float gameTimer = 0f;
    private readonly float gameTimerDelay = 1f;

    static private float saveTimer = 0f;
    private float saveTimerDelay = 300f;

    public Action saveGame;
    public Action gatherResources;


    [Header("General Object References")]
    [SerializeField] TMP_Text mineral1Text;
    [SerializeField] TMP_Text mineral2Text;
    [SerializeField] TMP_Text mineral3Text;
    private int selectedMaterial = 0;

    [Header("Planet Logic Stuff")]
    [SerializeField] static public int currentPlanetIndex = 0;
    public GameObject[] planetObject = new GameObject[8];

    [SerializeField] private GameObject scanObject;
     static public GameObject currentSelectedPlanet;

    private void Awake()
    {
        saveGame += SaveTheGame;
    }

    private void Update()
    {
        mineral1Text.SetText("Iron amount: " + mineral1Amount);
        mineral2Text.SetText("Copper amount: " + mineral2Amount); //Set the texts value so that they update constantly.
        mineral3Text.SetText("Teralium amount: " + mineral3Amount);

        //gameTimer += Time.deltaTime;
        StartHitScanningForPlanet(); //Can be put down as invokeRepeating if causing issues
        ChangeMaterials();

        gameTimer += Time.deltaTime;
        saveTimer += Time.deltaTime;
        if (gameTimer > gameTimerDelay)
        {
            gameTimer = 0f;
            gatherResources.Invoke(); //Invoking Actions

        }

        if(saveTimer > saveTimerDelay)
        {
            saveTimer = 0f;
            saveGame.Invoke();
        }
    }

    #region Button things here
    public void AddClickMoney()
    {
        switch (selectedMaterial)
        {
            default: mineral1Amount++;
                break;

            case 0: mineral1Amount++;
                break;
            case 1: mineral2Amount++; 
                break;
            case 2: mineral3Amount++;
                break;
        }
    }

    public void MoveLeft()
    {
        //Slide to the left
         currentPlanetIndex--;
    }
    public void MoveRight()
    {
        //Slide to the right
        currentPlanetIndex++;
    }
    #endregion

    public void AddMoneyPerSecond(int plusMineral1, int plusMineral2, int plusMineral3) //This won't stay the same for long as I now have multiple materials. I think I have an idea of how to make it interchangeable
        //int how much per second, and also take what mineral to add to. Add a switch case that adds to said mineralAmount.
    {

        mineral1Amount += plusMineral1;
        mineral2Amount += plusMineral2;
        mineral3Amount += plusMineral3;

    }

    private void ChangeMaterials()
    {
        if(Input.GetKeyDown(KeyCode.Comma) && selectedMaterial == 0) //If willing to switch materials, but are on last material, switch to the last material.
        {
            selectedMaterial = 2;
        }
        else if(Input.GetKeyDown(KeyCode.Comma))//Otherwise, just switch down one.
        {
            selectedMaterial--;
        }

        if(Input.GetKeyDown(KeyCode.Period) && selectedMaterial == 2)//If willing to switch materials, but are on last material, switch to the first material.
        {
            selectedMaterial = 0;
        }
        else if(Input.GetKeyDown(KeyCode.Period)) //Otherwise, just switch up one.
        {
            selectedMaterial++;
        }
    }

    private void StartHitScanningForPlanet()
    {
        if (Physics.Raycast(scanObject.transform.position, Vector3.forward, out RaycastHit hitPlanet, 200f)) //shoots a ray that gets the component we need.
        {
            currentSelectedPlanet = hitPlanet.collider.gameObject;  //Potential performance loss here.
        }
    }

    public void SaveTheGame()
    {
        SaveSystem.SaveData(FindObjectsOfType<Planet>(), this);
        Debug.Log("saved the game");
    }

    public void LoadTheGame()
    {
        GameData data = SaveSystem.LoadData();

        mineral1Amount = data.resources[0];
        mineral2Amount = data.resources[1];
        mineral3Amount = data.resources[2];

        currentPlanetIndex = data.currentSelectedPlanetIndex;

        

    }

//    protected bool CanAfford(int priceMineral1, int priceMineral2, int priceMineral3)
//    {

//    }
}

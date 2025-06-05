using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool debugMode = true;
    public Action saveDebugData;


    private SaveSystem saveSystem;
    static public bool IsInTutorial {  get; set; }

    [SerializeField] public GameObject mineralButton1;
    [SerializeField] public GameObject mineralButton2;
    [SerializeField] public GameObject mineralButton3;

    public int mineral1Amount = 0;
    public int mineral2Amount = 0;
    public int mineral3Amount = 0;

    public int min1Click = 0;
    public int min2Click = 0;
    public int min3Click = 0;

    //static private int actualMoney = moneyValue;

    static public float gameTimer = 0f;
    private readonly float gameTimerDelay = 1f;

    private float saveTimer = 0f;
    private readonly float saveTimerDelay = 120f;

    public Action saveGame;
    public Action gatherResources;


    [Header("General Object References")]
    [SerializeField] public TMP_Text mineral1Text;
    [SerializeField] public TMP_Text mineral2Text;
    [SerializeField] public TMP_Text mineral3Text;
    public int selectedMaterial = 0;

    [SerializeField] public TMP_Text relumText;
    [SerializeField] public TMP_Text kupruText;
    [SerializeField] public TMP_Text trevleockText;

    [SerializeField] private GameObject buttonLeft;
    [SerializeField] private GameObject buttonRight;
    public static bool automatedLeftRightButtons;


    [Header("Planet Logic Stuff")]
    [SerializeField] static public int currentPlanetIndex = 0;
    public GameObject[] planetObjects = new GameObject[8];

    [SerializeField] private GameObject scanObject;
     static public GameObject currentSelectedPlanet;

    private void Awake()
    {
        saveGame += SaveTheGame;
        saveSystem = this.GetComponent<SaveSystem>();
        StartHitScanningForPlanet();


        if (SceneManager.GetActiveScene().name == "MainScene")
        {
            IsInTutorial = false;
            automatedLeftRightButtons = true;
            saveSystem.LoadData("SaveData.oj"); //Change this back.
        }
    }

    private void Update()
    {
        mineral1Text.SetText($"{mineral1Amount}");
        mineral2Text.SetText($"{mineral2Amount}"); //Set the texts value so that they update constantly.
        mineral3Text.SetText($"{mineral3Amount}");

        //gameTimer += Time.deltaTime;
        StartHitScanningForPlanet(); //Can be put down as invokeRepeating if causing issues
        ChangeMaterials();
        ShowLeftRightButtons();

        gameTimer += Time.deltaTime;
        saveTimer += Time.deltaTime;
        if (gameTimer > gameTimerDelay)
        {
            gameTimer = 0f;
            gatherResources.Invoke(); //Invoking Actions
            saveDebugData.Invoke();

        }

        ChangeTextColor();
        //SaveTheGame();
        //LoadTheGame();

        if (saveTimer > saveTimerDelay)
        {
            Debug.Log("Tried to Save");
            saveTimer = 0f;
            if (SceneManager.GetActiveScene().name != "TutorialScene")
            {
                saveGame();
            }
        }
    }

    #region Button things here
    public void AddClickMoney()
    {
        switch (selectedMaterial)
        {
            default: mineral1Amount++;
                min1Click++;
                break;

            case 0: mineral1Amount++;
                min1Click++;
                break;
            case 1: mineral2Amount++;
                min2Click++;
                break;
            case 2: mineral3Amount++;
                min3Click++;
                break;
        }
    }

    private void ShowLeftRightButtons()
    {
        if (automatedLeftRightButtons)
        {
            if (currentPlanetIndex == 0)
            {
                buttonRight.SetActive(true);
                buttonLeft.SetActive(false);
            }
            if (currentPlanetIndex == planetObjects.Length - 1)
            {
                buttonLeft.SetActive(true);
                buttonRight.SetActive(false);
            }
            else if (currentPlanetIndex > 0 && currentPlanetIndex < planetObjects.Length -1)
            {
                buttonLeft.SetActive(true);
                buttonRight.SetActive(true);
            }
        }
        else
        {
            buttonLeft.SetActive(false);
            buttonRight.SetActive(false);
        }
    }

    private void ChangeTextColor()
    {
        if (!IsInTutorial)
        {
            switch (selectedMaterial)
            {
                default: mineral1Text.color = Color.white; mineral2Text.color = Color.white; mineral3Text.color = Color.white; break;
                case 0:
                    mineral1Text.color = new Color(0.3243f, 1, 0.8546f);
                    mineral2Text.color = Color.white;
                    mineral3Text.color = Color.white;

                    relumText.color = new Color(0.3243f, 1, 0.8546f);
                    kupruText.color = Color.white;
                    trevleockText.color = Color.white;
                    break;
                case 1:
                    mineral2Text.color = new Color(0.3243f, 1, 0.8546f);
                    mineral1Text.color = Color.white;
                    mineral3Text.color = Color.white;

                    kupruText.color = new Color(0.3243f, 1, 0.8546f);
                    relumText.color = Color.white;
                    trevleockText.color = Color.white;
                    break;
                case 2:
                    mineral3Text.color = new Color(0.3243f, 1, 0.8546f);
                    mineral1Text.color = Color.white;
                    mineral2Text.color = Color.white;

                    trevleockText.color = new Color(0.3243f, 1, 0.8546f);
                    relumText.color = Color.white;
                    kupruText.color = Color.white;
                    break;
            }
        }
    }

    public void ChangeMineral1()
    {
        selectedMaterial = 0;
    }
    public void ChangeMineral2()
    {
        selectedMaterial = 1;
    }
    public void ChangeMineral3()
    {
        selectedMaterial = 2;
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

    public void BackToMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    #endregion

    public void AddMoneyPerSecond(int plusMineral1, int plusMineral2, int plusMineral3) //This won't stay the same for long as I now have multiple materials. I think I have an idea of how to make it interchangeable
        //int how much per second, and also take what mineral to add to. Add a switch case that adds to said mineralAmount.
    {

        mineral1Amount += plusMineral1;
        mineral2Amount += plusMineral2;
        mineral3Amount += plusMineral3;

    }

    private void ChangeMaterials() //Computer Functionality.
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
        saveSystem.SaveData("SaveData.oj");  //Change this back
        Debug.Log("Saved the game");
    }

    public void LoadTheGame()
    {
        saveSystem.LoadData("SaveData.oj"); //Change this back.
        Debug.Log("Loaded the game");
    }

//    protected bool CanAfford(int priceMineral1, int priceMineral2, int priceMineral3)
//    {

//    }
}

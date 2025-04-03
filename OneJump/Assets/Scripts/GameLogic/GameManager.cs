using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static protected int moneyValue = 0;

    //static private float gameTimer = 0;
    //static private float gameTimerDelay = 1f;

    [Header("General Object References")]
    [SerializeField] TMP_Text moneyText;

    [Header("Planet Logic Stuff")]
    [SerializeField] static public int currentPlanetIndex = 0;

    [SerializeField] private GameObject scanObject;
    [SerializeField] private GameObject currentSelectedPlanet;


    private void Update()
    {
        moneyText.SetText("Money value: " + moneyValue);
        //gameTimer += Time.deltaTime;

        if (Physics.Raycast(scanObject.transform.position, Vector3.forward, out RaycastHit hitPlanet, 50f))
        {
            currentSelectedPlanet = hitPlanet.collider.gameObject;

            Debug.Log(currentSelectedPlanet.GetComponent<Planet>().planetUpgrades[0]);
        }

    }

    #region Button things here
    public void AddClickMoney()
    {
        moneyValue++;
    }

    public void MoveLeft()
    {
        //Slide to the left
         currentPlanetIndex--;
        //TellMeWhichPlanet();
    }
    public void MoveRight()
    {
        //Slide to the right
        currentPlanetIndex++;
        //TellMeWhichPlanet();
    }
    #endregion

    public void AddMoneyPerSecond(int howMuchPerSecond)
    {

        moneyValue += howMuchPerSecond;

    }

    private void TellMeWhichPlanet()
    {
        switch (currentPlanetIndex)
        {
            default:
                print("Currently in our solar system");
                break;
            case 0:
                Debug.Log("Currently on Mercury"); //Cooked.
                break;
            case 1:
                Debug.Log("Currently on Venus");
                break;
            case 2:
                Debug.Log("Currently on Earth");
                break;
            case 3:
                Debug.Log("Currently on Mars");
                break;
            case 4:
                Debug.Log("Currently on Jupiter");
                break;
            case 5:
                Debug.Log("Currently on Saturn");
                break;
            case 6:
                Debug.Log("Currently on Uranus");
                break;
            case 7:
                Debug.Log("Currently on Neptune");
                break;
        }
    }

}

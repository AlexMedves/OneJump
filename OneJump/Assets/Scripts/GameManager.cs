using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    protected int moneyValue = 0;

    private Vector3 currentCameraPosition;
    private Vector3 newCameraPosition;

    [Header("General Object References")]
    [SerializeField] private Camera cameraObj;
    [SerializeField] TMP_Text moneyText;

    [Header("Planet Logic Stuff")]
    [SerializeField] protected GameObject[] planetIndex = new GameObject[8];
    [SerializeField] protected int currentPlanetIndex = 0;

    private void Update()
    {
        moneyText.SetText("Money value: " + moneyValue);

        PlanetIndexLogic();
        Debug.Log("New camera position is: " + newCameraPosition);

        //cameraObj.transform.position = newCameraPosition;
        cameraObj.transform.position = Vector3.Lerp(cameraObj.transform.position, newCameraPosition, 5f * Time.deltaTime);

    }


    public void AddMoney()
    {
        moneyValue++;
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

    private void PlanetIndexLogic()
    {
        //Criss cross

        //Stop the planetIndex values between 0 and 8.
        currentPlanetIndex = Mathf.Clamp(currentPlanetIndex, 0, planetIndex.Length - 1);
        //Set the array index to the currentPlanetIndex value.
        planetIndex[currentPlanetIndex] = planetIndex[currentPlanetIndex];

        //This is the bit that handles the new camera position, it is updated constantly. For some reason when put in MoveLeft or MoveRight it doesn't work right.
        if (planetIndex[currentPlanetIndex] != null)
        {
            newCameraPosition = planetIndex[currentPlanetIndex].transform.position;
        }
    }

}

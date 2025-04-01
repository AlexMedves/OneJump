using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehavior : MonoBehaviour
{
    [SerializeField] private Camera cameraObj;
    [SerializeField] private GameObject gameManagerReference;
    [SerializeField] protected GameObject[] planetIndex = new GameObject[8];

    private Vector3 newCameraPosition;


    private void Awake()
    {
        cameraObj = GetComponent<Camera>();
    }

    private void Update()
    {
        PlanetPositionLogic();
    }


    private void PlanetPositionLogic()
    {
        //Stop the planetIndex values between 0 and 8.
        GameManager.currentPlanetIndex = Mathf.Clamp(GameManager.currentPlanetIndex, 0, planetIndex.Length - 1);
        //Set the array index to the currentPlanetIndex value.
        planetIndex[GameManager.currentPlanetIndex] = planetIndex[GameManager.currentPlanetIndex];

        //This is the bit that handles the new camera position, it is updated constantly. For some reason when put in MoveLeft or MoveRight it doesn't work right.
        if (planetIndex[GameManager.currentPlanetIndex] != null)
        {
            newCameraPosition = planetIndex[GameManager.currentPlanetIndex].transform.position;
        }

        cameraObj.transform.position = Vector3.Lerp(cameraObj.transform.position, newCameraPosition, 5f * Time.deltaTime);
    }
}

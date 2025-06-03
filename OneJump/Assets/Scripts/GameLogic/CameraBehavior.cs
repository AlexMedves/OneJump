using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehavior : MonoBehaviour
{
    [SerializeField] private Camera cameraObj;
    private GameManager gameManagerScript;

    [SerializeField] private float cameraSpeed = 0f;

    private Vector3 newCameraPosition;


    private void Awake()
    {
        cameraObj = GetComponent<Camera>();
        gameManagerScript = FindObjectOfType<GameManager>();
        
    }

    private void Update()
    {
        CameraPositionLogic();
    }


    private void CameraPositionLogic()
    {
        //Stop the planetIndex values between 0 and 8.
        GameManager.currentPlanetIndex = Mathf.Clamp(GameManager.currentPlanetIndex, 0, gameManagerScript.planetObjects.Length - 1);
        //Set the array index to the currentPlanetIndex value.
        gameManagerScript.planetObjects[GameManager.currentPlanetIndex] = gameManagerScript.planetObjects[GameManager.currentPlanetIndex];
        

        //This is the bit that handles the new camera position, it is updated constantly.
        if (gameManagerScript.planetObjects[GameManager.currentPlanetIndex] != null)
        {
            newCameraPosition = gameManagerScript.planetObjects[GameManager.currentPlanetIndex].transform.position;
        }

        cameraObj.transform.position = Vector3.Lerp(cameraObj.transform.position, newCameraPosition, cameraSpeed * Time.deltaTime);
    }
}

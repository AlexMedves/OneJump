using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehavior : MonoBehaviour
{
    [SerializeField] private Camera cameraObj;
    [SerializeField] private GameObject gameManagerReference;
    private GameManager gameManagerScript;


    private void Awake()
    {
        cameraObj = GetComponent<Camera>();
        gameManagerScript = gameManagerReference.GetComponent<GameManager>();
    }

    private void Update()
    {
        
    }
}

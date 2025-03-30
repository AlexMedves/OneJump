using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mercury : Planet
{
    Rigidbody mercury_Rb;


    private void Awake()
    {
        mercury_Rb = GetComponent<Rigidbody>();

        RealPlanetSpeed = 0.018f;
        PlanetSpeed = 25f;


        SetPlanetUpgradeValue(0, 250);
        SetPlanetUpgradeValue(1, 250);
        SetPlanetUpgradeValue(2, 500);
        SetPlanetUpgradeValue(3, 750);


    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.V) && RealRotationStatus == false)
        {
            RealRotationStatus = true;
        }
        else if(Input.GetKeyDown(KeyCode.V) && RealRotationStatus == true)
        {
            RealRotationStatus = false;
        }
        if(RealRotationStatus)
        {
            mercury_Rb.transform.Rotate(0, RealPlanetSpeed * Time.deltaTime, 0);
        }
        else
        {
            mercury_Rb.transform.Rotate(0, PlanetSpeed * Time.deltaTime, 0);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saturn : Planet
{
    Rigidbody saturn_Rb;
    // Start is called before the first frame update
    void Awake()
    {
        saturn_Rb = GetComponent<Rigidbody>();

        RealPlanetSpeed = 36.84f;
        PlanetSpeed = 22f;

        PlanetValue = 10000;


        SetPlanetUpgradeValue(0, 500);
        SetPlanetUpgradeValue(1, 500);
        SetPlanetUpgradeValue(2, 1000);
        SetPlanetUpgradeValue(3, 1300);
    }

    // Update is called once per frame
    void Update()
    {
        SpinThePlanet(saturn_Rb);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Earth : Planet
{
    Rigidbody earth_Rb;
    // Start is called before the first frame update
    void Awake()
    {
        earth_Rb = GetComponent<Rigidbody>();

        RealPlanetSpeed = 0.1574f;
        PlanetSpeed = 18f;

        PlanetValue = 100000;


        SetPlanetUpgradeValue(0, 1000);
        SetPlanetUpgradeValue(1, 1000);
        SetPlanetUpgradeValue(2, 2000);
        SetPlanetUpgradeValue(3, 3000);
    }

    // Update is called once per frame
    void Update()
    {
        SpinThePlanet(earth_Rb);
    }
}

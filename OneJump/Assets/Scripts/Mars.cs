using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mars : Planet
{
    Rigidbody mars_Rb;
    // Start is called before the first frame update
    void Awake()
    {
        mars_Rb = GetComponent<Rigidbody>();

        RealPlanetSpeed = 0.086f;
        PlanetSpeed = 13f;

        PlanetValue = 1000000;


        SetPlanetUpgradeValue(0, 2000);
        SetPlanetUpgradeValue(1, 2000);
        SetPlanetUpgradeValue(2, 4000);
        SetPlanetUpgradeValue(3, 5000);
    }

    // Update is called once per frame
    void Update()
    {
        SpinThePlanet(mars_Rb);
    }
}

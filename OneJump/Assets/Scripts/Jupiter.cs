using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jupiter : Planet
{
    Rigidbody jupiter_Rb;
    // Start is called before the first frame update
    void Awake()
    {
        jupiter_Rb = GetComponent<Rigidbody>();

        RealPlanetSpeed = 45.6f;
        PlanetSpeed = 28f;

        PlanetValue = 10000;


        SetPlanetUpgradeValue(0, 500);
        SetPlanetUpgradeValue(1, 500);
        SetPlanetUpgradeValue(2, 1000);
        SetPlanetUpgradeValue(3, 1300);
    }

    // Update is called once per frame
    void Update()
    {
        SpinThePlanet(jupiter_Rb);
    }
}

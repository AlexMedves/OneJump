using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Neptune : Planet
{
    Rigidbody neptune_Rb;
    // Start is called before the first frame update
    void Awake()
    {
        neptune_Rb = GetComponent<Rigidbody>();

        RealPlanetSpeed = 9.7f;
        PlanetSpeed = 15f;

        PlanetValue = 10000;


        SetPlanetUpgradeValue(0, 500);
        SetPlanetUpgradeValue(1, 500);
        SetPlanetUpgradeValue(2, 1000);
        SetPlanetUpgradeValue(3, 1300);
    }

    // Update is called once per frame
    void Update()
    {
        SpinThePlanet(neptune_Rb);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Uranus : Planet
{
    Rigidbody uranus_Rb;
    // Start is called before the first frame update
    void Awake()
    {
        uranus_Rb = this.GetComponent<Rigidbody>();

        RealPlanetSpeed = 14.79f;
        PlanetSpeed = 18f;

        PlanetValue = 10000;


        SetPlanetUpgradeValue(0, 500);
        SetPlanetUpgradeValue(1, 500);
        SetPlanetUpgradeValue(2, 1000);
        SetPlanetUpgradeValue(3, 1300);
    }

    // Update is called once per frame
    void Update()
    {
        SpinThePlanet(uranus_Rb);
    }
}

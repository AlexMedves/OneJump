using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mercury : Planet
{
    Rigidbody mercury_Rb;
    private float timer = 0;
    private readonly float timerDelay = 1;


    private void Awake()
    {
        mercury_Rb = GetComponent<Rigidbody>();

        RealPlanetSpeed = 0.00183f;
        PlanetSpeed = 13f;
        PlanetValue = 0;
        MoneyMadePerSecond = 10;



        SetPlanetUpgradeValue(0, 250);
        SetPlanetUpgradeValue(1, 250);
        SetPlanetUpgradeValue(2, 500);
        SetPlanetUpgradeValue(3, 750);


    }

    private void Update()
    {
        SpinThePlanet(mercury_Rb);

        timer += Time.deltaTime;
        if(timer > timerDelay)
        {
            timer = 0;
            GameManager.AddMoneyPerSecond(MoneyMadePerSecond);
        }
    }
}

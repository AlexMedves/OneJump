using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Venus : Planet
{
    Rigidbody venus_Rb;

    private float timer = 0;
    private float timerDelay = 1;
    
    void Awake()
    {
        
        venus_Rb = GetComponent<Rigidbody>();

        RealPlanetSpeed = 0.00652f;
        PlanetSpeed = 9f;

        MoneyMadePerSecond = 20;
        PlanetValue = 10000;


        SetPlanetUpgradeValue(0, 500);
        SetPlanetUpgradeValue(1, 500);
        SetPlanetUpgradeValue(2, 1000);
        SetPlanetUpgradeValue(3, 1300);
    }

    // Update is called once per frame
    void Update()
    {
        SpinThePlanet(venus_Rb);

        timer += Time.deltaTime;
        if (timer > timerDelay)
        {
            timer = 0;
            GameManager.AddMoneyPerSecond(MoneyMadePerSecond);
        }
    }
}

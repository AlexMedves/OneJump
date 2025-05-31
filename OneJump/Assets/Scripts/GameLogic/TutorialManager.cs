using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class TutorialManager : MonoBehaviour
{
    TypeWriter typewriter;

    public GameObject[] tutorialPopUps;
    private int currentPopUpID;

    public string currentString = "Hello and welcome to OneJump!";
    public TMP_Text testbox;

    public float waitTimer = 2f;


    private void Awake()
    {
        typewriter = GetComponentInChildren<TypeWriter>();
        typewriter.textBox = testbox;
    }


    private void Update()
    {

        for(int i = 0; i < tutorialPopUps.Length; i++)
        {
            if(i == currentPopUpID)
            {
                tutorialPopUps[i].SetActive(true);
                
            }
            else
            {
                tutorialPopUps[i].SetActive(false);
            }
        }

        if(currentPopUpID == 0)
        {
        }
    }



}

using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;

public class TypeWriter : MonoBehaviour
{
    TutorialManager tutorialManager;

    public TMP_Text textBox {  get; set; }

    public string currentString;

    public int currentVisibleCharID;
    public Coroutine typeWriterRoutine;

    private WaitForSeconds simpleDelay;
    private WaitForSeconds interPunctuationDelay;

    private WaitForSeconds skipDelay;

    private float charactersPerSecond = 30f;
    private float interPunctuationDelayFloat = 0.5f;

    static public bool isSkipping = false;

    [Min(1)] private int skipSpeedup = 10;
    public string chosenText;

    private void Awake()
    {

        simpleDelay = new WaitForSeconds(1 / charactersPerSecond);
        interPunctuationDelay = new WaitForSeconds(interPunctuationDelayFloat);
        skipDelay = new WaitForSeconds(1 / (charactersPerSecond * skipSpeedup));

        textBox = this.GetComponent<TMP_Text>();
        tutorialManager = FindObjectOfType<TutorialManager>();

        currentString = textBox.text;
    }

    private void Update()
    {
        if (currentVisibleCharID == currentString.Length)
        {
            isSkipping = false;
            
        }
    }


    private void OnEnable()
    {
        SetText(currentString);
    }

    public void SetText(string text) //This is what handles whatever text you want to display.
    {
        if(typeWriterRoutine != null)
        {
            StopCoroutine(typeWriterRoutine);
        }
        textBox.text = text;
        textBox.maxVisibleCharacters = 0;
        currentVisibleCharID = 0;

        typeWriterRoutine = StartCoroutine(Typewriter());  
    }


    private IEnumerator Typewriter()
    {
        TMP_TextInfo textInfo = textBox.textInfo;

        while (currentVisibleCharID < textInfo.characterCount +1)
        {

            char character = textInfo.characterInfo[currentVisibleCharID].character; //Tells us what character we are currently on.
            textBox.maxVisibleCharacters++; //One more character visible.
            //Debug.Log($"Current visible charID: {currentVisibleCharID} :: characterCount: {textInfo.characterCount}");


            if (!isSkipping && character == '?' || character == '.' || character == ',' || character == ':' || character == ';' || character == '!' || character == '-') //Handles how quick or slow
                //the text should appear.
            {
                yield return interPunctuationDelay;
            }
            else if (isSkipping)
            {
                yield return skipDelay;
                //textBox.maxVisibleCharacters = tutorialManager.currentString.Length;
            }
            else
            {
                yield return simpleDelay;
            }
            currentVisibleCharID++; //if taken out you will have an infinite loop.
        }
    }

}

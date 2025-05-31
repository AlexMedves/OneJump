using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TypeWriter : MonoBehaviour
{
    TutorialManager tutorialManager;

    public TMP_Text textBox {  get; set; }


    public int currentVisibleCharID;
    public Coroutine typeWriterRoutine;

    private WaitForSeconds simpleDelay;
    private WaitForSeconds interPunctuationDelay;

    private float charactersPerSecond = 30;
    private float interPunctuationDelayFloat = 0.5f;

    private bool isSkipping = false;
    [Min(1)] private int skipSpeedup = 5;
    public string chosenText;

    private void Awake()
    {
        simpleDelay = new WaitForSeconds(1 / charactersPerSecond);
        interPunctuationDelay = new WaitForSeconds(interPunctuationDelayFloat);
        textBox = GetComponent<TMP_Text>();
        tutorialManager = FindObjectOfType<TutorialManager>();


        SetText(tutorialManager.currentString);
    }


    public void SetText(string text)
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

        while (currentVisibleCharID < textInfo.characterCount + 1)
        {

            char character = textInfo.characterInfo[currentVisibleCharID].character;
            textBox.maxVisibleCharacters++;

            if (character == '?' || character == '.' || character == ',' || character == ':' || character == ';' || character == '!' || character == '-')
            {
                yield return interPunctuationDelay;
            }
            else
            {
                yield return simpleDelay;
            }

            currentVisibleCharID++;
        }

    }

}

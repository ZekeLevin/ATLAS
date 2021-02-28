using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class MorseScript : MonoBehaviour
{
    public static string output;
    private string inputValue;
    private string[] inputString = new string[4];
    private char[] word = new char[8];

    //keys for checking the which letter the input is
    private readonly string[] valueKey = new string[26]{".-","-...","-.-.","-..",".","..-.","--.","....","..",".---","-.-",".-..","--","-.","---",".--.","--.-",".-.","...","-","..-","...-",".--","-..-","-.--","--.."};
    private readonly char[] letterKey = new char[26] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y','Z' };

    private int blipCounter = 0;
    private int letterCounter = 0;

    //for display UI
    public Text messageText;

    private void Start()
    {
        output = "";
        StartCoroutine(DisplayText());
    }
    // Update is called once per frame
    void Update()
    {
        
            inputValue = Input.inputString;
            if(inputValue == "-" || inputValue == ".")
            {
                inputString[blipCounter] = inputValue;
                Debug.Log(inputValue);
                blipCounter++;
            }
            if (inputValue == "_")
            {
                //chop off the null parts of the string
                string[] parseString = new string[inputString.Length];
                int i = 0;
                foreach(string value in inputString)
                {
                    if (value != null)
                    {
                        parseString[i] = value;
                        i++;
                    }
                }
            //parse the string and print it
            //Debug.Log(string.Join("",parseString));
            //Debug.Log(parseString);
               ParseWord(parseString);
            //StopCoroutine(DisplayText());
            //StartCoroutine(DisplayText());

                //reset the counter and the inputstring array
               blipCounter = 0;
               inputString = new string[4];
            }

        if(Input.GetKeyDown(KeyCode.C))
        {
            ResetWord();
            
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            output = string.Join("", word);
            Debug.Log("Sending: " + output);
            ResetWord();

        }

    }

   void ParseWord(string[] inputString)
    {
        int arrayPlacement = 0;
        foreach(string value in valueKey)
        {
            if(value == string.Join("", inputString))
            {
                word[letterCounter] = letterKey[arrayPlacement];
                letterCounter++;
                Debug.Log("Letter placement:" + letterCounter);
                Debug.Log(new string(word));
                return;
            }
            arrayPlacement++;
        }

        Debug.Log("Missing");    
    }

    void ResetWord()
    {
        //resets word to ______
        word[0] = ' ';
        word[1] = ' ';
        word[2] = ' ';
        word[3] = ' ';
        word[4] = ' ';
        word[5] = ' ';
        word[6] = ' ';
        word[7] = ' ';
        Debug.Log("RESET");
        letterCounter = 0;
        //StopCoroutine(DisplayText());
        //StartCoroutine(DisplayText());
    }


    IEnumerator DisplayText()
    {
        //string lastText = new string(word);

        //while(new string(word) == lastText)
        while(true)
        {
            yield return new WaitForEndOfFrame();
            
            word[letterCounter] = ' ';
            messageText.text = new string(word);
            yield return new WaitForSeconds(.7f);

            word[letterCounter] = '_';
            messageText.text = new string(word);
            yield return new WaitForSeconds(.7f);
        }
    }
}

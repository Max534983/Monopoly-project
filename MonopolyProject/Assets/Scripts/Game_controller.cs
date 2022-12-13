using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Game_controller : MonoBehaviour
{
    string[] playerNames = new string[] { "speler_1", "speler_2", "speler_3", "speler_4", "speler_5", "speler_6" };
    int[] firstThrowValue = new int[] {0,0,0,0,0,0};

    public Text textPlayer_1;
    public Text textPlayer_2;
    public Text textPlayer_3;
    public Text textPlayer_4;
    public Text textPlayer_5;
    public Text textPlayer_6;
    public Text textLower_1;
    public Text textLower_2;
    public Text textLower_3;
    public Text textLower_4;
    public Text textLower_5;
    public Text textLower_6;
    public Text ErrorBox;

    public Animator dobbolsteen1_animation;
    public Animator dobbolsteen2_animation;

    int huidige_speler = 0;

    bool firstThrow = true;
    bool diceThrown = false;
    int dice1 = 0;
    int dice2 = 0;
    int totalOnDice = 0;
    bool dubbel = false;
    bool dubbel1 = false;

    String currentError = "noError";

    void Start()
    {
        gameStartUi();
        switchPlayerUIColor();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void playerTurn() 
    {
        if (firstThrowValue[huidige_speler] != 0 || (firstThrow == false && diceThrown == true && dubbel == false))
        {
            huidige_speler++;
            diceThrown = false;
        }
        else
        {
            currentError = "throwDice";
            error("throwDice");
        }


        if (huidige_speler == 6)
        {
            huidige_speler = 0;
        }

        switchPlayerUIColor();

    }

    public void switchPlayerUIColor() 
    {
        if (huidige_speler == 0)
        {
            textPlayer_1.color = Color.red;

            textPlayer_2.color = Color.green;
            textPlayer_3.color = Color.green;
            textPlayer_4.color = Color.green;
            textPlayer_4.color = Color.green;
            textPlayer_6.color = Color.green;
        }
        else if (huidige_speler == 1)
        {
            textPlayer_2.color = Color.red;

            textPlayer_1.color = Color.green;
            textPlayer_3.color = Color.green;
            textPlayer_4.color = Color.green;
            textPlayer_5.color = Color.green;
            textPlayer_6.color = Color.green;
        }
        else if (huidige_speler == 2)
        {
            textPlayer_3.color = Color.red;

            textPlayer_1.color = Color.green;
            textPlayer_2.color = Color.green;
            textPlayer_4.color = Color.green;
            textPlayer_5.color = Color.green;
            textPlayer_6.color = Color.green;

        }
        else if (huidige_speler == 3)
        {
            textPlayer_4.color = Color.red;

            textPlayer_1.color = Color.green;
            textPlayer_2.color = Color.green;
            textPlayer_3.color = Color.green;
            textPlayer_5.color = Color.green;
            textPlayer_6.color = Color.green;
        }
        else if (huidige_speler == 4)
        {
            textPlayer_5.color = Color.red;

            textPlayer_1.color = Color.green;
            textPlayer_2.color = Color.green;
            textPlayer_3.color = Color.green;
            textPlayer_4.color = Color.green;
            textPlayer_6.color = Color.green;
        }
        else if (huidige_speler == 5)
        {
            textPlayer_6.color = Color.red;

            textPlayer_1.color = Color.green;
            textPlayer_2.color = Color.green;
            textPlayer_3.color = Color.green;
            textPlayer_4.color = Color.green;
            textPlayer_5.color = Color.green;
        }
    }

    //De logica voor een dobbolsteen gooien en firstThrow
    public void throwDice()
    {
        if (diceThrown == false || firstThrow)
        {
            dice1 = 0;
            dice2 = 0;
            totalOnDice = 0;
            dubbel = false;
            dubbel1 = false;

            diceThrown = true;

            if (currentError.Equals("throwDice"))
            {
                error("noError");
                currentError = "noError";
            }

            dice1 = UnityEngine.Random.Range(1, 7);
            dice2 = UnityEngine.Random.Range(1, 7);

            if (dice1 == dice2)
            {
                dubbel = true;
                diceThrown = false;
            }

            if (dice1 == 1 && dice2 == 1)
            {
                dubbel1 = true;
            }

            totalOnDice = dice1 + dice2;

            if (firstThrow == true)
            {
                firstThrowValue[huidige_speler] = totalOnDice;
                showFirstNumberThrown();
                playerTurn();
            }

            if (firstThrowValue[firstThrowValue.Length - 1] > 0 && firstThrow == true)
            {
                firstThrow = false;
                endFirstThrow();
                Debug.Log("first throw is klaar");
            }

            dobbolsteen1_animation.SetInteger("Dice", dice1);
            dobbolsteen2_animation.SetInteger("Dice", dice2);
        }
    }

    public void showFirstNumberThrown()
    {
        if (huidige_speler == 0)
        {
            textLower_1.text = "Threw: " + totalOnDice;
        }
        else if (huidige_speler == 1)
        {
            textLower_2.text = "Threw: " + totalOnDice;
        }
        else if (huidige_speler == 2)
        {
            textLower_3.text = "Threw: " + totalOnDice;
        }
        else if (huidige_speler == 3)
        {
            textLower_4.text = "Threw: " + totalOnDice;
        }
        else if (huidige_speler == 4)
        {
            textLower_5.text = "Threw: " + totalOnDice;
        }
        else if (huidige_speler == 5)
        {
            textLower_6.text = "Threw: " + totalOnDice;
        }
    }

    //hier staan alle soorten error's die op het scherm mogen verschijnen
    public void error(string errorType)
    {   
        if (errorType.Equals("throwDice"))
        {
            ErrorBox.gameObject.SetActive(true);
            ErrorBox.text = "You need to throw the dice";
        }

        if (errorType.Equals("noError"))
        {
            ErrorBox.gameObject.SetActive(false);
        }
    }

    public void endFirstThrow() 
    {
        string[] playerNamesUpdate = new string[] { "", "", "", "", "", "" };

        //Kijk wie er het hoogste gooide
        for (int i = 0; i < playerNames.Length; i++)
        {
            int spelerMetHoogsteNummer = 0;
            int hoogesteNummer = 0;
            for (int y = 0; y < playerNames.Length; y++) 
            {
                if (firstThrowValue[y] > hoogesteNummer)
                {
                    spelerMetHoogsteNummer = y;
                    hoogesteNummer = firstThrowValue[y];
                }
            }
            firstThrowValue[spelerMetHoogsteNummer] = 0;
            playerNamesUpdate[i] = playerNames[spelerMetHoogsteNummer];
            Debug.Log(playerNamesUpdate[i]);
        }

        //verander de namen. laat de mensen die het hoogst gooien op volgorde gaan.
        for (int i = 0; i < playerNames.Length; i++)
        {
            playerNames[i] = playerNamesUpdate[i];
        }

        diceThrown = false;
        gameStartUi();
        updateMoney();
    }

    //Zorg dat de UI er goed uitziet wanneer het spel start
    public void gameStartUi() 
    {   
        textPlayer_1.text = playerNames[0];
        textPlayer_1.color = Color.red;

        textPlayer_2.text = playerNames[1];
        textPlayer_2.color = Color.green;

        textPlayer_3.text = playerNames[2];
        textPlayer_3.color = Color.green;

        textPlayer_4.text = playerNames[3];
        textPlayer_4.color = Color.green;

        textPlayer_5.text = playerNames[4];
        textPlayer_5.color = Color.green;

        textPlayer_6.text = playerNames[5];
        textPlayer_6.color = Color.green;

        ErrorBox.color = Color.red;
        ErrorBox.gameObject.SetActive(false);
    }

    public void updateMoney() 
    {
        //TODO zorg dat het geld dat spelers hebben op het scherm zichtbaar wordt.
        textLower_1.enabled = false;
        textLower_2.enabled = false;
        textLower_3.enabled = false;
        textLower_4.enabled = false;
        textLower_5.enabled = false;
        textLower_6.enabled = false;
    }
}

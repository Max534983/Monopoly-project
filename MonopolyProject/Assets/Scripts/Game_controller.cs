using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Game_controller : MonoBehaviour
{
    string[] playerNames = new string[] { "player1", "player2", "player3", "player4", "player5", "player6" };
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

    public Animator dice1_animation;
    public Animator dice2_animation;

    int currentPlayer = 0;

    bool firstThrow = true;
    bool diceThrown = false;
    int dice1 = 0;
    int dice2 = 0;
    int totalOnDice = 0;
    bool diceDouble = false;
    bool dice1Double = false;

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
        if (firstThrowValue[currentPlayer] != 0 || (firstThrow == false && diceThrown == true && dice == false))
        {
            currentPlayer++;
            diceThrown = false;
        }
        else
        {
            currentError = "throwDice";
            error("throwDice");
        }


        if (currentPlayer == 6)
        {
            currentPlayer = 0;
        }

        switchPlayerUIColor();

    }

    public void switchPlayerUIColor() 
    {
        if (currentPlayer == 0)
        {
            textPlayer_1.color = Color.green;

            textPlayer_2.color = Color.red;
            textPlayer_3.color = Color.red;
            textPlayer_4.color = Color.red;
            textPlayer_4.color = Color.red;
            textPlayer_6.color = Color.red;
        }
        else if (currentPlayer == 1)
        {
            textPlayer_2.color = Color.green;

            textPlayer_1.color = Color.red;
            textPlayer_3.color = Color.red;
            textPlayer_4.color = Color.red;
            textPlayer_5.color = Color.red;
            textPlayer_6.color = Color.red;
        }
        else if (currentPlayer == 2)
        {
            textPlayer_3.color = Color.green;

            textPlayer_1.color = Color.red;
            textPlayer_2.color = Color.red;
            textPlayer_4.color = Color.red;
            textPlayer_5.color = Color.red;
            textPlayer_6.color = Color.red;

        }
        else if (currentPlayer == 3)
        {
            textPlayer_4.color = Color.green;

            textPlayer_1.color = Color.red;
            textPlayer_2.color = Color.red;
            textPlayer_3.color = Color.red;
            textPlayer_5.color = Color.red;
            textPlayer_6.color = Color.red;
        }
        else if (currentPlayer == 4)
        {
            textPlayer_5.color = Color.green;

            textPlayer_1.color = Color.red;
            textPlayer_2.color = Color.red;
            textPlayer_3.color = Color.red;
            textPlayer_4.color = Color.red;
            textPlayer_6.color = Color.red;
        }
        else if (currentPlayer == 5)
        {
            textPlayer_6.color = Color.green;

            textPlayer_1.color = Color.red;
            textPlayer_2.color = Color.red;
            textPlayer_3.color = Color.red;
            textPlayer_4.color = Color.red;
            textPlayer_5.color = Color.red;
        }
    }

    //De logica voor een dobbelsteen gooien en firstThrow
    public void throwDice()
    {
        if (diceThrown == false || firstThrow)
        {
            dice1 = 0;
            dice2 = 0;
            totalOnDice = 0;
            diceDouble = false;
            dice1Double = false;

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
                diceDouble = true;
                diceThrown = false;
            }

            if (dice1 == 1 && dice2 == 1)
            {
                dice1Double = true;
            }

            totalOnDice = dice1 + dice2;

            if (firstThrow == true)
            {
                firstThrowValue[currentPlayer] = totalOnDice;
                showFirstNumberThrown();
                playerTurn();
            }

            if (firstThrowValue[firstThrowValue.Length - 1] > 0 && firstThrow == true)
            {
                firstThrow = false;
                endFirstThrow();
                Debug.Log("first throw is klaar");
            }

            dice1_animation.SetInteger("Dice", dice1);
            dice2_animation.SetInteger("Dice", dice2);
        }
    }

    public void showFirstNumberThrown()
    {
        if (currentPlayer == 0)
        {
            textLower_1.text = "Threw: " + totalOnDice;
        }
        else if (currentPlayer == 1)
        {
            textLower_2.text = "Threw: " + totalOnDice;
        }
        else if (currentPlayer == 2)
        {
            textLower_3.text = "Threw: " + totalOnDice;
        }
        else if (currentPlayer == 3)
        {
            textLower_4.text = "Threw: " + totalOnDice;
        }
        else if (currentPlayer == 4)
        {
            textLower_5.text = "Threw: " + totalOnDice;
        }
        else if (currentPlayer == 5)
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
            int playerWithHighestThrow = 0;
            int highestNumber = 0;
            for (int y = 0; y < playerNames.Length; y++) 
            {
                if (firstThrowValue[y] > highestNumber)
                {
                    playerWithHighestThrow = y;
                    highestNumber = firstThrowValue[y];
                }
            }
            firstThrowValue[playerWithHighestThrow] = 0;
            playerNamesUpdate[i] = playerNames[playerWithHighestThrow];
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
        textPlayer_1.color = Color.green;

        textPlayer_2.text = playerNames[1];
        textPlayer_2.color = Color.red;

        textPlayer_3.text = playerNames[2];
        textPlayer_3.color = Color.red;

        textPlayer_4.text = playerNames[3];
        textPlayer_4.color = Color.red;

        textPlayer_5.text = playerNames[4];
        textPlayer_5.color = Color.red;

        textPlayer_6.text = playerNames[5];
        textPlayer_6.color = Color.red;

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

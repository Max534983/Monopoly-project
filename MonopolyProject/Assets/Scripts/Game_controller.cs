using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Game_controller : MonoBehaviour
{
    string[] playerNames = new string[] {"Player_1", "Player_2", "Player_3", "Player_4" };
    int[] playerLineUp = new int[] { 1, 2, 3, 4 };
    int[] firstThrowValue = new int[] {0,0,0,0};
    int[] playerCurrentTilePos = new int[] {0,0,0,0};

    double bottomRowXCord = -3.325;
    double[] bottomRowYCord = new double[] {-3.1,-2.4,-1.8,-1.2,-0.6,0,0.6,1.2,1.8,2.4};

    public Text textPlayer_1;
    public Text textPlayer_2;
    public Text textPlayer_3;
    public Text textPlayer_4;
    public Text textLower_1;
    public Text textLower_2;
    public Text textLower_3;
    public Text textLower_4;
    public Text ErrorBox;

    public Animator dice1_animation;
    public Animator dice2_animation;

    int currentPlayer = 0;

    public Transform player_1;
    public Transform player_2;
    public Transform player_3;
    public Transform player_4;

    bool firstThrow = true;
    bool diceThrown = false;
    int dice1 = 0;
    int dice2 = 0;
    int totalOnDice = 0;
    bool doubleDice = false;
    bool doubleDice1 = false;

    String currentError = "noError";

    public float moveSpeed = 1;
    bool moving = false;

    void Start()
    {
        fillPlayerNames();
        gameStartUi();
        switchPlayerUIColor();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void fillPlayerNames() 
    {
       if (PlayerNames.Player1 != null) 
       {
            playerNames[0] = PlayerNames.Player1;
       }
       if (PlayerNames.Player2 != null)
       {
            playerNames[1] = PlayerNames.Player2;
       }
       if (PlayerNames.Player3 != null)
       {
             playerNames[2] = PlayerNames.Player3;
       }
       if (PlayerNames.Player4 != null)
       {
            playerNames[3] = PlayerNames.Player4;
       }
    }

    public void playerTurn() 
    {
        if (firstThrowValue[currentPlayer] != 0 || (firstThrow == false && diceThrown == true && doubleDice == false))
        {
            currentPlayer++;
            diceThrown = false;
        }
        else
        {
            currentError = "throwDice";
            error("throwDice");
        }


        if (currentPlayer == 4)
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
        }
        else if (currentPlayer == 1)
        {
            textPlayer_2.color = Color.green;

            textPlayer_1.color = Color.red;
            textPlayer_3.color = Color.red;
            textPlayer_4.color = Color.red;
        }
        else if (currentPlayer == 2)
        {
            textPlayer_3.color = Color.green;

            textPlayer_1.color = Color.red;
            textPlayer_2.color = Color.red;
            textPlayer_4.color = Color.red;

        }
        else if (currentPlayer == 3)
        {
            textPlayer_4.color = Color.green;

            textPlayer_1.color = Color.red;
            textPlayer_2.color = Color.red;
            textPlayer_3.color = Color.red;
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
            doubleDice = false;
            doubleDice1 = false;

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
                doubleDice = true;
                diceThrown = false;
            }

            if (dice1 == 1 && dice2 == 1)
            {
                doubleDice1 = true;
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

            if (!firstThrow) 
            {
                movePlayer();
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
        string[] playerNamesUpdate = new string[] { "", "", "", "" };
        int[] newPlayerLineup = new int[] { 0, 0, 0, 0 };

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
            newPlayerLineup[i] = playerWithHighestThrow;
            playerNamesUpdate[i] = playerNames[playerWithHighestThrow];
            Debug.Log(newPlayerLineup[i]);
        }

        //verander de namen. laat de mensen die het hoogst gooien op volgorde gaan.
        for (int i = 0; i < playerNames.Length; i++)
        {
            playerNames[i] = playerNamesUpdate[i];
            playerLineUp[i] = newPlayerLineup[i];
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
    }

    public void movePlayer()
    {
        playerCurrentTilePos[currentPlayer] = totalOnDice;
        if (playerLineUp[currentPlayer] == 0)
        {
            moving = true;
            startPlayerMove(player_1);
        }
        else if (playerLineUp[currentPlayer] == 1)
        {
            moving = true;
            startPlayerMove(player_2);
        }
        else if (playerLineUp[currentPlayer] == 2)
        {
            moving = true;
            startPlayerMove(player_3);
        }
        else if (playerLineUp[currentPlayer] == 3)
        {
            moving = true;
            startPlayerMove(player_4);
        }
    }

    public void startPlayerMove(Transform player)
    {
        player.position = new Vector2(-(float)bottomRowYCord[playerCurrentTilePos[currentPlayer]], (float)bottomRowXCord);
        moving = false;
    }
}

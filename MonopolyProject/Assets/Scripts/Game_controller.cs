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
    public Text textThrew_1;
    public Text textThrew_2;
    public Text textThrew_3;
    public Text textThrew_4;
    public Text textThrew_5;
    public Text textThrew_6;
    public Text ErrorBox;

    public Animator dobbolsteen1_animation;
    public Animator dobbolsteen2_animation;

    int huidige_speler = 0;

    bool firstThrow = true;
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
        if (firstThrowValue[huidige_speler] != 0)
        {
            huidige_speler++;
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

    public void throwDice()
    {
        dice1 = 0;
        dice2 = 0;
        totalOnDice = 0;
        dubbel = false;
        dubbel1 = false;

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
        }

        if (dice1 == 1 && dice2 == 1)
        {
            dubbel1 = true;
        }

        totalOnDice = dice1 + dice2;

        if (firstThrow == true)
        {
                firstThrowValue[huidige_speler] = totalOnDice;
        }

        if (firstThrowValue[firstThrowValue.Length - 1] > 0)
        {
            firstThrow = false;
        }

        dobbolsteen1_animation.SetInteger("Dice", dice1);
        dobbolsteen2_animation.SetInteger("Dice", dice2);

        Debug.Log("Total on dice is: " + totalOnDice);
        showNumberThrown();
    }

    public void showNumberThrown()
    {
        if (huidige_speler == 0)
        {
            textThrew_1.text = "Threw: " + totalOnDice;
        }
        else if (huidige_speler == 1)
        {
            textThrew_2.text = "Threw: " + totalOnDice;
        }
        else if (huidige_speler == 2)
        {
            textThrew_3.text = "Threw: " + totalOnDice;
        }
        else if (huidige_speler == 3)
        {
            textThrew_4.text = "Threw: " + totalOnDice;
        }
        else if (huidige_speler == 4)
        {
            textThrew_5.text = "Threw: " + totalOnDice;
        }
        else if (huidige_speler == 5)
        {
            textThrew_6.text = "Threw: " + totalOnDice;
        }
    }


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
}

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
    int huidige_speler = 0;

    void Start()
    {
        gameStartUi();
    }

    // Update is called once per frame
    void Update()
    {
        selectPlayer();
    }


    public void selectPlayer() 
    {
        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            if (huidige_speler == 0)
            {
                textPlayer_1.color = Color.green;
                
                textPlayer_2.color = Color.red;
                textPlayer_3.color = Color.red;
                textPlayer_4.color = Color.red;
                textPlayer_4.color = Color.red;
                textPlayer_6.color = Color.red;

            } else if (huidige_speler == 1) 
            {
                textPlayer_2.color = Color.green;

                textPlayer_1.color = Color.red;
                textPlayer_3.color = Color.red;
                textPlayer_4.color = Color.red;
                textPlayer_5.color = Color.red;
                textPlayer_6.color = Color.red;
            } else if (huidige_speler == 2)
            {
                textPlayer_3.color = Color.green;

                textPlayer_1.color = Color.red;
                textPlayer_2.color = Color.red;
                textPlayer_4.color = Color.red;
                textPlayer_5.color = Color.red;
                textPlayer_6.color = Color.red;
            } else if (huidige_speler == 3)
            {
                textPlayer_4.color = Color.green;

                textPlayer_1.color = Color.red;
                textPlayer_2.color = Color.red;
                textPlayer_3.color = Color.red;
                textPlayer_5.color = Color.red;
                textPlayer_6.color = Color.red;
            } else if (huidige_speler == 4)
            {
                textPlayer_5.color = Color.green;

                textPlayer_1.color = Color.red;
                textPlayer_2.color = Color.red;
                textPlayer_3.color = Color.red;
                textPlayer_4.color = Color.red;
                textPlayer_6.color = Color.red;
            } else if (huidige_speler == 5)
            {
                textPlayer_6.color = Color.green;

                textPlayer_1.color = Color.red;
                textPlayer_2.color = Color.red;
                textPlayer_3.color = Color.red;
                textPlayer_4.color = Color.red;
                textPlayer_5.color = Color.red;
            }

            huidige_speler++;

            if (huidige_speler == 6) 
            {
                huidige_speler = 0;
            }
        }
    }

    public void gameStartUi() 
    {
        textPlayer_1.text = playerNames[0];
        textPlayer_1.color = Color.red;

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
    }
}

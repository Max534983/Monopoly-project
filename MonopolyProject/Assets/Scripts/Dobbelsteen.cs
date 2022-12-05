using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dobbelsteen : MonoBehaviour
{
    int dice1 = 0;
    int dice2 = 0;
    int totalOnDice = 0;
    bool dubbel = false;
    bool dubbel1 = false;

    void Start()
    {

    }


    void Update()
    {
        throwDice();
    }

    public void throwDice()
    {
        dice1 = 0;
        dice2 = 0;
        totalOnDice = 0;
        dubbel = false;
        dubbel1 = false;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            dice1 = Random.Range(1, 7);
            dice2 = Random.Range(1, 7);

            if (dice1 == dice2)
            {
                dubbel = true;
            }

            if (dice1==1 && dice2==1)
            {
                dubbel1 = true;
            }

            totalOnDice = dice1 + dice2;
            Debug.Log("---------------------------------------------------------------");
            Debug.Log("Dice 1 was: " + dice1);
            Debug.Log("Dice 2 was: " + dice2);
            Debug.Log("Total on dice is: " + totalOnDice);
            Debug.Log("Is it a dubbel: " + dubbel);
            Debug.Log("Is it a dubbel 1: " + dubbel1);
        }
    }
}

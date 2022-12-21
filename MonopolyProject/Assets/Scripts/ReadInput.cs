using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ReadInput : MonoBehaviour
{

    private string input;

    public void ReadStringInput1(string player1)
    {
    input = player1;
    PlayerNames.Player1 = input;
    }

    public void ReadStringInput2(string player2)
    {
    input = player2;
    PlayerNames.Player2 = input;
    }

    public void ReadStringInput3(string player3)
    {
    input = player3;
    PlayerNames.Player3 = input;
    }

    public void ReadStringInput4(string player4)
    {
    input = player4;
    PlayerNames.Player4 = input;
    }
}

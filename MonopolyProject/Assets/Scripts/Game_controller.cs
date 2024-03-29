using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

public class Game_controller : MonoBehaviour
{
    string[] playerNames = new string[] { "Player_1", "Player_2", "Player_3", "Player_4" };
    int[] playerLineUp = new int[] { 1, 2, 3, 4 };
    int[] firstThrowValue = new int[] { 0, 0, 0, 0 };
    int[] playerCurrentTilePos = new int[] { 0, 0, 0, 0 };
    int[] playerMoney = new int[] { 1500, 1500, 1500, 1500 };
    int[] propertyOwner = new int[] { 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10 };
    int[] propertyCost = new int[] { 60, 60, 200, 100, 100, 120, 150, 140, 140, 160, 200, 180, 180, 200, 220, 220, 240, 200, 260, 260, 150, 260, 300, 300, 320, 200, 350, 400 };
    int[] propertyRent = new int[] { 2, 4, 6, 6, 8, 10, 10, 12, 14, 14, 16, 18, 18, 20, 22, 22, 24, 26, 26, 28, 35, 50 };
    int[] fullSetRent = new int[] { 4, 8, 12, 12, 16, 20, 20, 24, 28, 28, 32, 36, 36, 40, 44, 44, 48, 52, 52, 56, 70, 100 };
    int[] buildLevel = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
    double bottomRowXCord = -3.3;
    double[] bottomRowYCord = new double[] { -3.1, -2.4, -1.8, -1.2, -0.6, 0, 0.6, 1.2, 1.8, 2.4, 3.15 };
    double leftRowYCord = 3.3;
    double[] leftRowXCord = new double[] { 2.4, 1.8, 1.2, 0.6, 0.0, -0.6, -1.2, -1.8, -2.4, -3.15 };
    double topRowXcord = 3.3;
    double[] topRowYcord = new double[] { 2.4, 1.8, 1.2, 0.6, 0.0, -0.6, -1.2, -1.8, -2.4, -3.15 };
    double rightRowXcord = -3.3;
    double[] rightRowYCord = new double[] { -2.4, -1.8, -1.2, -0.6, 0.0, 0.6, 1.2, 1.8, 2.4, 3.15 };
    bool[] bankRuptPlayers = new bool[] { false, false, false, false };


    public Text textPlayer_1;
    public Text textPlayer_2;
    public Text textPlayer_3;
    public Text textPlayer_4;
    public Text textLower_1;
    public Text textLower_2;
    public Text textLower_3;
    public Text textLower_4;
    public Text ErrorBox;
    public GameObject BoxRed1;
    public GameObject BoxGreen1;
    public GameObject BoxYellow1;
    public GameObject BoxBlue1;
    public GameObject BoxRed2;
    public GameObject BoxGreen2;
    public GameObject BoxYellow2;
    public GameObject BoxBlue2;
    public GameObject BoxRed3;
    public GameObject BoxGreen3;
    public GameObject BoxYellow3;
    public GameObject BoxBlue3;
    public GameObject BoxRed4;
    public GameObject BoxGreen4;
    public GameObject BoxYellow4;
    public GameObject BoxBlue4;
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

    public GameObject buyMenu;
    public GameObject buyPanel;
    public Text buyMenuPriceTag;
    bool buyingProperty = false;
    int propertyNumber = 10;

    public GameObject biddingPanel;
    public Text buyMenuPlayer1;
    public Text buyMenuPlayer2;
    public Text buyMenuPlayer3;
    public Text buyMenuPlayer4;
    public InputField inputPlayer1;
    public InputField inputPlayer2;
    public InputField inputPlayer3;
    public InputField inputPlayer4;
    public Text regularValue;
    int highestBid = 0;
    int hasHighestBid = 10;

    public Text whoPaysWho;
    public Text payAmount;
    public GameObject payMenu;

    public GameObject[] propertyMarkers = new GameObject[] { };
    public SpriteRenderer[] propertyMarkersSprites = new SpriteRenderer[] { };
    public Sprite[] propertyMarkersBaseSprites = new Sprite[] { };

    public Sprite[] propertyCards = new Sprite[] { };

    public GameObject startTradePanel;
    public GameObject tradePanel;

    public Text option1Text;
    public Text option2Text;
    public Text option3Text;
    int[] tradeOption = new int[] { 10, 10, 10 };
    int playerPickedForTrade = 10;
    int[] ownedCardId = new int[] { 99, 99, 99, 99, 99, 99, 99, 99, 99, 99, 99, 99, 99, 99, 99, 99, 99, 99, 99, 99, 99, 99, 99, 99, 99, 99, 99, 99 };
    public Image tradePropertyImage;
    int selectedTradeCard = 0;
    int boughtTradeCard = 99;
    public InputField tradePrice;

    public GameObject[] houses = new GameObject[] { };
    public GameObject buildPanel;
    public Image buildPropertyImage;
    public Text buildPropertyCost;
    int[] buildCost = new int[] { 40, 45, 50, 55, 60, 65, 70, 75, 80, 85, 90, 95, 100, 105, 110, 115, 120, 125, 130, 135, 140, 145 };
    int buildingOnProperty = 40;

    public GameObject tradeError;

    public Sprite[] algemeenFondsKaarten = new Sprite[] { };
    public Sprite[] kansKaarten = new Sprite[] { };
    public Image cardShower;
    public GameObject cardScreen;

    string winnerName;
    public GameObject winnerScreen;
    public Text winnerScreenName;
    bool hasMonopoly = false;

    void Start()
    {
        fillPlayerNames();
        gameStartUi();
        switchPlayerUIColor();
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
        if (firstThrowValue[currentPlayer] != 0 || (firstThrow == false && diceThrown == true && doubleDice == false && buyingProperty == false))
        {
            currentPlayer++;
            diceThrown = false;
            payMenu.SetActive(false);
        }
        else if (diceThrown == false)
        {
            currentError = "throwDice";
            error("throwDice");
        }
        else if (buyingProperty == true)
        {
            currentError = "decideToBuy";
            error("decideToBuy");
        }

        if (currentPlayer >= 4)
        {
            currentPlayer = 0;
        }

        for (int i = 0; i < bankRuptPlayers.Length; i++)
        {
            if (bankRuptPlayers[i] == true)
            {
                UnityEngine.Debug.Log(i + " checked " + currentPlayer);
                if (i == currentPlayer)
                {
                    UnityEngine.Debug.Log(playerNames[i] + " bankruptcy " + bankRuptPlayers[i]);
                    currentPlayer = i + 1;
                }

            }
        }

        ownedCardId = new int[] { 99, 99, 99, 99, 99, 99, 99, 99, 99, 99, 99, 99, 99, 99, 99, 99, 99, 99, 99, 99, 99, 99, 99, 99, 99, 99, 99, 99 };


        if (currentPlayer >= 4)
        {
            currentPlayer = 0;

            if (bankRuptPlayers[currentPlayer] == true)
            {
                currentPlayer++;
            }
            if (bankRuptPlayers[currentPlayer] == true)
            {
                currentPlayer++;
            }
            if (bankRuptPlayers[currentPlayer] == true)
            {
                currentPlayer++;
            }
        }

        ErrorBox.gameObject.SetActive(false);
        hasMonopoly = false;
        checkForWinner();
        UnityEngine.Debug.Log("The current player should be: " + currentPlayer);
        switchPlayerUIColor();
        cardScreen.SetActive(false);
    }

    public void checkForWinner()
    {
        int numberOfBankruptPlayers = 0;
        for (int i = 0; i < bankRuptPlayers.Length; i++)
        {
            if (bankRuptPlayers[i] == true)
            { 
                numberOfBankruptPlayers++;
            }
        }

        if (numberOfBankruptPlayers == 3) 
        {
            for (int i = 0; i < bankRuptPlayers.Length; i++)
            {
                if (bankRuptPlayers[i] == false)
                {
                    winnerName = playerNames[i];
                }
            }
            winnerScreen.SetActive(true);
            winnerScreenName.text = winnerName;
        }
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

            if (!firstThrow)
            {
                movePlayer();
            }

            if (firstThrowValue[firstThrowValue.Length - 1] > 0 && firstThrow == true)
            {
                firstThrow = false;
                endFirstThrow();
                UnityEngine.Debug.Log("first throw is klaar");
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

        if (errorType.Equals("decideToBuy"))
        {
            ErrorBox.gameObject.SetActive(true);
            ErrorBox.text = "You need to decide to buy or not to buy";
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
        Sprite[] newMarkerSpriteLineUp = new Sprite[] { propertyMarkersBaseSprites[0], propertyMarkersBaseSprites[1], propertyMarkersBaseSprites[2], propertyMarkersBaseSprites[3] };

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
            UnityEngine.Debug.Log(newPlayerLineup[i]);
        
        }

        //verander de namen. laat de mensen die het hoogst gooien op volgorde gaan.
        for (int i = 0; i < playerNames.Length; i++)
        {
            playerNames[i] = playerNamesUpdate[i];
            playerLineUp[i] = newPlayerLineup[i];
        }

        for (int i = 0; i < playerLineUp.Length; i++)
        {
            propertyMarkersBaseSprites[i] = newMarkerSpriteLineUp[playerLineUp[i]];
        }
        //Box 1
        if (playerLineUp[0]==0){
                BoxRed1.SetActive(false);
                BoxRed1.SetActive(true);
            }
            if (playerLineUp[0]==1){
                BoxRed1.SetActive(false);
                BoxGreen1.SetActive(true);
            }
            if (playerLineUp[0]==2){
                BoxRed1.SetActive(false);
                BoxYellow1.SetActive(true);
            }
            if (playerLineUp[0]==3){
                BoxRed1.SetActive(false);
                BoxBlue1.SetActive(true);
            }

            //Box 2

            if (playerLineUp[1]==0){
                BoxGreen2.SetActive(false);
                BoxRed2.SetActive(true);
            }
            if (playerLineUp[1]==1){
                BoxGreen2.SetActive(false);
                BoxGreen2.SetActive(true);
            }
            if (playerLineUp[1]==2){
                BoxGreen2.SetActive(false);
                BoxYellow2.SetActive(true);
            }
            if (playerLineUp[1]==3){
                BoxGreen2.SetActive(false);
                BoxBlue2.SetActive(true);
            }

            //Box 3

            if (playerLineUp[2]==0){
                BoxYellow3.SetActive(false);
                BoxRed3.SetActive(true);
            }
            if (playerLineUp[2]==1){
                BoxYellow3.SetActive(false);
                BoxGreen3.SetActive(true);
            }
            if (playerLineUp[2]==2){
                BoxYellow3.SetActive(false);
                BoxYellow3.SetActive(true);
            }
            if (playerLineUp[2]==3){
                BoxYellow3.SetActive(false);
                BoxBlue3.SetActive(true);
            }

            //Box 4

            if (playerLineUp[3]==0){
                BoxBlue4.SetActive(false);
                BoxRed4.SetActive(true);
            }
            if (playerLineUp[3]==1){
                BoxBlue4.SetActive(false);
                BoxGreen4.SetActive(true);
            }
            if (playerLineUp[3]==2){
                BoxBlue4.SetActive(false);
                BoxYellow4.SetActive(true);
            }
            if (playerLineUp[3]==3){
                BoxBlue4.SetActive(false);
                BoxBlue4.SetActive(true);
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
        textLower_1.text = playerMoney[0].ToString() + " $";
        textLower_2.text = playerMoney[1].ToString() + " $";
        textLower_3.text = playerMoney[2].ToString() + " $";
        textLower_4.text = playerMoney[3].ToString() + " $";
        checkBankrupt();
    }

    public void movePlayer()
    {
        playerCurrentTilePos[currentPlayer] = playerCurrentTilePos[currentPlayer] + totalOnDice;
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


        if (playerCurrentTilePos[currentPlayer] >= 40)
        {
            playerCurrentTilePos[currentPlayer] = playerCurrentTilePos[currentPlayer] - 40;
            playerMoney[currentPlayer] += 200;
        }

        if (playerCurrentTilePos[currentPlayer] <= 10)
        {
            player.position = new Vector2(-(float)bottomRowYCord[playerCurrentTilePos[currentPlayer]], (float)bottomRowXCord);
        }
        else if (playerCurrentTilePos[currentPlayer] <= 20)
        {
            int leftPos = playerCurrentTilePos[currentPlayer] - 11;
            player.position = new Vector2(-(float)leftRowYCord, -(float)leftRowXCord[leftPos]);
        }
        else if (playerCurrentTilePos[currentPlayer] <= 30)
        {
            int topPos = playerCurrentTilePos[currentPlayer] - 21;
            player.position = new Vector2(-(float)topRowYcord[topPos], (float)topRowXcord);
        }
        else if (playerCurrentTilePos[currentPlayer] <= 40)
        {
            int rightPos = playerCurrentTilePos[currentPlayer] - 31;
            player.position = new Vector2(-(float)rightRowXcord, -(float)rightRowYCord[rightPos]);
        }


        UnityEngine.Debug.Log("after player move pos: " + playerCurrentTilePos[currentPlayer]);
        checkPosType(playerCurrentTilePos[currentPlayer]);

        moving = false;
    }

    public void chanceCards()
    {
        int randomChanceCard = UnityEngine.Random.Range(0, 15);

        if (randomChanceCard == 0)
        {
            playerMoney[currentPlayer] = playerMoney[currentPlayer] - 200;
            cardShower.sprite = kansKaarten[0];
        }

        else if (randomChanceCard == 1)
        {
            playerMoney[currentPlayer] = playerMoney[currentPlayer] + 200;
            cardShower.sprite = kansKaarten[1];
        }

        else if (randomChanceCard == 2)
        {
            playerMoney[currentPlayer] = playerMoney[currentPlayer] - 100;
            cardShower.sprite = kansKaarten[2];
        }

        else if (randomChanceCard == 3)
        {
            playerMoney[currentPlayer] = playerMoney[currentPlayer] - 100;
            cardShower.sprite = kansKaarten[3];
        }

        else if (randomChanceCard == 4)
        {
            playerMoney[currentPlayer] = playerMoney[currentPlayer] - 50;
            cardShower.sprite = kansKaarten[4];
        }

        else if (randomChanceCard == 5)
        {
            playerMoney[currentPlayer] = playerMoney[currentPlayer] - 25;
            cardShower.sprite = kansKaarten[5];
        }

        else if (randomChanceCard == 6)
        {
            playerMoney[currentPlayer] = playerMoney[currentPlayer] + 150;
            cardShower.sprite = kansKaarten[6];
        }

        else if (randomChanceCard == 7)
        {
            playerMoney[currentPlayer] = playerMoney[currentPlayer] + 50;
            cardShower.sprite = kansKaarten[7];
        }

        else if (randomChanceCard == 8)
        {
            playerMoney[currentPlayer] = playerMoney[currentPlayer] - 100;
            cardShower.sprite = kansKaarten[8];
        }

        else if (randomChanceCard == 9)
        {
            playerMoney[currentPlayer] = playerMoney[currentPlayer] + 150;
            cardShower.sprite = kansKaarten[9];
        }

        else if (randomChanceCard == 10)
        {
            playerMoney[currentPlayer] = playerMoney[currentPlayer];
            cardShower.sprite = kansKaarten[10];
        }

        else if (randomChanceCard == 11)
        {
            playerCurrentTilePos[currentPlayer] = 0;

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
            cardShower.sprite = kansKaarten[11];
        }

        else if (randomChanceCard == 12)
        {
            playerMoney[currentPlayer] = playerMoney[currentPlayer] + 50;
            cardShower.sprite = kansKaarten[12];
        }

        else if (randomChanceCard == 13)
        {
            playerMoney[currentPlayer] = playerMoney[currentPlayer] + 100;
            cardShower.sprite = kansKaarten[13];
        }

        else if (randomChanceCard == 14)
        {
            playerMoney[currentPlayer] = playerMoney[currentPlayer] - 100;
            cardShower.sprite = kansKaarten[14];
        }

        else if (randomChanceCard == 15)
        {
            playerMoney[currentPlayer] = playerMoney[currentPlayer] - 200;
            cardShower.sprite = kansKaarten[15];
        }
        cardScreen.SetActive(true);
        updateMoney();
    }


    public void communityChestCards()
    {
        int randomCommunityChestCard = UnityEngine.Random.Range(0, 15);

        if (randomCommunityChestCard == 0)
        {
            playerMoney[currentPlayer] = playerMoney[currentPlayer] + 200;
            cardShower.sprite = algemeenFondsKaarten[0];
        }

        else if (randomCommunityChestCard == 1)
        {
            playerMoney[currentPlayer] = playerMoney[currentPlayer] + 100;
            cardShower.sprite = algemeenFondsKaarten[1];
        }

        else if (randomCommunityChestCard == 2)
        {
            playerMoney[currentPlayer] = playerMoney[currentPlayer] - 150;
            cardShower.sprite = algemeenFondsKaarten[2];
        }

        else if (randomCommunityChestCard == 3)
        {
            playerMoney[currentPlayer] = playerMoney[currentPlayer] + 20;
            cardShower.sprite = algemeenFondsKaarten[3];
        }

        else if (randomCommunityChestCard == 4)
        {
            playerMoney[currentPlayer] = playerMoney[currentPlayer] + 10;
            cardShower.sprite = algemeenFondsKaarten[4];
        }

        else if (randomCommunityChestCard == 5)
        {
            playerMoney[currentPlayer] = playerMoney[currentPlayer] + 50;
            cardShower.sprite = algemeenFondsKaarten[5];
        }

        else if (randomCommunityChestCard == 6)
        {
            //Give current player 4x10=40.
            playerMoney[currentPlayer] = playerMoney[currentPlayer] + 40;

            //Subtract 10 off of every player, which leaves the current player with +10 of every player.
            playerMoney[0] -= 10;
            playerMoney[1] -= 10;
            playerMoney[2] -= 10;
            playerMoney[3] -= 10;
            cardShower.sprite = algemeenFondsKaarten[6];
        }

        else if (randomCommunityChestCard == 7)
        {
            //Give current player 4x50=200.
            playerMoney[currentPlayer] = playerMoney[currentPlayer] + 200;

            //Subtract 50 off of every player, which leaves the current player with +50 of every player.
            playerMoney[0] = 50;
            playerMoney[1] = 50;
            playerMoney[2] = 50;
            playerMoney[3] = 50;
            cardShower.sprite = algemeenFondsKaarten[7];
        }

        else if (randomCommunityChestCard == 8)
        {
            playerMoney[currentPlayer] = playerMoney[currentPlayer] - 10;
            cardShower.sprite = algemeenFondsKaarten[8];
        }

        else if (randomCommunityChestCard == 9)
        {
            playerCurrentTilePos[currentPlayer] = 0;

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
            cardShower.sprite = algemeenFondsKaarten[9];
        }

        else if (randomCommunityChestCard == 10)
        {
            playerMoney[currentPlayer] = playerMoney[currentPlayer] + 25;
            cardShower.sprite = algemeenFondsKaarten[10];
        }

        else if (randomCommunityChestCard == 11)
        {
            playerMoney[currentPlayer] = playerMoney[currentPlayer] + 100;
            cardShower.sprite = algemeenFondsKaarten[11];
        }

        else if (randomCommunityChestCard == 12)
        {
            playerMoney[currentPlayer] = playerMoney[currentPlayer] + 250;
            cardShower.sprite = algemeenFondsKaarten[12];
        }

        else if (randomCommunityChestCard == 13)
        {
            playerCurrentTilePos[currentPlayer] = 0;
            playerMoney[currentPlayer] = playerMoney[currentPlayer] + 200;
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
            cardShower.sprite = algemeenFondsKaarten[13];
        }

        else if (randomCommunityChestCard == 14)
        {
            playerMoney[currentPlayer] = playerMoney[currentPlayer] + 50;
            cardShower.sprite = algemeenFondsKaarten[14];
        }

        else if (randomCommunityChestCard == 15)
        {
            playerMoney[currentPlayer] = playerMoney[currentPlayer] - 50;
            cardShower.sprite = algemeenFondsKaarten[15];
        }
        cardScreen.SetActive(true);
        updateMoney();
    }

    public void checkPosType(int pos)
    {

        int property = 50;
        if (pos == 1)
        {
            property = 0;
        }
        else if (pos == 2)
        {
            algemeenFondsCard();
        }
        else if (pos == 3)
        {
            property = 1;
        }
        else if (pos == 5)
        {
            property = 2;
        }
        else if (pos == 6)
        {
            property = 3;
        }
        else if (pos == 7)
        {
            chanceCard();
        }
        else if (pos == 8)
        {
            property = 4;
        }
        else if (pos == 9)
        {
            property = 5;
        }
        else if (pos == 11)
        {
            property = 6;
        }
        else if (pos == 12)
        {
            property = 7;
        }
        else if (pos == 13)
        {
            property = 8;
        }
        else if (pos == 14)
        {
            property = 9;
        }
        else if (pos == 15)
        {
            property = 10;
        }
        else if (pos == 16)
        {
            property = 11;
        }
        else if (pos == 17)
        {
            algemeenFondsCard();
        }
        else if (pos == 18)
        {
            property = 12;
        }
        else if (pos == 19)
        {
            property = 13;
        }
        else if (pos == 21)
        {
            property = 14;
        }
        else if (pos == 22)
        {
            chanceCard();
        }
        else if (pos == 23)
        {
            property = 15;
        }
        else if (pos == 24)
        {
            property = 16;
        }
        else if (pos == 25)
        {
            property = 17;
        }
        else if (pos == 26)
        {
            property = 18;
        }
        else if (pos == 27)
        {
            property = 19;
        }
        else if (pos == 28)
        {
            property = 20;
        }
        else if (pos == 29)
        {
            property = 21;
        }
        else if (pos == 31)
        {
            property = 22;
        }
        else if (pos == 32)
        {
            property = 23;
        }
        else if (pos == 33)
        {
            algemeenFondsCard();
        }
        else if (pos == 34)
        {
            property = 24;
        }
        else if (pos == 35)
        {
            property = 25;
        }
        else if (pos == 36)
        { 
            chanceCard();
        }
        else if (pos == 37)
        {
            property = 26;
        }
        else if (pos == 39)
        {
            property = 27;
        }

        if (pos == 4)
        {
            playerMoney[currentPlayer] -= 60;
            updateMoney();
            checkBankrupt();
            if (playerMoney[currentPlayer] <= 0)
            {
                returnAllProperty(currentPlayer);
            }
        }
        else if (pos == 38)
        {
            playerMoney[currentPlayer] -= 100;
            updateMoney();
            checkBankrupt();
            if (playerMoney[currentPlayer] <= 0)
            {
                returnAllProperty(currentPlayer);
            }
        }

        UnityEngine.Debug.Log("position: " + pos.ToString());

        if (property != 50)
        {
            if (propertyOwner[property] == 10)
            {
                buyMenu.active = true;
                buyingProperty = true;
                propertyNumber = property;
                buyMenuPriceTag.text = "Purchase price: " + propertyCost[property].ToString();
                regularValue.text = "Regular value: " + propertyCost[property].ToString();
            }
            else
            {
                payPropertyOwner(property);
            }
        }
    }

    public void buyProperty()
    {
        if (currentError.Equals("decideToBuy"))
        {
            error("noError");
            currentError = "noError";
        }

        UnityEngine.Debug.Log("buys for = " + propertyCost[propertyNumber]);
        propertyOwner[propertyNumber] = currentPlayer;
        playerMoney[currentPlayer] = playerMoney[currentPlayer] - propertyCost[propertyNumber];
        buyingProperty = false;
        buyMenu.SetActive(false);
        setPropertyMarker(propertyNumber);
        updateMoney();
    }

    public void didNotBuyProperty()
    {
        buyPanel.SetActive(false);
        biddingPanel.SetActive(true);
        inputPlayer1.text = 0.ToString();
        inputPlayer2.text = 0.ToString();
        inputPlayer3.text = 0.ToString();
        inputPlayer4.text = 0.ToString();
        highestBid = 0;
        hasHighestBid = 10;

        buyMenuPlayer1.text = playerNames[0];
        buyMenuPlayer2.text = playerNames[1];
        buyMenuPlayer3.text = playerNames[2];
        buyMenuPlayer4.text = playerNames[3];
    }

    public void endBidding()
    {
        buyPanel.SetActive(true);
        biddingPanel.SetActive(false);
        buyMenu.SetActive(false);

        UnityEngine.Debug.Log(hasHighestBid + " Won and got " + propertyNumber);

        propertyOwner[propertyNumber] = hasHighestBid;
        playerMoney[hasHighestBid] = playerMoney[hasHighestBid] - highestBid;
        changePropertyMarker(propertyNumber, hasHighestBid);
        buyingProperty = false;
        buyMenu.SetActive(false);
        updateMoney();
    }

    public void newBid(int playerNumber)
    {
        if (playerNumber == 0 && int.Parse(inputPlayer1.text) > highestBid)
        {
            highestBid = int.Parse(inputPlayer1.text);
            hasHighestBid = 0;
        }

        if (playerNumber == 1 && int.Parse(inputPlayer2.text) > highestBid)
        {
            highestBid = int.Parse(inputPlayer2.text);
            hasHighestBid = 1;
        }

        if (playerNumber == 2 && int.Parse(inputPlayer3.text) > highestBid)
        {
            highestBid = int.Parse(inputPlayer3.text);
            hasHighestBid = 2;
        }

        if (playerNumber == 3 && int.Parse(inputPlayer4.text) > highestBid)
        {
            highestBid = int.Parse(inputPlayer4.text);
            hasHighestBid = 3;
        }

        UnityEngine.Debug.Log("Player " + hasHighestBid + " has highest bid with " + highestBid);
    }

    public void payPropertyOwner(int property)
    {
        int payTotal = 0;
        if (currentPlayer != propertyOwner[property])
        {
            if (property == 2 || property == 10 || property == 17 || property == 25)
            {
                playerMoney[currentPlayer] -= trainStation(propertyOwner[property]);
                payTotal = trainStation(propertyOwner[property]);
                playerMoney[propertyOwner[property]] += trainStation(propertyOwner[property]);
                UnityEngine.Debug.Log("Pay for the train");

                if (playerMoney[currentPlayer] <= 0)
                {
                    transferAllProperty(currentPlayer, propertyOwner[property]);
                }
            }
            else if (property == 7 || property == 20)
            {
                if (propertyOwner[7] == propertyOwner[property] && propertyOwner[20] == propertyOwner[property])
                {
                    playerMoney[currentPlayer] -= totalOnDice * 10;
                    payTotal = totalOnDice * 10;
                    playerMoney[propertyOwner[property]] += totalOnDice * 10;
                    UnityEngine.Debug.Log("2 kluts");

                    if (playerMoney[currentPlayer] <= 0)
                    {
                        transferAllProperty(currentPlayer, propertyOwner[property]);
                    }
                }
                else if (propertyOwner[7] == propertyOwner[property] || propertyOwner[20] == propertyOwner[property])
                {
                    playerMoney[currentPlayer] -= totalOnDice * 4;
                    payTotal = totalOnDice * 4;
                    playerMoney[propertyOwner[property]] += totalOnDice * 4;
                    UnityEngine.Debug.Log("1 kluts");

                    if (playerMoney[currentPlayer] <= 0)
                    {
                        transferAllProperty(currentPlayer, propertyOwner[property]);
                    }
                }
            }
            else
            {
                payTotal = payForCommonProperty(property);

                playerMoney[currentPlayer] -= payTotal;
                playerMoney[propertyOwner[property]] += payTotal;

                if (playerMoney[currentPlayer] <= 0)
                {
                    transferAllProperty(currentPlayer, propertyOwner[property]);
                }
            }
            payMenu.SetActive(true);
            whoPaysWho.text = playerNames[currentPlayer] + " pays: " + playerNames[propertyOwner[property]];
            payAmount.text = "The amount: " + payTotal.ToString();

            if (playerMoney[currentPlayer] <= 0)
            {
                checkBankrupt();
            }

            updateMoney();
        }
    }

    public int payForCommonProperty(int property)
    {
        int price = 0;
        int rentPos = 30;
        int ownerNo = propertyOwner[property];

        if (property == 0)
        {
            rentPos = 0;
        }
        if (property == 1)
        {
            rentPos = 1;
        }
        if (property == 3)
        {
            rentPos = 2;
        }
        if (property == 4)
        {
            rentPos = 3;
        }
        if (property == 5)
        {
            rentPos = 4;
        }
        if (property == 6)
        {
            rentPos = 5;
        }
        if (property == 8)
        {
            rentPos = 6;
        }
        if (property == 9)
        {
            rentPos = 7;
        }
        if (property == 11)
        {
            rentPos = 8;
        }
        if (property == 12)
        {
            rentPos = 9;
        }
        if (property == 13)
        {
            rentPos = 10;
        }
        if (property == 14)
        {
            rentPos = 11;
        }
        if (property == 15)
        {
            rentPos = 12;
        }
        if (property == 16)
        {
            rentPos = 13;
        }
        if (property == 18)
        {
            rentPos = 14;
        }
        if (property == 19)
        {
            rentPos = 15;
        }
        if (property == 21)
        {
            rentPos = 16;
        }
        if (property == 22)
        {
            rentPos = 17;
        }
        if (property == 23)
        {
            rentPos = 18;
        }
        if (property == 24)
        {
            rentPos = 19;
        }
        if (property == 26)
        {
            rentPos = 20;
        }
        if (property == 27)
        {
            rentPos = 21;
        }

        price = propertyRent[rentPos];

        if (propertyOwner[0] == ownerNo && propertyOwner[1] == ownerNo)
        {
            price = fullSetRent[rentPos] + (buildLevel[rentPos] * 10);
        }
        if (propertyOwner[3] == ownerNo && propertyOwner[4] == ownerNo && propertyOwner[5] == ownerNo)
        {
            price = fullSetRent[rentPos] + (buildLevel[rentPos] * 20);
        }
        if (propertyOwner[6] == ownerNo && propertyOwner[8] == ownerNo && propertyOwner[9] == ownerNo)
        {
            price = fullSetRent[rentPos] + (buildLevel[rentPos] * 30);
        }
        if (propertyOwner[11] == ownerNo && propertyOwner[12] == ownerNo && propertyOwner[13] == ownerNo)
        {
            price = fullSetRent[rentPos] + (buildLevel[rentPos] * 40);
        }
        if (propertyOwner[14] == ownerNo && propertyOwner[15] == ownerNo && propertyOwner[16] == ownerNo)
        {
            price = fullSetRent[rentPos] + (buildLevel[rentPos] * 50);
        }
        if (propertyOwner[18] == ownerNo && propertyOwner[19] == ownerNo && propertyOwner[21] == ownerNo)
        {
            price = fullSetRent[rentPos] + (buildLevel[rentPos] * 60);
        }
        if (propertyOwner[22] == ownerNo && propertyOwner[23] == ownerNo && propertyOwner[24] == ownerNo)
        {
            price = fullSetRent[rentPos] + (buildLevel[rentPos] * 70);
        }
        if (propertyOwner[26] == ownerNo && propertyOwner[27] == ownerNo)
        {
            price = fullSetRent[rentPos] + (buildLevel[rentPos] * 80);
        }

        return price;
    }

    public int trainStation(int playerNr)
    {
        int totalPrice = 0;
        int owns = 0;
        if (propertyOwner[2] == playerNr)
        {
            totalPrice += 25;
            owns += 1;
        }
        if (propertyOwner[10] == playerNr)
        {
            totalPrice += 25;
            owns += 1;
        }
        if (propertyOwner[17] == playerNr)
        {
            totalPrice += 25;
            owns += 1;
        }
        if (propertyOwner[25] == playerNr)
        {
            totalPrice += 25;
            owns += 1;
        }

        if (owns == 3)
        {
            totalPrice = 100;
        }

        if (owns == 4)
        {
            totalPrice = 200;
        }

        return totalPrice;
    }

    public void hidePayMenu()
    {
        payMenu.SetActive(false);
    }

    public void checkBankrupt()
    {
        if (playerMoney[currentPlayer] <= 0)
        {
            UnityEngine.Debug.Log("Bankrupted player: " + currentPlayer);
            bankRuptPlayers[currentPlayer] = true;
            if (currentPlayer == 0)
            {
                textLower_1.enabled = false;
                textPlayer_1.enabled = false;
            }
            if (currentPlayer == 1)
            {
                textLower_2.enabled = false;
                textPlayer_2.enabled = false;
            }
            if (currentPlayer == 2)
            {
                textLower_3.enabled = false;
                textPlayer_3.enabled = false;
            }
            if (currentPlayer == 3)
            {
                textLower_4.enabled = false;
                textPlayer_4.enabled = false;
            }
            playerTurn();
        }
    }

    public void transferAllProperty(int bankruptPlayerNr, int propertyOwnerNr)
    {
        for (int i = 0; i < propertyOwner.Length; i++)
        {
            if (propertyOwner[i] == bankruptPlayerNr)
            {
                propertyOwner[i] = propertyOwnerNr;
                changePropertyMarker(i, propertyOwnerNr);
            }
        }
    }

    public void returnAllProperty(int bankruptPlayerNr)
    {
        for (int i = 0; i < propertyOwner.Length; i++)
        {
            if (propertyOwner[i] == bankruptPlayerNr)
            {
                propertyOwner[i] = 10;
                disablePropertyMarker(i);
            }
        }
    }
    public void changePropertyMarker(int property, int newOwner)
    {
        UnityEngine.Debug.Log("Changed owner");
        propertyMarkers[property].SetActive(true);
        propertyMarkersSprites[property].sprite = propertyMarkersBaseSprites[newOwner];
    }

    public void disablePropertyMarker(int property)
    {
        UnityEngine.Debug.Log("Disabled property markers");
        propertyMarkers[property].SetActive(false);
    }

    public void setPropertyMarker(int property)
    {
        UnityEngine.Debug.Log("Marked property: " + property);
        propertyMarkers[property].SetActive(true);
        propertyMarkersSprites[property].sprite = propertyMarkersBaseSprites[currentPlayer];
    }

    public void startTrade()
    {
        if (firstThrow == false)
        {
            startTradePanel.SetActive(true);
            UnityEngine.Debug.Log("Start trade");
            int ingevuld = 0;
            int[] hasNoProperty = new int[] {10,10,10,10};
            for (int i = 0; i < playerNames.Length; i++)
            {
                if (i != currentPlayer)
                {
                    if (ingevuld == 0)
                    {
                        option1Text.text = playerNames[i];
                        tradeOption[0] = i;
                    }
                    if (ingevuld == 1)
                    {
                        option2Text.text = playerNames[i];
                        tradeOption[1] = i;
                    }
                    if (ingevuld == 2)
                    {
                        option3Text.text = playerNames[i];
                        tradeOption[2] = i;
                    }
                    ingevuld++;
                }
            }
        }
    }

    public void stopTrade()
    {
        startTradePanel.SetActive(false);
        tradeError.SetActive(false);
    }

    public void option1()
    {
        ownedCardId = new int[] { 99, 99, 99, 99, 99, 99, 99, 99, 99, 99, 99, 99, 99, 99, 99, 99, 99, 99, 99, 99, 99, 99, 99, 99, 99, 99, 99, 99 };
        startTradePanel.SetActive(false);
        playerPickedForTrade = tradeOption[0];
        tradePanel.SetActive(true);
        int nr = 0;
        for (int i = 0; i < propertyOwner.Length; i++)
        {
            if (propertyOwner[i] == playerPickedForTrade)
            {
                ownedCardId[nr] = i;
                nr++;
            }
        }

        if (ownedCardId[0] != 99)
        {
            tradePropertyImage.sprite = propertyCards[ownedCardId[0]];
            tradeError.SetActive(false);
        }
        else 
        {
            startTradePanel.SetActive(true);
            tradePanel.SetActive(false);
            tradeError.SetActive(true);
        }
    }

    public void option2()
    {
        ownedCardId = new int[] { 99, 99, 99, 99, 99, 99, 99, 99, 99, 99, 99, 99, 99, 99, 99, 99, 99, 99, 99, 99, 99, 99, 99, 99, 99, 99, 99, 99 };
        startTradePanel.SetActive(false);
        playerPickedForTrade = tradeOption[1];
        tradePanel.SetActive(true);
        int nr = 0;
        for (int i = 0; i < propertyOwner.Length; i++)
        {
            if (propertyOwner[i] == playerPickedForTrade)
            {
                ownedCardId[nr] = i;
                nr++;
            }
        }

        if (ownedCardId[0] != 99)
        {
            tradePropertyImage.sprite = propertyCards[ownedCardId[0]];
            tradeError.SetActive(false);
        }
        else
        {
            startTradePanel.SetActive(true);
            tradePanel.SetActive(false);
            tradeError.SetActive(true);
        }
    }

    public void option3()
    {
        ownedCardId = new int[] { 99, 99, 99, 99, 99, 99, 99, 99, 99, 99, 99, 99, 99, 99, 99, 99, 99, 99, 99, 99, 99, 99, 99, 99, 99, 99, 99, 99 };
        startTradePanel.SetActive(false);
        playerPickedForTrade = tradeOption[2];
        tradePanel.SetActive(true);
        int nr = 0;
        for (int i = 0; i < propertyOwner.Length; i++)
        {
            if (propertyOwner[i] == playerPickedForTrade)
            {
                ownedCardId[nr] = i;
                nr++;
            }
        }

        if (ownedCardId[0] != 99)
        {
            tradePropertyImage.sprite = propertyCards[ownedCardId[0]];
            tradeError.SetActive(false);
        }
        else
        {
            startTradePanel.SetActive(true);
            tradePanel.SetActive(false);
            tradeError.SetActive(true);
        }
    }

    public void noDeal()
    {
        startTradePanel.SetActive(true);
        tradePanel.SetActive(false);
        tradeError.SetActive(false);
    }

    public void nextCard()
    {
        selectedTradeCard++;
        if (ownedCardId[selectedTradeCard] == 99)
        {
            selectedTradeCard = 0;
        }
        tradePropertyImage.sprite = propertyCards[ownedCardId[selectedTradeCard]];
    }

    public void Deal()
    {
        playerMoney[currentPlayer] -= int.Parse(tradePrice.text);
        playerMoney[playerPickedForTrade] += int.Parse(tradePrice.text);
        propertyOwner[ownedCardId[selectedTradeCard]] = currentPlayer;
        updateMoney();
        changePropertyMarker(ownedCardId[selectedTradeCard], currentPlayer);
        startTradePanel.SetActive(false);
        tradePanel.SetActive(false);
        tradePrice.text = "0";
    }

    public void openBuildPanel()
    {
        if (firstThrow == false)
        {
            checkForMonopoly();
            if (hasMonopoly == true)
            {
                buildPanel.SetActive(true);
                int cost = buildHousePrice();
                buildPropertyCost.text = "Build price: " + cost.ToString();
                buildPropertyImage.sprite = propertyCards[ownedCardId[0]];
            }
            else
            {
                ErrorBox.gameObject.SetActive(true);
                ErrorBox.text = "You need to own all cards of one color";
            }
        }
    }

    public void checkForMonopoly()
    {
        ownedCardId = new int[] { 99, 99, 99, 99, 99, 99, 99, 99, 99, 99, 99, 99, 99, 99, 99, 99, 99, 99, 99, 99, 99, 99, 99, 99, 99, 99, 99, 99 };
        int nr = 0;
        if (propertyOwner[0] == currentPlayer && propertyOwner[1] == currentPlayer)
        {
            hasMonopoly = true;
            ownedCardId[nr] = 0;
            nr++;
            ownedCardId[nr] = 1;
            nr++;
        }
        if (propertyOwner[3] == currentPlayer && propertyOwner[4] == currentPlayer && propertyOwner[5] == currentPlayer)
        {
            hasMonopoly = true;
            ownedCardId[nr] = 3;
            nr++;
            ownedCardId[nr] = 4;
            nr++;
            ownedCardId[nr] = 5;
            nr++;
        }
        if (propertyOwner[6] == currentPlayer && propertyOwner[8] == currentPlayer && propertyOwner[9] == currentPlayer)
        {
            hasMonopoly = true;
            ownedCardId[nr] = 6;
            nr++;
            ownedCardId[nr] = 8;
            nr++;
            ownedCardId[nr] = 9;
            nr++;
        }
        if (propertyOwner[11] == currentPlayer && propertyOwner[12] == currentPlayer && propertyOwner[13] == currentPlayer)
        {
            hasMonopoly = true;
            ownedCardId[nr] = 11;
            nr++;
            ownedCardId[nr] = 12;
            nr++;
            ownedCardId[nr] = 13;
            nr++;
        }
        if (propertyOwner[14] == currentPlayer && propertyOwner[15] == currentPlayer && propertyOwner[16] == currentPlayer)
        {
            hasMonopoly = true;
            ownedCardId[nr] = 14;
            nr++;
            ownedCardId[nr] = 15;
            nr++;
            ownedCardId[nr] = 16;
            nr++;
        }
        if (propertyOwner[18] == currentPlayer && propertyOwner[19] == currentPlayer && propertyOwner[21] == currentPlayer)
        {
            hasMonopoly = true;
            ownedCardId[nr] = 18;
            nr++;
            ownedCardId[nr] = 19;
            nr++;
            ownedCardId[nr] = 21;
            nr++;
        }
        if (propertyOwner[22] == currentPlayer && propertyOwner[23] == currentPlayer && propertyOwner[24] == currentPlayer)
        {
            hasMonopoly = true;
            ownedCardId[nr] = 22;
            nr++;
            ownedCardId[nr] = 23;
            nr++;
            ownedCardId[nr] = 24;
            nr++;
        }
        if (propertyOwner[26] == currentPlayer && propertyOwner[27] == currentPlayer)
        {
            hasMonopoly = true;
            ownedCardId[nr] = 26;
            nr++;
            ownedCardId[nr] = 27;
        }
    }

    public void closeBuildPanel()
    {
        buildPanel.SetActive(false);
    }

    public void nextCardInBuidMenu()
    {
        selectedTradeCard++;
        if (ownedCardId[selectedTradeCard] == 99)
        {
            selectedTradeCard = 0;
        }
        buildPropertyImage.sprite = propertyCards[ownedCardId[selectedTradeCard]];
        int cost = buildHousePrice();
        buildPropertyCost.text = "Build price: " + cost.ToString();
    }

    public int buildHousePrice()
    {
        int property = ownedCardId[selectedTradeCard];
        int buildPos = 30;
        if (property == 0)
        {
            buildPos = 0;
        }
        if (property == 1)
        {
            buildPos = 1;
        }
        if (property == 3)
        {
            buildPos = 2;
        }
        if (property == 4)
        {
            buildPos = 3;
        }
        if (property == 5)
        {
            buildPos = 4;
        }
        if (property == 6)
        {
            buildPos = 5;
        }
        if (property == 8)
        {
            buildPos = 6;
        }
        if (property == 9)
        {
            buildPos = 7;
        }
        if (property == 11)
        {
            buildPos = 8;
        }
        if (property == 12)
        {
            buildPos = 9;
        }
        if (property == 13)
        {
            buildPos = 10;
        }
        if (property == 14)
        {
            buildPos = 11;
        }
        if (property == 15)
        {
            buildPos = 12;
        }
        if (property == 16)
        {
            buildPos = 13;
        }
        if (property == 18)
        {
            buildPos = 14;
        }
        if (property == 19)
        {
            buildPos = 15;
        }
        if (property == 21)
        {
            buildPos = 16;
        }
        if (property == 22)
        {
            buildPos = 17;
        }
        if (property == 23)
        {
            buildPos = 18;
        }
        if (property == 24)
        {
            buildPos = 19;
        }
        if (property == 26)
        {
            buildPos = 20;
        }
        if (property == 27)
        {
            buildPos = 21;
        }

        buildingOnProperty = buildPos;
        return buildCost[buildPos] + (buildLevel[buildPos] * 30);
    }

    public void buidHouse()
    {
        if (buildLevel[buildingOnProperty] <= 4)
        {
            int cost = buildHousePrice();
            playerMoney[currentPlayer] -= cost;
            updateMoney();
            buildLevel[buildingOnProperty] += 1;
            showHouseBuild();
            cost = buildHousePrice();
            buildPropertyCost.text = "Build price: " + cost.ToString();
        }
        
        if (buildLevel[buildingOnProperty] == 5)
        {
            buildPropertyCost.text = "Fully build";
        }
    }

    public void showHouseBuild()
    {
        int houseToShow = buildLevel[buildingOnProperty] - 1;
        int skip = buildingOnProperty * 5;
        if (buildingOnProperty == 0)
        {
            houses[houseToShow + skip].SetActive(true);
        }
        if (buildingOnProperty == 1)
        {
            houses[houseToShow + skip].SetActive(true);
        }
        if (buildingOnProperty == 2)
        {
            houses[houseToShow + skip].SetActive(true);
        }
        if (buildingOnProperty == 3)
        {
            houses[houseToShow + skip].SetActive(true);
        }
        if (buildingOnProperty == 4)
        {
            houses[houseToShow + skip].SetActive(true);
        }
        if (buildingOnProperty == 5)
        {
            houses[houseToShow + skip].SetActive(true);
        }
        if (buildingOnProperty == 6)
        {
            houses[houseToShow + skip].SetActive(true);
        }
        if (buildingOnProperty == 7)
        {
            houses[houseToShow + skip].SetActive(true);
        }
        if (buildingOnProperty == 8)
        {
            houses[houseToShow + skip].SetActive(true);
        }
        if (buildingOnProperty == 9)
        {
            houses[houseToShow + skip].SetActive(true);
        }
        if (buildingOnProperty == 10)
        {
            houses[houseToShow + skip].SetActive(true);
        }
        if (buildingOnProperty == 11)
        {
            houses[houseToShow + skip].SetActive(true);
        }
        if (buildingOnProperty == 12)
        {
            houses[houseToShow + skip].SetActive(true);
        }
        if (buildingOnProperty == 13)
        {
            houses[houseToShow + skip].SetActive(true);
        }
        if (buildingOnProperty == 14)
        {
            houses[houseToShow + skip].SetActive(true);
        }
        if (buildingOnProperty == 15)
        {
            houses[houseToShow + skip].SetActive(true);
        }
        if (buildingOnProperty == 16)
        {
            houses[houseToShow + skip].SetActive(true);
        }
        if (buildingOnProperty == 17)
        {
            houses[houseToShow + skip].SetActive(true);
        }
        if (buildingOnProperty == 18)
        {
            houses[houseToShow + skip].SetActive(true);
        }
        if (buildingOnProperty == 19)
        {
            houses[houseToShow + skip].SetActive(true);
        }
        if (buildingOnProperty == 20)
        {
            houses[houseToShow + skip].SetActive(true);
        }
        if (buildingOnProperty == 21)
        {
            houses[houseToShow + skip].SetActive(true);
        }

    }

    public void chanceCard()
    {
        UnityEngine.Debug.Log("Kans kaart");
        chanceCards();
    }

    public void algemeenFondsCard()
    {
        UnityEngine.Debug.Log("algemeen fonds kaart");
        communityChestCards();
    }

    public void closeCardScreen()
    { 
        cardScreen.SetActive(false);
    }

    public void goToStart()
    {
        SceneManager.LoadScene(0);
    }
}


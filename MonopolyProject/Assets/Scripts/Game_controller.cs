using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Game_controller : MonoBehaviour
{
    string[] playerNames = new string[] {"Player_1", "Player_2", "Player_3", "Player_4" };
    int[] playerLineUp = new int[] { 1, 2, 3, 4 };
    int[] firstThrowValue = new int[] {0,0,0,0};
    int[] playerCurrentTilePos = new int[] {0,0,0,0};
    int[] playerMoney = new int[] { 1500, 1500, 1500, 1500 };
    int[] propertyOwner = new int[] {10,10,10,10,10,10,10,10,10,10,10,10,10,10,10,10,10,10,10,10,10,10,10,10,10,10,10,10 };
    int[] propertyCost = new int[] {60,60,200,100,100,120,150,140,140,160,200,180,180,200,220,220,240,200,260,260,150,260,300,300,320,200,350,400};
    int[] propertyRent = new int[] { 2, 4, 6, 6, 8, 10, 10, 12, 14, 14, 16, 18, 18, 20, 22, 22, 24, 26, 26, 28, 35, 50 };
    int[] fullSetRent = new int[] {4,8,12,12,16,20,20,24,28,28,32,36,36,40,44,44,48,52,52,56,70,100};
    int[] buildLevel = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,0,0,0 };
    double bottomRowXCord = -3.3;
    double[] bottomRowYCord = new double[] {-3.1,-2.4,-1.8,-1.2,-0.6,0,0.6,1.2,1.8,2.4,3.15};
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

    int turn = 0;

    public Text whoPaysWho;
    public Text payAmount;
    public GameObject payMenu;

    public GameObject[] propertyMarkers = new GameObject[] {};
    public SpriteRenderer[] propertyMarkersSprites = new SpriteRenderer[] { };
    public Sprite[] propertyMarkersBaseSprites = new Sprite[] { };

    public Sprite[] propertyCards = new Sprite[] { };

    public GameObject startTradePanel;
    public GameObject tradePanel;

    public Text option1Text;
    public Text option2Text;
    public Text option3Text;
    int[] tradeOption = new int[] {10,10,10 };
    int playerPickedForTrade = 10;
    int[] ownedCardId = new int[] {99,99,99,99,99,99,99,99,99,99,99,99,99,99,99,99,99,99,99,99,99,99,99,99,99,99,99,99};
    public UnityEngine.UI.Image tradePropertyImage;
    int selectedTradeCard = 0;
    int boughtTradeCard = 99;
    public InputField tradePrice;

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
                Debug.Log(i + " checked " + currentPlayer);
                if (i == currentPlayer)
                {
                    Debug.Log(playerNames[i] + " bankruptcy " + bankRuptPlayers[i]);
                    currentPlayer = i + 1;
                }

            }
        }
        


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
        Debug.Log("The current player should be: " + currentPlayer);
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

            if (!firstThrow)
            {
                movePlayer();
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
            Debug.Log(newPlayerLineup[i]);
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
            player.position = new Vector2(-(float)rightRowXcord, - (float)rightRowYCord[rightPos] );
        }


        Debug.Log("after player move pos: "+playerCurrentTilePos[currentPlayer]);
        checkPosType(playerCurrentTilePos[currentPlayer]);

        moving = false;
    }

    public void chanceCards()
    {
    int randomChanceCard = UnityEngine.Random.Range(0, 15);

        if(randomChanceCard == 0)
        {
            playerMoney[currentPlayer] = playerMoney[currentPlayer]-200;
        }

        else if(randomChanceCard == 1)
        {
            playerMoney[currentPlayer] = playerMoney[currentPlayer]+200;
        }

        else if(randomChanceCard == 2)
        {
            playerMoney[currentPlayer] = playerMoney[currentPlayer]-100;
        }

        else if(randomChanceCard == 3)
        {
            playerMoney[currentPlayer] = playerMoney[currentPlayer]-100;
        }

        else if(randomChanceCard == 4)
        {
            playerMoney[currentPlayer] = playerMoney[currentPlayer]-50;
        }

        else if(randomChanceCard == 5)
        {
            playerMoney[currentPlayer] = playerMoney[currentPlayer]-25;
        }

        else if(randomChanceCard == 6)
        {
            playerMoney[currentPlayer] = playerMoney[currentPlayer]+150;
        }

        else if(randomChanceCard == 7)
        {
            playerMoney[currentPlayer] = playerMoney[currentPlayer]+50;
        }

        else if(randomChanceCard == 8)
        {
            playerMoney[currentPlayer] = playerMoney[currentPlayer]-100;
        }

        else if(randomChanceCard == 9)
        {
            playerMoney[currentPlayer] = playerMoney[currentPlayer]+150;
        }

        else if(randomChanceCard == 10)
        {
            playerMoney[currentPlayer] = playerMoney[currentPlayer];
        }

        else if(randomChanceCard == 11)
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
        }

        else if(randomChanceCard == 12)
        {
            playerMoney[currentPlayer] = playerMoney[currentPlayer]+50;
        }

        else if(randomChanceCard == 13)
        {
            playerMoney[currentPlayer] = playerMoney[currentPlayer]+100;
        }

        else if(randomChanceCard == 14)
        {
            playerMoney[currentPlayer] = playerMoney[currentPlayer]-100;
        }

        else if(randomChanceCard == 15)
        {
            playerMoney[currentPlayer] = playerMoney[currentPlayer]-200;
        }
    }


public void communityChestCards()
    {
    int randomCommunityChestCard = UnityEngine.Random.Range(0, 15);

        if(randomCommunityChestCard == 0)
        {
            playerMoney[currentPlayer] = playerMoney[currentPlayer]+200;
        }

        else if(randomCommunityChestCard == 1)
        {
            playerMoney[currentPlayer] = playerMoney[currentPlayer]+100;
        }

        else if(randomCommunityChestCard == 2)
        {
            playerMoney[currentPlayer] = playerMoney[currentPlayer]-150;

        }

        else if(randomCommunityChestCard == 3)
        {
            playerMoney[currentPlayer] = playerMoney[currentPlayer]+20;
        }

        else if(randomCommunityChestCard == 4)
        {
            playerMoney[currentPlayer] = playerMoney[currentPlayer]+10;
        }

        else if(randomCommunityChestCard == 5)
        {
            playerMoney[currentPlayer] = playerMoney[currentPlayer]+50;
        }

        else if(randomCommunityChestCard == 6)
        {
            //Give current player 4x10=40.
            playerMoney[currentPlayer] = playerMoney[currentPlayer]+40;

            //Subtract 10 off of every player, which leaves the current player with +10 of every player.
            playerMoney[1] = playerMoney[1]-10;
            playerMoney[2] = playerMoney[2]-10;
            playerMoney[3] = playerMoney[3]-10;
            playerMoney[4] = playerMoney[4]-10;
        }

        else if(randomCommunityChestCard == 7)
        {
            //Give current player 4x50=200.
            playerMoney[currentPlayer] = playerMoney[currentPlayer]+200;

            //Subtract 50 off of every player, which leaves the current player with +50 of every player.
            playerMoney[1] = playerMoney[1]-50;
            playerMoney[2] = playerMoney[2]-50;
            playerMoney[3] = playerMoney[3]-50;
            playerMoney[4] = playerMoney[4]-50;
        }

        else if(randomCommunityChestCard == 8)
        {
            playerMoney[currentPlayer] = playerMoney[currentPlayer]-10;
        }

        else if(randomCommunityChestCard == 9)
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
        }

        else if(randomCommunityChestCard == 10)
        {
            playerMoney[currentPlayer] = playerMoney[currentPlayer]+25;
        }

        else if(randomCommunityChestCard == 11)
        {
            playerMoney[currentPlayer] = playerMoney[currentPlayer]+100;
        }

        else if(randomCommunityChestCard == 12)
        {
            playerMoney[currentPlayer] = playerMoney[currentPlayer]+250;
        }

        else if(randomCommunityChestCard == 13)
        {
            playerCurrentTilePos[currentPlayer] = 0;
            playerMoney[currentPlayer] = playerMoney[currentPlayer]+200;
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

        else if(randomCommunityChestCard == 14)
        {
            playerMoney[currentPlayer] = playerMoney[currentPlayer]+50;
        }

        else if(randomCommunityChestCard == 15)
        {
            playerMoney[currentPlayer] = playerMoney[currentPlayer]-50;
        }
    }

    public void checkPosType(int pos)
    {

        int property = 50;
        if (pos == 1)
        {
            property = 0;
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
        else if (pos == 34)
        {
            property = 24;
        }
        else if (pos == 35)
        {
            property = 25;
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

        Debug.Log("position: " + pos.ToString());

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

        Debug.Log("buys for = " + propertyCost[propertyNumber]);
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

        Debug.Log(hasHighestBid + " Won and got " + propertyNumber);

        propertyOwner[propertyNumber] = hasHighestBid;
        playerMoney[hasHighestBid] = playerMoney[hasHighestBid] - highestBid;
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

        Debug.Log("Player " + hasHighestBid + " has highest bid with " + highestBid);
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
                Debug.Log("Pay for the train");

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
                    payTotal= totalOnDice * 10;
                    playerMoney[propertyOwner[property]] += totalOnDice * 10;
                    Debug.Log("2 kluts");

                    if (playerMoney[currentPlayer] <= 0)
                    {
                        transferAllProperty(currentPlayer, propertyOwner[property]);
                    }
                }
                else if (propertyOwner[7] == propertyOwner[property] || propertyOwner[20] == propertyOwner[property])
                {
                    playerMoney[currentPlayer] -= totalOnDice * 4;
                    payTotal= totalOnDice * 4;
                    playerMoney[propertyOwner[property]] += totalOnDice * 4;
                    Debug.Log("1 kluts");

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
            rentPos= 0;
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
            Debug.Log("Bankrupted player: " + currentPlayer);
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
        Debug.Log("Changed owner");
        propertyMarkers[property].SetActive(true);
        propertyMarkersSprites[property].sprite = propertyMarkersBaseSprites[newOwner];
    }

    public void disablePropertyMarker(int property)
    {
        Debug.Log("Disabled property markers");
        propertyMarkers[property].SetActive(false);
    }

    public void setPropertyMarker(int property)
    {
        Debug.Log("Marked property: " + property);
        propertyMarkers[property].SetActive(true);
        propertyMarkersSprites[property].sprite = propertyMarkersBaseSprites[currentPlayer];
    }

    public void startTrade()
    {
        if (firstThrow == false)
        {
            startTradePanel.SetActive(true);
            Debug.Log("Start trade");
            int ingevuld = 0;
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
    }

    public void option1()
    {
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
        tradePropertyImage.sprite = propertyCards[ownedCardId[0]];
    }

    public void option2() 
    {
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
        tradePropertyImage.sprite = propertyCards[ownedCardId[0]];
    }

    public void option3()
    {
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
        tradePropertyImage.sprite = propertyCards[ownedCardId[0]];
    }

    public void noDeal()
    { 
        startTradePanel.SetActive(true);
        tradePanel.SetActive(false);
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


}


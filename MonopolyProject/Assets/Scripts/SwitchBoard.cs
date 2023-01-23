using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;


public class SwitchBoard : MonoBehaviour
{
public GameObject GameObjectToDeactivate;

public void playerCount()
{
if (PlayerNames.Player1 != null && PlayerNames.Player2 != null && PlayerNames.Player3 != null && PlayerNames.Player4 != null){
SceneManager.LoadScene(0);
}
else {
SceneManager.LoadScene(2);
PlayerNames.Player1 = null;
PlayerNames.Player2 = null;
PlayerNames.Player3 = null;
PlayerNames.Player4 = null;
SceneManager.LoadScene(1);
GameObjectToDeactivate.SetActive(true);
}
}
    public void CloseButton()
    {
        SceneManager.LoadScene(0);
        PlayerNames.Player1 = null;
        PlayerNames.Player2 = null;
        PlayerNames.Player3 = null;
        PlayerNames.Player4 = null;
        SceneManager.LoadScene(1);
        GameObjectToDeactivate.SetActive(false);
    }



}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchBoard : MonoBehaviour
{
public GameObject PlayersNeededPopup;
public void playerCount()
{
if (PlayerNames.Player1 != null && PlayerNames.Player2 != null && PlayerNames.Player3 != null && PlayerNames.Player4 != null){
SceneManager.LoadScene(0);
}
else {
PlayersNeededPopup.SetActive(true);
}

}
    public void CloseButton()
    {
        PlayersNeededPopup.SetActive(false);
        SceneManager.LoadScene(2);
        PlayerNames.Player1 = null;
        PlayerNames.Player2 = null;
        PlayerNames.Player3 = null;
        PlayerNames.Player4 = null;
        SceneManager.LoadScene(1);
    }


}

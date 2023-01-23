using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchBoard : MonoBehaviour
{
public void playerCount()
{
if (PlayerNames.Player1 != null && PlayerNames.Player2 != null && PlayerNames.Player3 != null && PlayerNames.Player4 != null){
SceneManager.LoadScene(0);
}
<<<<<<< HEAD
else {
SceneManager.LoadScene(2);
PlayerNames.Player1 = null;
PlayerNames.Player2 = null;
PlayerNames.Player3 = null;
PlayerNames.Player4 = null;
SceneManager.LoadScene(1);
GameObjectToDeactivate.SetActive(!GameObjectToDeactivate.activeInHierarchy);
}
=======
else SceneManager.LoadScene(2);
>>>>>>> parent of d10723c (bord veranderd)
}
[SerializeField] private string board = "Board";
    public void CloseButton()
    {
<<<<<<< HEAD
        GameObjectToDeactivate.SetActive(false);
=======
        SceneManager.LoadScene(2);
        PlayerNames.Player1 = null;
        PlayerNames.Player2 = null;
        PlayerNames.Player3 = null;
        PlayerNames.Player4 = null;
        SceneManager.LoadScene(board);
>>>>>>> parent of d10723c (bord veranderd)
    }


}

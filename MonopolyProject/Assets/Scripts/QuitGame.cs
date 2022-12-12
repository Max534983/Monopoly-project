using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Quits game when users presses quit button.

public class QuitGame : MonoBehaviour
{
    public void Quit()
    {
        Application.Quit();
        UnityEditor.EditorApplication.isPlaying = false;
    }
}

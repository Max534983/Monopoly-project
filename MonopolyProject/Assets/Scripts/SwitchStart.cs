    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchStart : MonoBehaviour
{
    public void BackButton()
    {
        Debug.Log("asas");

        SceneManager.LoadScene(2);
    }
}

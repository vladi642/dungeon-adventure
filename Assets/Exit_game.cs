using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Exit_game : MonoBehaviour
{
    public void ExitGame()
    {
        Debug.Log("Exit");
        Application.Quit();
    }
}

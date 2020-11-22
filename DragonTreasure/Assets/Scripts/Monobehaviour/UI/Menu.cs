using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void LoadMenu()
    {
        SceneManager.LoadScene("Main Menu Scene");
    }

    public void LoadLeaderBoard()
    {
        SceneManager.LoadScene("High Score Scene");
    }
}

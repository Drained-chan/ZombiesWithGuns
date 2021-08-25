using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void PlayButton()
    {
        Debug.Log("You're now a player.");
    }

    public void QuitButton()
    {
        Application.Quit();
        Debug.Log("Game's closed.");
    }
}

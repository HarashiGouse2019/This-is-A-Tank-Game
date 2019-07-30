using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public GameManager manager;
    public Navigator navi;

    public void Play()
    {
        manager.gameplayStart = true;
        navi.Navigate("Canvas", "GamePlay");
    }

    public void Options()
    {
        navi.Navigate("MainMenu", "OptionsMenu");
    }

    public void ReturnToMainMenu() {
        navi.Navigate("OptionsMenu", "MainMenu");
    }

    public void Apply()
    {

    }

    public void Quit()
    {
        Debug.Log("Application has quit!!!");
        Application.Quit();
    }
}

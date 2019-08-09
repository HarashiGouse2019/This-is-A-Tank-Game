using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

using static System.Convert;

public class MainMenu : MonoBehaviour
{
    public static MainMenu menu;

    public List<Toggle> toggles;
    public List<Slider> sliders;

    public GameManager manager;
    public Navigator navi;
    public ProceduralGen gen;
    public TMP_InputField pIf;
    public Camera canvasCamera;
    
    public bool fScreenEnabled = false;
    public bool motd = false;
    public int seedVal;
    public float musicVal = 1;
    public float soundVal = 1;

    private void Start()
    {
        menu = this;
        AudioManager.manager.Play("BackgroundMusic");
        LoadPref();
    }

    public void Play(string playerMode)
    {
        switch (playerMode)
        {
            case "Single":
                manager.playermode = GameManager.PlayerMode.SinglePlayer;
                break;
            case "Multi":
                manager.playermode = GameManager.PlayerMode.Multiplayer;
                break;
        }
        canvasCamera.enabled = false;
        AudioManager.manager.Stop("BackgroundMusic");
        manager.gameplayStart = true;
        navi.Navigate("MainMenu", "GamePlay");
    }

    public void Options()
    {
        navi.Navigate("MainMenu", "OptionsMenu");
        
    }

    public void ReturnToMainMenu() {
        navi.Navigate("OptionsMenu", "MainMenu");
        LoadPref();
    }

    public void Apply()
    {
        //Apply any possible changes
        gen.mapOfTheDay = motd;
        gen.genSeed = seedVal;
        AudioManager.manager.musicVolumeAdjust.value = musicVal;
        AudioManager.manager.soundVolumeAdjust.value = soundVal;

        PlayerPrefs.SetInt("Seed", seedVal);
        PlayerPrefs.SetInt("MOTD", (motd ? 1 : 0));
        PlayerPrefs.SetFloat("MusicVolume", musicVal);
        PlayerPrefs.SetFloat("SoundVolume", soundVal);
        PlayerPrefs.Save();
        
    }

    public void UpdateSeedValue()
    {
        seedVal = ToInt32(pIf.text);
    }

    public void UpdateMusicVal()
    {
        musicVal = AudioManager.manager.musicVolumeAdjust.value;

        //Put all sounds that are music below
        AudioManager.manager.Volume("BackgroundMusic", musicVal);

    }

    public void UpdateSoundVal()
    {
        soundVal = AudioManager.manager.soundVolumeAdjust.value;

        //Put all sounds that are effects below
        AudioManager.manager.Volume("FireSound", soundVal);
        AudioManager.manager.Volume("TankEngineSound", soundVal);
    }

    public void ToggleFullScreen()
    {
        Toggle fSToggle = FindToggle("FullscreenToggle");
        fScreenEnabled = fSToggle.isOn;
    }

    public void ToggleMapOfTheDay()
    {
        Toggle motdToggle = FindToggle("MapOfTheDayToggle");
        motd = motdToggle.isOn;
    }

    public void Quit()
    {
        Debug.Log("Application has quit!!!");
        Application.Quit();
    }

    public Toggle FindToggle(string name)
    {
        for (int i = 0; i < toggles.Count; i++)
        {
            if (toggles[i].name == name)
            {
                return toggles[i];
            }
        }
        throw new System.NullReferenceException();
    }

    void LoadPref()
    {
        seedVal = ToInt32(PlayerPrefs.GetInt("Seed"));
        motd = ToBoolean(PlayerPrefs.GetInt("MOTD"));
        musicVal = PlayerPrefs.GetFloat("MusicVolume");
        soundVal = PlayerPrefs.GetFloat("SoundVolume");
        Apply();
    }
}

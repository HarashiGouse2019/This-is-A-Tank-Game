using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ReadPrefs : MonoBehaviour
{
    public TextMeshProUGUI genSeedText;
    public Toggle motdToggle;

    //Read values from Player Prefs, showing previously saved values
    void Start()
    {
        genSeedText.text = MainMenu.menu.seedVal.ToString();
        motdToggle.isOn = MainMenu.menu.motd;
    }
}

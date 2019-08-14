using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DisplayScore : MonoBehaviour
{
    public TextMeshProUGUI resultText;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.parentUi.enabled = false;

        //I did this so the program can be grammatically correct
        if (GameManager.instance.enemyKilled > 1)
            resultText.text = "You've eliminated: " + GameManager.instance.enemyKilled + " enemy tanks!!!! (U w U)";
        else if (GameManager.instance.enemyKilled == 1)
            resultText.text = "You've eliminated: 1 enemy tank!!!!";
        else
            resultText.text = "You've eliminated ZEROOOOOOOOOOO enemy tanks!!!!";
    }
}

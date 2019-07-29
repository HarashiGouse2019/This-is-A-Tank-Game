using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class SaveData
{
    public int xp;
    public int gold;
    public string playerName;
}


public class SaveTest : MonoBehaviour
{
    public InputField inputBox;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnLoadButton()
    {
        inputBox.text = PlayerPrefs.GetString("TestValue");   
    }
}

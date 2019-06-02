using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public List<InputController> players;
    public List<TankData> tanks;
    public int bulletInstance;

    //Data
    public float playerHealth, playerMaxHealth;
    public float enemyHealth, enemyMaxHealth;

    public float playerDamage;
    public float enemyDamage;

    //GUI
    public Image healthUI;

    // Start is called before the first frame update
    void Start()
    {
        //Singleton implementation
        if (instance == null)
        {
            instance = this; //instance is referening the current script
            DontDestroyOnLoad(this); //Do not destroy the gameobject when moving to a new scene, or if a scene is loading
        } else
        {
            Destroy(gameObject); //Destroy any other extra GameManagers that should be in the hierarchy.
        }
        playerHealth = playerMaxHealth; //playerHealth starts off with the max health set in the inspector
        enemyHealth = enemyMaxHealth; //enemyHealth starts off with the max health set in the inspector
        healthUI.fillAmount = playerHealth / playerMaxHealth; //The fill amount has a value between 0 and 1. Dividing the current player health by the max Health will make sure that the value is in the 0-1 range.
    }

    private void Update()
    {
        healthUI.fillAmount = playerHealth; //The fill amount will equal to the player health (which will be a decmial values between 0 and 1)
    }

    //Lost Health
    public void LoseHealth(float amount, float entityValue)
    {
        //If player health is not set to zero, decrease the health by a certain amount
        if(playerHealth != 0)
        {
            healthUI.fillAmount -= amount / entityValue;
            playerHealth = healthUI.fillAmount;
            
        }
    }
    public void GainHealth(float amount, float entityValue)
    {
        //If player health is smaller than the max, decrease the health by a certain amount
        if (playerHealth < playerMaxHealth)
        {
            healthUI.fillAmount += amount / entityValue;
            playerHealth = healthUI.fillAmount;
        }
    }
}

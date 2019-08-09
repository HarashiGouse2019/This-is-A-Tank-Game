using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public List<InputController> players;
    public List<TankData> tanks;

    //Reference to Procedural Generation
    public ProceduralGen progen;

    public int bulletInstance;

    //Data
    public float playerHealth, playerMaxHealth;
    public float enemyHealth, enemyMaxHealth;

    public float playerDamage;
    public float enemyDamage;

    //Gameplay
    public bool gameplayStart = false;

    //GUI
    public Canvas parentUi;
    public Image healthUI;

    //Waypoints
    public List<Transform> waypoints;

    //Player Respawn points
    public List<Transform> respawnPoint;

    //Create a enumerator to see if you are one player or 2 player
    public enum PlayerMode
    {
        SinglePlayer,
        Multiplayer
    }

    //Cameras
    public List<Camera> playerCam;

    public PlayerMode playermode;

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

       
        //FindWaypoints();
    }

    private void Update()
    {
        healthUI.fillAmount = playerHealth; //The fill amount will equal to the player health (which will be a decmial values between 0 and 1)

        //Check if the player has reach the play room
        if (IsPlaying())
        {
            progen.enabled = true;
            parentUi.gameObject.SetActive(true);
        }
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

    public void SpawnOnSpot(InputController player, Vector3 spawnPosition) {
        Instantiate(player.gameObject);
        player.gameObject.transform.position = spawnPosition;
    }

    public bool IsPlaying(PlayerMode mode = PlayerMode.SinglePlayer)
    {
        switch (gameplayStart)
        {
            case true:
                switch (playermode)
                {
                    case PlayerMode.SinglePlayer:
                        //Change player 1 camera to normal size
                        playerCam[0].rect = new Rect(0, 0, 1, 1);
                        parentUi.gameObject.SetActive(true);
                        break;
                    case PlayerMode.Multiplayer:
                        //Split player 1 camera to fit player 2 camera
                        playerCam[0].rect = new Rect(0, 0, 0.49f, 1);
                        parentUi.gameObject.SetActive(true);
                        break;
                    default:
                        //Default stuff
                        break;
                }
                return true;
            default:
                parentUi.gameObject.SetActive(false);
                return false;
        }
    }
}

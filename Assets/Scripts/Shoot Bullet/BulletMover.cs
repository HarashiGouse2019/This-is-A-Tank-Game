using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMover : MonoBehaviour
{
    public float bulletSpeed; //Bullet speed
    public Transform tf; //The transform of the bullet
    public Shoot shoot; //Our shoot script
    public Timer timer; //The timer script
    public TankData data; // Our tank data script
    public bool enemySide; //If the bullet is coming from you, or the enemy
    float destroyDuration; //How long until the bullet is destroyed
    private void Awake()
    {
        //Iterator through all gameobjects, and find these components
        shoot = FindObjectOfType<Shoot>(); 
        timer = FindObjectOfType<Timer>(); 
        data = FindObjectOfType<TankData>();
        

    }
    private void Start()
    {
        destroyDuration = data.shotsPerSecond; //Destroy duration will be based on how long it takes for us to shoot again
    }
    private void Update()
    {
        DestroyDuration(destroyDuration); //ONce this object is created, it'll destroy at a certain amount of time
        Move(); //Propels the bullet
    }
    public void Move()
    {
        tf.Translate(Vector3.forward * bulletSpeed * Time.deltaTime); //Translate the bullet foward
    }
    public void DestroyDuration(float seconds)
    {
        timer.StartTimer(); //Start our timer
        if (timer.currentTime > seconds) //If the timer's time is greater than our destroy duration
        {
            timer.ResetTime(false); //Reset the time
            Destroy(gameObject); //Destory this object
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (enemySide)
        {
            //If on enemy side, check if the gameObject name is "SD_Tiger-I"
            //Otherwise, check if the bullet hits walls, or the enemy
            case false:
                if (other.gameObject.tag == "Obstacle" || other.gameObject.tag == "Enemy")
                {
                    Destroy(gameObject);
                }
                break;
            case true:
                if (other.gameObject.name == "SD_Tiger-I")
                    GameManager.instance.LoseHealth(10f, GameManager.instance.playerMaxHealth); //We lose 10 health
                break;
        }
    }
}

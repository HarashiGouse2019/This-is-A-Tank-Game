using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMover : MonoBehaviour
{
    public float bulletSpeed;
    public Transform tf;
    public Shoot shoot;
    public Timer timer;
    public TankData data;
    float destroyDuration;
    private void Awake()
    {
        shoot = FindObjectOfType<Shoot>();
        timer = FindObjectOfType<Timer>();
        data = FindObjectOfType<TankData>();
        
        GameManager.instance.bulletInstance++;
    }
    private void Start()
    {
        destroyDuration = data.shotsPerSecond;
    }
    private void Update()
    {
        DestroyDuration(destroyDuration);
        Move();
    }
    public void Move()
    {
        tf.Translate(Vector3.forward * bulletSpeed * Time.deltaTime);
    }
    public void DestroyDuration(float seconds)
    {
        timer.StartTimer();
        if (timer.currentTime > seconds)
        {
            timer.ResetTime();
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Obstacle")
        {
            Destroy(gameObject);
        }
    }
    void OnDestroy()
    {
        GameManager.instance.bulletInstance--;
    }
}

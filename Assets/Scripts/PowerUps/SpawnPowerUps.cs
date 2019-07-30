using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnPowerUps : MonoBehaviour
{
    public List<GameObject> powerups;
    public int instanceCount = 0;

    private Timer timer;

    private void Update()
    {
        timer = GetComponent<Timer>();
        timer.StartTimer(0);
        if (timer.currentTime[0] > 5)
        {
            if (instanceCount < 1) SpawnPowerUp();
        }
    }

    public void SpawnPowerUp()
    {
        int i = Random.Range(0, powerups.Count);
        GameObject powerUp = Instantiate(powerups[i]); instanceCount++;
        powerUp.transform.position = transform.position;
    }

}

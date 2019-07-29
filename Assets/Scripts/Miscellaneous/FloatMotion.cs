using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatMotion : MonoBehaviour
{
    const float min = 0, max = 1; //Our min and max value for our hightest and lowest points.
    public float[] levRange = { min, max };

    //Other variables
    public float speed = 0;
    public float increment = 0;
    public float duration = 0;

    float direction = 0f;

    //Grab our transform
    Transform powerUpBall;

    //And grab a timer
    Timer timer = new Timer();

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Levitate(speed, increment, 1);
    }

    float SwitchDirection()
    {
        float dir = 0f;
        timer.StartTimer(0);
        if (timer.currentTime[0] >= duration)
        {
            dir *= -1;
            timer.ResetTime(0, false);
        }
        return dir;
    }

    void Levitate(float speed, float increment, float dir)
    {
        speed += increment;
        powerUpBall = GetComponent<FloatMotion>().transform;
        powerUpBall.Translate(new Vector3(0, dir * speed, 0), Space.Self);
    }
}

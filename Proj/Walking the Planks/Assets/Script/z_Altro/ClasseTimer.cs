using System;
using UnityEngine;

public class MyTimer
{
    private float _maxTime;
    private float _timeElapsed;
    public event Action OnTimerDone;

    public
    float maxTime
    {
        get => _maxTime;
        set { _maxTime = value; }
    }

    public float timeElapsed
    {
        get => _timeElapsed;
    }

    /// <summary>
    /// This function checks if the timer is done,
    /// <br></br>if it's done, invokes OnTimerDone and resets the time on th timer,
    /// <br></br>if it's still counting down, adds Time.deltaTime (of Unity)
    /// </summary>
    public void AddTimeToTimer()
    {
        if (_timeElapsed >= maxTime)
        {
            OnTimerDone.Invoke();

            _timeElapsed = 0;     //Reset the timer
        }
        else
        {
            _timeElapsed += Time.deltaTime;   //Increases the elapsed time
        }
    }
    /// <summary>
    /// This function checks if the timer is done,
    /// <br></br>if it's done, invokes OnTimerDone and resets the time on the timer,
    /// <br></br>if it's still counting down, adds <b><i>timeToAdd</i></b>
    /// </summary>
    /// <param name="timeToAdd">Other time to add</param>
    public void AddTimeToTimer(float timeToAdd)
    {
        if (_timeElapsed >= maxTime)
        {
            OnTimerDone.Invoke();

            _timeElapsed = 0;     //Reset the timer
        }
        else
        {
            _timeElapsed += timeToAdd;   //Increases the elapsed time
        }
    }
}

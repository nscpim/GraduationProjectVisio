using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer
{
    private float timeStamp;
    private float interval;
    private float pauseDifference;
    public int uniqueID;
    public GameObject gameObject;
    public string name; 

    public bool isPaused { get; private set; }
    public bool isActive { get; private set; }

    /// <summary>
    /// Set a UniqueID so the timer is accessable later on
    /// Optional: gameObject can be filled in to pass the gameobject this timer is being ran on or accosiated to
    /// </summary>
    /// <param name="uniqueID"></param>
    /// <param name="gameObject"></param>
    public Timer(int uniqueID, string name, GameObject gameObject = null)
    {
        this.uniqueID = uniqueID;
        this.gameObject = gameObject;
        this.name = name;
        GameManager.instance.timers.Add(this);
    }

    /// <summary>
    /// Return time left on the timer
    /// </summary>
    /// <returns></returns>
    public float TimeLeft()
    {
        return TimerDone() ? 0 : (1 - TimerProgress()) * interval;
    }

    /// <summary>
    /// return the progress of the timer
    /// </summary>
    /// <returns></returns>
    public float TimerProgress()
    {
        return (isPaused) ? (interval - pauseDifference / interval) : TimerDone() == true ? 1 : Mathf.Abs((timeStamp - Time.time) / interval);
    }

    /// <summary>
    /// Return if the timer is done or not
    /// </summary>
    /// <returns></returns>
    public bool TimerDone()
    {
        return (isPaused) ? pauseDifference == 0.0f : Time.time >= timeStamp + interval ? true : false;
    }
    /// <summary>
    /// Sets a timer with a given time, defaults to 2 seconds
    /// </summary>
    /// <param name="_interval"></param>
    public void SetTimer(float _interval = 2)
    {
        timeStamp = Time.time;
        interval = _interval;
        isActive = true;
    }

    /// <summary>
    /// Restarts the timer with the same variable values
    /// </summary>
    public void RestartTimer()
    {
        SetTimer(interval);
    }

    /// <summary>
    /// Stops the timer
    /// </summary>
    public void StopTimer()
    {
        isActive = false;
        timeStamp = interval;
    }

    /// <summary>
    /// Pauses the timer
    /// </summary>
    /// <param name="pause"></param>
    public void PauseTimer(bool pause)
    {
        if (pause)
        {
            pauseDifference = TimeLeft();
            isPaused = pause;
            return;
        }
        isPaused = pause;
        timeStamp = Time.time - (interval - pauseDifference);
    }

}
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
    /// Set a UniqueID so the timer is accessable later on.
    /// Optional: gameObject can be filled in to pass the gameobject this timer is being ran on or accosiated to.
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

    public float TimeLeft()
    {
        return TimerDone() ? 0 : (1 - TimerProgress()) * interval;
    }

    public float TimerProgress()
    {
        return (isPaused) ? (interval - pauseDifference / interval) : TimerDone() == true ? 1 : Mathf.Abs((timeStamp - Time.time) / interval);
    }

    public bool TimerDone()
    {
        return (isPaused) ? pauseDifference == 0.0f : Time.time >= timeStamp + interval ? true : false;
    }

    public void SetTimer(float _interval = 2)
    {
        timeStamp = Time.time;
        interval = _interval;
        isActive = true;
    }

    public void RestartTimer()
    {
        SetTimer(interval);
    }

    public void StopTimer()
    {
        isActive = false;
        timeStamp = interval;
    }

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
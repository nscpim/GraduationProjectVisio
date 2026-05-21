using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class AudioManager : Manager
{
    public float generalVolume { get; private set; }
    public float TTSVolume { get; private set; }

    public List<AudioSource> audioSources = new List<AudioSource>();

    /// <summary>
    /// Adds an audio source to play audio on.
    /// </summary>
    /// <param name="source"></param>
    public void AddAudioSource(AudioSource source) 
    {
        audioSources.Add(source);
    }
    /// <summary>
    /// Text-to-speech audio based on a string message.
    /// </summary>
    /// <param name="text"></param>
    public void Speak(string text)
    {
        if (GameManager.GetManager<AndroidTTS>() == null)
        {
            return;
        }
            GameManager.GetManager<AndroidTTS>().Speak(text);
    }

    /// <summary>
    /// Play audio when correct combination is being done.
    /// </summary>
    /// <param name="message"></param>
    public void PlayCorrect(string message)
    {
        Speak(message);
    }

    /// <summary>
    /// Play audio when combination is incorrect.
    /// </summary>
    /// <param name="message"></param>
    public void PlayIncorrect(string message)
    {
        Speak(message);
    }

    /// <summary>
    /// Volume higher
    /// </summary>
    public void VolumeUp()
    {
        generalVolume += 10;
        if (generalVolume > 100)
        {
            generalVolume = 100;
        }
        SetVolume(generalVolume);
    }
    /// <summary>
    /// Volume lower
    /// </summary>
    public void VolumeDown()
    {
        generalVolume -= 10;
        if (generalVolume < 0)
        {
            generalVolume = 0;
        }
        SetVolume(generalVolume);
    }

    /// <summary>
    /// Sets the volume with a float value of 0-100
    /// </summary>
    /// <param name="volume"></param>
    public void SetVolume(float volume = 20f)
    {
        generalVolume = volume / 100;
        Debug.Log(generalVolume);
        for (int i = 0; i < audioSources.Count; i++)
        {
            audioSources[i].volume = generalVolume;
        }
    }

}
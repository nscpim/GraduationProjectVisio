using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class AudioManager : Manager
{
    public float generalVolume { get; private set; }
    public float TTSVolume { get; private set; }

    public List<AudioSource> audioSources = new List<AudioSource>();


    public void AddAudioSource(AudioSource source) 
    {
        audioSources.Add(source);
    }

    public void Speak(string text)
    {
        if (GameManager.GetManager<AndroidTTS>().tts != null)
        {
            GameManager.GetManager<AndroidTTS>().Speak(text);
        }
    }

    public void PlayCorrect(string message)
    {
        Speak(message);
    }

    public void PlayIncorrect(string message)
    {
        Speak(message);
    }

    public void VolumeUp()
    {
        generalVolume += 10;
        if (generalVolume > 100)
        {
            generalVolume = 100;
        }
        SetVolume(generalVolume);
    }

    public void VolumeDown()
    {
        generalVolume -= 10;
        if (generalVolume < 0)
        {
            generalVolume = 0;
        }
        SetVolume(generalVolume);
    }


    public void SetVolume(float volume = 20f)
    {
        generalVolume = volume;
        Debug.Log(generalVolume);
        for (int i = 0; i < audioSources.Count; i++)
        {
            audioSources[i].volume = generalVolume;
        }
    }

}
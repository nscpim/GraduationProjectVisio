using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }

    private static Manager[] managers;

    [Header("User Interface")]
    public TextMeshProUGUI displayKeyText;
    [Space(10)]
    [Header("LessonManager")]
    public List<LessonData> lessons;
    [Space(10)]
    [Header("AudioManager")]
    public AudioSource voiceSource;
    public AudioSource feedbackSource;
    [Space(10)]
    [Header("Timers")]
    public List<Timer> timers;

    GameManager()
    {
        if (instance == null)
        {
            instance = this;
        }

        managers = new Manager[]
        {
         new AudioManager(),
         new LessonManager(),
         new KeyboardInputManager(),
         new UIManager(),
         new SaveLoadManager(),
        };
    }

    public void Awake()
    {
        for (int i = 0; i < managers.Length; i++)
        {
            managers[i].Awake();
        }
    }

    public static T GetManager<T>() where T : Manager
    {
        for (int i = 0; i < managers.Length; i++)
        {
            if (typeof(T) == managers[i].GetType())
            {
                return (T)managers[i];
            }
        }
        return default(T);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Start()
    {
        for (int i = 0; i < managers.Length; i++)
        {
            managers[i].Start();
        }

        GetManager<LessonManager>().InitializeLesson(lessons[0]);
    }

    // Update is called once per frame
    public void Update()
    {
        for (int i = 0; i < managers.Length; i++)
        {
            managers[i].Update();
        }
    }

    public Timer GetTimerByName(string name)
    {
        for (int i = 0; i < timers.Count; i++)
        {
            if (timers[i].name == name)
            {
                return timers[i];
            }
        }
        Debug.LogError($"No Timer was found with name: {name}");
        return null;
    }
}

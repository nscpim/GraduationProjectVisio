using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }

    private static Manager[] managers;

    public List<LessonData> lessons;

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
}

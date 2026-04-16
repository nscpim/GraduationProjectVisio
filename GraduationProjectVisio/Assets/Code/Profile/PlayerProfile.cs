using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class PlayerProfile
{
    public string profileID;
    public string profileName;

    public long createdAt;

    public bool onBoarding = false;

    public List<string> completedLessons = new List<string>();

    public int totalLessonsCompleted;

    public List<LessonProgress> lessonProgress = new List<LessonProgress>();

}
[System.Serializable]
public class LessonProgress
{
    public string lessonID;
    public bool completed;
    public int score;
}

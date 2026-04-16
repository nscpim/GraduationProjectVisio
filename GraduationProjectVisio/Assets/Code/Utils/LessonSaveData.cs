using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LessonSaveData
{
    public string id;
    public string lessonName;
    public List<TypingStep> steps;
}
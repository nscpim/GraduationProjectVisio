using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LessonData", menuName = "Scriptable Objects/LessonData")]
public class LessonData : ScriptableObject
{
    public string id;
    public string lessonName;
    public List<TypingStep> steps;
}

[System.Serializable]
public class TypingStep
{
    public string instructionText;
    public KeyCode targetKey; //The main key 
    public List<KeyCode> requiredKeys; //For combinations
    public string instructionIfWrong;
}

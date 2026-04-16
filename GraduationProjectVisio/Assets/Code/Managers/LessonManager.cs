using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class LessonManager : Manager
{

    public LessonData currentLesson;

    private int currentStepIndex = 0;


    public void InitializeLesson(LessonData lesson)
    {
        currentLesson = lesson;
        currentStepIndex = 0;
        AnnounceCurrentStep();
        UpdateUI();
    }

    public void SetLesson(string name)
    {
        LessonData lesson = GetLesson(name);
        currentLesson = lesson;
        currentStepIndex = 0;
        AnnounceCurrentStep();
        UpdateUI();
    }

    public LessonData GetLesson(string name)
    {
        for (int i = 0; i < GameManager.instance.lessons.Count; i++)
        {
            if (GameManager.instance.lessons[i].name == name)
            {
                return GameManager.instance.lessons[i];
            }
        }
        Debug.Log($"Lesson with name {name} could not be found");
        return null;
    }


    public void UpdateUI()
    {
        GameManager.GetManager<UIManager>().DisplayUI(currentLesson.steps[currentStepIndex].targetKey, GameManager.instance.displayKeyText, Color.orange,
              currentLesson.steps[currentStepIndex].requiredKeys);
        GameManager.GetManager<UIManager>().DisplayText(currentLesson.steps[currentStepIndex].instructionText, GameManager.instance.descriptionText, Color.green);
    }

    public void ReceiveInput(KeyCode key)
    {
        TypingStep step = currentLesson.steps[currentStepIndex];


        if (key == step.targetKey)
        {
            bool allKeysHeld = true;
            foreach (KeyCode requiredKey in step.requiredKeys)
            {

                Debug.Log(requiredKey);
                if (!Input.GetKey(requiredKey))
                {
                    allKeysHeld = false;
                    break;
                }
            }
            if (allKeysHeld)
            {
                GameManager.GetManager<AudioManager>().PlayCorrect();
                currentStepIndex++;
                AnnounceCurrentStep();
                UpdateUI();
            }
            else
            {
                GameManager.GetManager<AudioManager>().PlayIncorrect();
                return;
            }

        }
    }

    private void AnnounceCurrentStep()
    {
        if (currentStepIndex >= currentLesson.steps.Count)
        {
            CompleteLesson(currentLesson.id);
            return;
        }
        GameManager.GetManager<AudioManager>().Speak(currentLesson.steps[currentStepIndex].instructionText);


    }

    private void CompleteLesson(string lessonID)
    {
        GameManager.GetManager<AudioManager>().Speak("Les afgerond.");

        var profile = GameManager.GetManager<SaveLoadManager>().currentProfile;
        if (!profile.completedLessons.Contains(currentLesson.id))
        {
            profile.totalLessonsCompleted++;
            profile.completedLessons.Add(currentLesson.id);
        }

        var progress = profile.lessonProgress.Find(l => l.lessonID == lessonID);

        if (progress == null)
        {
            progress = new LessonProgress
            {
                lessonID = lessonID,
                completed = true,
                score = 100
            };
            profile.lessonProgress.Add(progress);
        }
        else
        {
            progress.completed = true;
        }

        GameManager.GetManager<SaveLoadManager>().SaveCurrentProfile();
    }
}

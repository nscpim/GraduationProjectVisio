using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class LessonManager : Manager, IDataInterface
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


    public void UpdateUI()
    {
        GameManager.GetManager<UIManager>().DisplayUI(currentLesson.steps[currentStepIndex].targetKey, GameManager.instance.displayKeyText, Color.orange,
              currentLesson.steps[currentStepIndex].requiredKeys);
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
            CompleteLesson();
            return;
        }
        GameManager.GetManager<AudioManager>().Speak(currentLesson.steps[currentStepIndex].instructionText);


    }

    private void CompleteLesson()
    {
        GameManager.GetManager<AudioManager>().Speak("Les afgerond.");

    }

    public void LoadData(SaveFile data)
    {
        this.currentLesson = data.lesson;
    }

    public void SaveData(ref SaveFile data)
    {
        data.lesson = this.currentLesson;
    }
}

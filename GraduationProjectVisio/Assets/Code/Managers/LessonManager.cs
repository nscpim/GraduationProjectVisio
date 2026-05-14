using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

public class LessonManager : Manager
{
    public LessonData currentLesson;
    private bool stepCompleted = false;

    private int currentStepIndex = 0;

    private Timer keyPressTimer = new Timer(1, "keyPressTimer");

    private Color combinationColor;
    private Color descriptionColor;
    private Color newColor;

    private List<String> correctAnswer = new List<string>
    {
        "Goed gedaan!",
        "Ga zo door!",
        "Geweldig",
    };


    public override void Start()
    {
        ColorUtility.TryParseHtmlString("#FFD600", out combinationColor);
        ColorUtility.TryParseHtmlString("#00AEEF", out descriptionColor);
        ColorUtility.TryParseHtmlString("#000000", out newColor);

    }

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

    public override void Update()
    {
        ReceiveInput();
        if (currentLesson != null)
        {
            TypingStep step; 
            try
            {
                step = currentLesson.steps[currentStepIndex];
            }
            catch (IndexOutOfRangeException e)
            {
                Debug.Log(e.Message);
                throw;
            }

            // Create valid key set adding the main key
            HashSet<KeyCode> validKeys = new HashSet<KeyCode>
        {
            step.targetKey
        };

            //Adding all additional keys
            foreach (var key in step.requiredKeys)
            {
                validKeys.Add(key);
            }

            // Check all keyboard keys
            foreach (var pair in GameManager.GetManager<VisualKeyboardManager>().keyMap)
            {
                if (Input.GetKey(pair.Key))
                {
                    // Correct combo key
                    if (validKeys.Contains(pair.Key))
                    {
                        pair.Value.SetPressed();
                    }
                    // Wrong key
                    else
                    {
                        pair.Value.SetIncorrect();
                    }
                }
            }
        }
    }
    public void UpdateUI()
    {
        GameManager.GetManager<UIManager>().DisplayUI(currentLesson.steps[currentStepIndex].targetKey, GameManager.instance.displayKeyText, newColor,
              currentLesson.steps[currentStepIndex].requiredKeys);
        GameManager.GetManager<UIManager>().DisplayText(currentLesson.steps[currentStepIndex].instructionText, GameManager.instance.descriptionText, descriptionColor);
        ShowStep(currentLesson.steps[currentStepIndex]);
    }

    public void ShowStep(TypingStep step)
    {
        ResetAllKeys();

        // Highlight required keys
        foreach (var key in step.requiredKeys)
        {
            if (GameManager.GetManager<VisualKeyboardManager>().keyMap.ContainsKey(key))
            {
                GameManager.GetManager<VisualKeyboardManager>().keyMap[key].SetRequired();
            }
        }

        // Highlight target key
        if (GameManager.GetManager<VisualKeyboardManager>().keyMap.ContainsKey(step.targetKey))
        {
            GameManager.GetManager<VisualKeyboardManager>().keyMap[step.targetKey].SetRequired();
        }
        Debug.Log("Highlighted all keys");
    }

    public void ResetAllKeys()
    {
        foreach (var key in GameManager.GetManager<VisualKeyboardManager>().keyMap.Keys)
        {
            GameManager.GetManager<VisualKeyboardManager>().keyMap[key].SetNormal();
        }
    }

    public void ReceiveInput()
    {
        if (currentLesson != null)
        {
            Debug.Log(currentLesson.lessonName);
            if (stepCompleted) return;

            TypingStep step = currentLesson.steps[currentStepIndex];

            bool allKeysPressed = true;

            // Required keys
            foreach (KeyCode key in step.requiredKeys)
            {
                if (!Input.GetKey(key))
                {
                    allKeysPressed = false;
                    break;
                }
            }

            // Target key
            if (!Input.GetKey(step.targetKey))
            {
                allKeysPressed = false;
            }

            // Success
            if (allKeysPressed)
            {
                stepCompleted = true;
                int correctStringIndex = UnityEngine.Random.Range(0, correctAnswer.Count);
                GameManager.GetManager<AudioManager>().PlayCorrect(correctAnswer[correctStringIndex]);
                currentStepIndex++;
                PrepareNextStep();

            }
        }
    }
    private async void PrepareNextStep()
    {
        await Task.Delay(50);
        stepCompleted = false;

        AnnounceCurrentStep();
        if (currentLesson != null)
        {
            UpdateUI();
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
        if (profile == null)
        {
            GameManager.instance.SelectProfileButton.Select();
            GameManager.GetManager<UIManager>().CloseAllPanels();
            return;
        }

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
        GameManager.instance.SelectProfileButton.Select();
        Debug.LogWarning("Completed the lesson");
        GameManager.GetManager<SaveLoadManager>().SaveCurrentProfile();
        GameManager.GetManager<UIManager>().CloseAllPanels();

    }
}

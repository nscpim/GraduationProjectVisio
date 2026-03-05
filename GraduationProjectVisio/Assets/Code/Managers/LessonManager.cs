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
    }

    public void ReceiveInput(KeyCode key, bool shiftHeld)
    {
        TypingStep step = currentLesson.steps[currentStepIndex];

        if (key == step.targetKey && shiftHeld == step.requiresShift)
        {
            GameManager.GetManager<AudioManager>().PlayCorrect();
            currentStepIndex++;
            AnnounceCurrentStep();
        }
        else
        {
            GameManager.GetManager<AudioManager>().PlayIncorrect();
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
 


}

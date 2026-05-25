using System.Collections.Generic;
using UnityEngine;

public static class LessonConverter 
{
    /// <summary>
    /// Converts LessonData to a useable ScriptableObject
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    public static LessonData ToScriptableObject(LessonSaveData data)
    {
        LessonData so = ScriptableObject.CreateInstance<LessonData>();

        foreach (var saveStep in data.steps)
        {
            so.steps = new List<TypingStep>();

            TypingStep step = new TypingStep();

            step.instructionText = saveStep.instructionText;

            step.instructionIfWrong = saveStep.instructionIfWrong;

            step.targetKey = (KeyCode)saveStep.targetKey;

            step.requiredKeys = new List<KeyCode>();

            foreach (var key in saveStep.requiredKeys)
            {
                step.requiredKeys.Add((KeyCode)key);
            }

            so.steps.Add(step);
        }
        so.id = data.id;
        so.lessonName = data.lessonName;
        so.name = data.lessonName;
        return so;
    }
}

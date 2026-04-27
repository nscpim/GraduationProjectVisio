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
        so.steps = data.steps;
        so.id = data.id;
        so.lessonName = data.lessonName;
        so.name = data.lessonName;
        return so;
    }
}

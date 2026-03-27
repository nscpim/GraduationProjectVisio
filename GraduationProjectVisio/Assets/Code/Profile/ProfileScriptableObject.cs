using UnityEngine;


[CreateAssetMenu(fileName = "Profile", menuName = "Scriptable Objects/Profiles")]
public class ProfileScriptableObject : ScriptableObject
{
   
    public ProfileData profileData;
}
[System.Serializable]
public class ProfileData
{
    public string name;
    public int progress;
    public LessonData currentLesson;
    public LessonData[] completedLessons;

   public ProfileData(string name, int progress, LessonData currentLesson, LessonData[] completedLessons) 
    {
        this.name = name;
        this.progress = progress;
        this.currentLesson = currentLesson;
        this.completedLessons = completedLessons;
    }
   
}


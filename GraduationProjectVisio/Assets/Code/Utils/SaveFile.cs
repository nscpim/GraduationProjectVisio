using UnityEngine;


[System.Serializable]
public class SaveFile
{
    public int lessonProgress;
    public string name;
    public LessonData lesson;

    public SaveFile()
    {
        this.name = "";
        this.lessonProgress = 0;
        this.lesson = GameManager.instance.lessons[0];
    }
}

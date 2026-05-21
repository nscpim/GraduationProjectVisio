using System.IO;
using UnityEngine;

public class LessonFileSystem
{
    private static string FolderPath =>  Path.Combine(Application.persistentDataPath, "Lessons");


    /// <summary>
    /// Saves a lesson to disk
    /// </summary>
    /// <param name="lesson"></param>
    public static void SaveLesson(LessonSaveData lesson)
    {
        if (!Directory.Exists(FolderPath))
            Directory.CreateDirectory(FolderPath);

        string json = JsonUtility.ToJson(lesson, true);

        string path = GetPath(lesson.id);

        File.WriteAllText(path, json);
    }

    /// <summary>
    /// Loads a lesson from disk
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public static LessonSaveData LoadLesson(string id)
    {
        string path = GetPath(id);

        if (!File.Exists(path))
            return null;

        string json = File.ReadAllText(path);
        return JsonUtility.FromJson<LessonSaveData>(json);
    }

    /// <summary>
    /// Gets all lesson files currently on disk
    /// </summary>
    /// <returns></returns>
    public static string[] GetAllLessonFiles()
    {
        if (!Directory.Exists(FolderPath))
            return new string[0];

        return Directory.GetFiles(FolderPath, "*.json");
    }

    /// <summary>
    /// Gets the data path of the lessons
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    private static string GetPath(string id)
    {
        return Path.Combine(FolderPath, $"lesson_{id}.json");
    }
}

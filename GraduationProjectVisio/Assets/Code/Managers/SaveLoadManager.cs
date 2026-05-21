using UnityEngine;
using System.IO;
using System.Linq;
using System.Collections.Generic;

public class SaveLoadManager : Manager
{
    public PlayerProfile currentProfile { get; private set; }

    public List<LessonData> loadedLessons = new List<LessonData>();

    /// <summary>
    /// Executed at the start of the application.
    /// </summary>
    public override void Start()
    {
        LoadAllLessons(false);
    }

    /// <summary>
    /// Loads all lessons from the disk using a persistent data path so it works for all operating systems.
    /// If its a reload in runtime it will clear the lessons and then add the customs alongside the prebuild lessons.
    /// </summary>
    /// <param name="reload"></param>
    public void LoadAllLessons(bool reload)
    {
        loadedLessons.Clear();
       
        var files = LessonFileSystem.GetAllLessonFiles();

        foreach (var file in files)
        {
            string json = File.ReadAllText(file);
            LessonSaveData data = JsonUtility.FromJson<LessonSaveData>(json);
            LessonData so = LessonConverter.ToScriptableObject(data);
            loadedLessons.Add(so);
            
        }
        if (reload)
        {
            int runTimeLessons = GameManager.instance.lessons.Count - GameManager.instance.preBuildLessons;
            if (runTimeLessons > 0)
            {
                GameManager.instance.lessons.RemoveRange(GameManager.instance.preBuildLessons, runTimeLessons);
            }
        }
        GameManager.instance.lessons.AddRange(loadedLessons);

    }

    /// <summary>
    /// Creates a new profile with the name as string
    /// </summary>
    /// <param name="profileName"></param>
    public void CreateNewProfile(string profileName)
    {
        PlayerProfile profile = new PlayerProfile
        {
            profileID = System.Guid.NewGuid().ToString(),
            profileName = profileName,
            createdAt = System.DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
        };
        currentProfile = profile;

        ProfileSaveSystem.SaveProfile(profile);
    }

    /// <summary>
    /// Loads a profile with a given name
    /// </summary>
    /// <param name="profileID"></param>
    public void LoadProfile(string profileID)
    {
        currentProfile = ProfileSaveSystem.LoadProfile(profileID);

        if (currentProfile != null)
        {
            SaveCurrentProfile();
        }

    }

    /// <summary>
    /// Saves the current profile
    /// </summary>
    public void SaveCurrentProfile()
    {
        ProfileSaveSystem.SaveProfile(currentProfile);
    }


}

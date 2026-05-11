using UnityEngine;
using System.IO;
using System.Linq;
using System.Collections.Generic;

public class SaveLoadManager : Manager
{
    public PlayerProfile currentProfile { get; private set; }

    public List<LessonData> loadedLessons = new List<LessonData>();

    public override void Start()
    {
        LoadAllLessons(false);
    }

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

    public void LoadProfile(string profileID)
    {
        currentProfile = ProfileSaveSystem.LoadProfile(profileID);

        if (currentProfile != null)
        {
            SaveCurrentProfile();
        }

    }

    public void SaveCurrentProfile()
    {
        ProfileSaveSystem.SaveProfile(currentProfile);
    }


}

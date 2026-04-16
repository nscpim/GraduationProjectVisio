using UnityEngine;
using System.IO;

public static class ProfileSaveSystem
{
    private static string FolderPath =>
       Path.Combine(Application.persistentDataPath, "Profiles");

    public static void SaveProfile(PlayerProfile profile)
    {
        if (!Directory.Exists(FolderPath))
            Directory.CreateDirectory(FolderPath);

        string json = JsonUtility.ToJson(profile, true);

        string path = GetProfilePath(profile.profileID);

        File.WriteAllText(path, json);
    }

    public static PlayerProfile LoadProfile(string profileId)
    {
        string path = GetProfilePath(profileId);

        if (!File.Exists(path))
        {
            Debug.LogError("Profile not found: " + profileId);
            return null;
        }

        string json = File.ReadAllText(path);
        return JsonUtility.FromJson<PlayerProfile>(json);
    }

    public static void DeleteProfile(string profileId)
    {
        string path = GetProfilePath(profileId);

        if (File.Exists(path))
            File.Delete(path);
    }
    private static string GetProfilePath(string profileId)
    {
        return Path.Combine(FolderPath, $"profile_{profileId}.json");
    }
}

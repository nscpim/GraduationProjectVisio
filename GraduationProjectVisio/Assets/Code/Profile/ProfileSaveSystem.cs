using UnityEngine;
using System.IO;

public static class ProfileSaveSystem
{
    private static string FolderPath =>
       Path.Combine(Application.persistentDataPath, "Profiles");

    /// <summary>
    /// Saves profile to disk
    /// </summary>
    /// <param name="profile"></param>
    public static void SaveProfile(PlayerProfile profile)
    {
        if (!Directory.Exists(FolderPath))
            Directory.CreateDirectory(FolderPath);

        string json = JsonUtility.ToJson(profile, true);

        string path = GetProfilePath(profile.profileID);

        File.WriteAllText(path, json);
    }

    /// <summary>
    /// Loads profile from disk
    /// </summary>
    /// <param name="profileId"></param>
    /// <returns></returns>
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

    /// <summary>
    /// Deletes a profile
    /// </summary>
    /// <param name="profileId"></param>
    public static void DeleteProfile(string profileId)
    {
        string path = GetProfilePath(profileId);

        if (File.Exists(path))
            File.Delete(path);
    }
    /// <summary>
    /// Gets the profile path of the stores profiles
    /// </summary>
    /// <param name="profileId"></param>
    /// <returns></returns>
    private static string GetProfilePath(string profileId)
    {
        return Path.Combine(FolderPath, $"profile_{profileId}.json");
    }
}

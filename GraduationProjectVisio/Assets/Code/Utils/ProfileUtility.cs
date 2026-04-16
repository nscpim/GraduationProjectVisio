using UnityEngine;
using System.IO;
using System.Collections.Generic;

public static class ProfileUtility
{
    public static List<string> GetAllProfiles() 
    {
        string folder = Path.Combine(Application.persistentDataPath, "Profiles");
       
        if (!Directory.Exists(folder))
        {
            return new List<string>();
        }

        var files = Directory.GetFiles(folder, "profile_*.json");

        List<string> ids = new List<string>();

        foreach (var file in files)
        {
            string name = Path.GetFileNameWithoutExtension(file);
            string id = name.Replace("profile_", "");
            ids.Add(id);
        }

        return ids;
    }


}
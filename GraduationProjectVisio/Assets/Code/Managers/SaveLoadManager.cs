using UnityEngine;
using System.IO;
using System.Linq;
using System.Collections.Generic;

public class SaveLoadManager : Manager
{
   public PlayerProfile currentProfile { get; private set; }

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

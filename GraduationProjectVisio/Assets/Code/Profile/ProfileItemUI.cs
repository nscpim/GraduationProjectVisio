using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProfileItemUI : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public Button button;

    private string profileId;


    public void Setup(PlayerProfile profile, Action<string> onClick) 
    {
        profileId = profile.profileID;
        nameText.text = profile.profileName;

        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() => onClick?.Invoke(profileId));
        Debug.Log(Application.persistentDataPath);
    }
}

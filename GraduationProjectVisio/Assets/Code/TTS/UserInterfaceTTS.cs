using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class UserInterfaceTTS : MonoBehaviour , ISelectHandler
{
    public string textToSpeak;

    /// <summary>
    ///  Executed whenever you select another button in the UI, uses TTS for the buttons that are then selected. (But this one is for the navigation bar)
    /// </summary>
    /// <param name="eventData"></param>
    public void OnSelect(BaseEventData eventData)
    {
        if (!string.IsNullOrEmpty(textToSpeak))
        {
            GameManager.GetManager<AudioManager>().Speak(textToSpeak);
        }
    }
}

using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class LessonTTS : MonoBehaviour , ISelectHandler
{

    /// <summary>
    /// Executed whenever you select another button in the UI, uses TTS for the buttons that are then selected.
    /// </summary>
    /// <param name="eventData"></param>
    public void OnSelect(BaseEventData eventData)
    {
        string textToSpeak = gameObject.GetComponentInChildren<TextMeshProUGUI>().text;

        if (!string.IsNullOrEmpty(textToSpeak))
        {
            GameManager.GetManager<AudioManager>().Speak(textToSpeak);
        }
    }
}

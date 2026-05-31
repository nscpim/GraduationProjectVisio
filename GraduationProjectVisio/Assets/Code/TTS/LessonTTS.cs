using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.EventSystems;

public class LessonTTS : MonoBehaviour , ISelectHandler
{
    public void OnSelect(BaseEventData eventData)
    {
        string textToSpeak = gameObject.GetComponentInChildren<TextMeshProUGUI>().text;

        if (!string.IsNullOrEmpty(textToSpeak))
        {
            GameManager.GetManager<AudioManager>().Speak(textToSpeak);
        }
    }
}

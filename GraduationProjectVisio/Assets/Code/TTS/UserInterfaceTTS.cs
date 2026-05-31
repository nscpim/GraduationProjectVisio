using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.EventSystems;

public class UserInterfaceTTS : MonoBehaviour , ISelectHandler
{
    public string textToSpeak;
    public void OnSelect(BaseEventData eventData)
    {
        if (!string.IsNullOrEmpty(textToSpeak))
        {
            GameManager.GetManager<AudioManager>().Speak(textToSpeak);
        }
    }
}

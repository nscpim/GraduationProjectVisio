using UnityEngine;

public class AndroidTTS : Manager
{
    public AndroidJavaObject tts;
    private bool isInit = false;


    public override void Start()
    {
        if (Application.platform != RuntimePlatform.Android)
        {
            return;
        }

        AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");

        AndroidJavaObject activity =
            unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");

        tts = new AndroidJavaObject("android.speech.tts.TextToSpeech", activity, new AndroidInitializer(this));
    }

    public void Speak(string message)
    {
        if (!isInit || tts == null)
        {
            Debug.LogWarning("TTS not initalized");
            return;
        }
        tts.Call<int>("speak", message, 0, null, null);
    }


    public void OnTTSInit(bool succes)
    {
        isInit = succes;

        if (succes)
        {
            Debug.Log("TTS Initizalized");
        }
        else
        {
            Debug.LogError("TTS Failed to initalize");
        }

        AndroidJavaObject dutchLocale = new AndroidJavaObject("java.util.Locale", "nl", "NL");
        tts.Call<int>("setLanguage", dutchLocale);
    }

    public void OnDestroy()
    {
        if (tts != null)
        {
            tts.Call("shutdown");
        }
    }
}

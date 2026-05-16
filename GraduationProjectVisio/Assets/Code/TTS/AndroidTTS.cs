using System.Threading.Tasks;
using UnityEngine;

public class AndroidTTS : Manager
{
    private AndroidJavaObject tts;
    private bool initialized = false;

    public override void Start()
    {
        if (Application.platform != RuntimePlatform.Android)
            return;

        try
        {
            AndroidJavaClass unityPlayer =
                new AndroidJavaClass("com.unity3d.player.UnityPlayer");

            AndroidJavaObject activity =
                unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");

            tts = new AndroidJavaObject(
                "android.speech.tts.TextToSpeech",
                activity,
                null
            );

            InitializeTTS();
        }
        catch (System.Exception e)
        {
            Debug.LogError("TTS Init Error: " + e.Message);
        }
    }

    private async void InitializeTTS()
    {
        await Task.Delay(2000);
        if (tts == null)
            return;

        try
        {
            AndroidJavaObject locale =
                new AndroidJavaObject("java.util.Locale", "nl", "NL");

            tts.Call<int>("setLanguage", locale);

            initialized = true;

            Debug.Log("TTS Initialized");
        }
        catch (System.Exception e)
        {
            Debug.LogError("TTS Language Error: " + e.Message);
        }
    }

    public void Speak(string text)
    {
        if (!initialized || tts == null)
        {
            Debug.LogWarning("TTS not ready");
            return;
        }

        try
        {
            tts.Call<int>("speak", text, 0, null, null);
        }
        catch (System.Exception e)
        {
            Debug.LogError("Speak Error: " + e.Message);
        }
    }

    private void OnDestroy()
    {
        if (tts != null)
        {
            tts.Call("shutdown");
        }
    }
}

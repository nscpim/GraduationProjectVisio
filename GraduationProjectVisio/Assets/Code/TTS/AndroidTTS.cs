using System.Threading.Tasks;
using UnityEngine;

public class AndroidTTS : Manager
{
    private AndroidJavaObject tts;
    private bool initialized = false;

    public override void Start()
    {
        //Check if the application runs in an android container
        if (Application.platform != RuntimePlatform.Android)
            return;

        //Try to get the text to speech Java object
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

    /// <summary>
    /// Initalizes the Text-to-speech and changes the voice language to dutch
    /// Async task to give it time to get the java object, this process can take some time
    /// </summary>
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

    /// <summary>
    /// Lets the Text-to-speech voice speak with a certain text
    /// </summary>
    /// <param name="text"></param>
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
}

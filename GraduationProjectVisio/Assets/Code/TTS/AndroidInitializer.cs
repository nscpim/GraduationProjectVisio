using UnityEngine;

public class AndroidInitializer : AndroidJavaProxy
{
    private AndroidTTS ttsManager;

    public AndroidInitializer(AndroidTTS manager) : base ("android.speech.tts.TextToSpeech#OnInitListener")
    {
        ttsManager = manager;
    }

    public void OnInit(int status)
    {
        bool success = status == 0;
        ttsManager.OnTTSInit(success);
    }
}

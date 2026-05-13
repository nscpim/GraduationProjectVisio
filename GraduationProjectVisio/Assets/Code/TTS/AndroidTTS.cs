using UnityEngine;

public class AndroidTTS : Manager
{
    private AndroidJavaObject tts;
    private bool isInit = false;


    public override void Start()
    {
        if (Application.platform != RuntimePlatform.Android)
        {

        }
        
    }
}

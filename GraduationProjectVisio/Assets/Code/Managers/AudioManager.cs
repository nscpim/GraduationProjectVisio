using UnityEngine;

public class AudioManager : Manager
{
    


    public void Speak(string text)
    {
        //TTS code
    }

    public void PlayCorrect()
    {
        // feedbackSource.PlayOneShot()
        Debug.Log("Correct");
    }

    public void PlayIncorrect()
    {
        //   feedbackSource.PlayOneShot()
        Debug.Log("Incorrect");
    }

}

using System;
using UnityEngine;

public class KeyboardInputManager : Manager
{
    public Action<KeyCode> OnKeyPressed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public override void Start()
    {

    }

    // Update is called once per frame
    public override void Update()
    {

        //Everytime a key is pressed check if it is shiftpressed and pass it over to the OnKeyPressed action.
        if (Input.anyKeyDown)
        {
            bool shiftHeld = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);

            foreach (KeyCode key in Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown(key))
                {
                    OnKeyPressed?.Invoke(key);
                    Debug.Log($"Press key: {key}");
                    if (GameManager.GetManager<LessonManager>().currentLesson)
                    {
                        LessonManager lesson = GameManager.GetManager<LessonManager>();
                        lesson.ReceiveInput(key);
                    }
                }
            }
        }

    }
}

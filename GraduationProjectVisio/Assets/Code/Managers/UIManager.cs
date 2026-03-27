using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using System.Collections.Generic;
using System.Text;

public class UIManager : Manager
{
    public void DisplayUI(KeyCode text, TextMeshProUGUI textElement, Color color, List<KeyCode> combinationKeys) 
    {
        StringBuilder p = new StringBuilder();
        foreach (KeyCode key in combinationKeys)
        {
            p.Append(key.ToString() + " + ");
        }
        p.Append(" " + text.ToString());
        textElement.text = string.Format("Toetsen Combinatie: {0}", p.ToString());
        textElement.color = color;
    }


    public void CreateProfileButton() 
    {
        GameManager.GetManager<LessonManager>().InitializeLesson(GameManager.instance.lessons[0]);
    }
}

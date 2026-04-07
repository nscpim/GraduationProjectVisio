using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using System.Collections.Generic;
using System.Text;
using System;
using Unity.VisualScripting;
using UnityEngine.UI;

public class UIManager : Manager
{
    string replacedString;
    private bool lessonsFilled = false;
    public void DisplayUI(KeyCode text, TextMeshProUGUI textElement, Color color, List<KeyCode> combinationKeys)
    {
        StringBuilder p = new StringBuilder();
        foreach (KeyCode key in combinationKeys)
        {
            string newKey = key.ToString();
            newKey = ReplaceString(newKey, "Alpha");
            p.Append(newKey + " + ");
        }
        string newText = text.ToString();
        newText = ReplaceString(newText, "Alpha");
        p.Append(" " + newText.ToString());
        textElement.text = string.Format("Toetsen Combinatie: {0}", p.ToString());
        textElement.color = color;
    }

    public string ReplaceString(string _string, string subStringToRemove, string replaceWith = "")
    {
        return replacedString = _string.Replace(subStringToRemove, replaceWith);
    }

    public void DisplayText(string text, TextMeshProUGUI textElement, Color color)
    {
        textElement.text = text;
        textElement.color = color;
    }

    public GameObject GetPanelByName(string name)
    {
        for (int i = 0; i < GameManager.instance.panels.Count; i++)
        {
            if (GameManager.instance.panels[i].name == name)
            {
                return GameManager.instance.panels[i];
            }
        }
        Debug.Log($"The panel with name: {name} does not exist");
        return null;

    }

    public void MagnifyToggle() 
    {
        if (GameManager.instance.magnifyObject.activeInHierarchy)
        {
            GameManager.instance.magnifyObject.SetActive(false);
        }
        else
        {
            GameManager.instance.magnifyObject.SetActive(true);
        }
    }

    public void CloseAllPanels()
    {
        for (int i = 0; i < GameManager.instance.panels.Count; i++)
        {
            GameManager.instance.panels[i].SetActive(false);
        }
    }


    public void FontSizeUp() 
    {
        for (int i = 0; i < GameManager.instance.allTextComps.Count; i++)
        {
            GameManager.instance.allTextComps[i].fontSize += 2;
        }
    }

    public void FontSizeDown() 
    {
        for (int i = 0; i < GameManager.instance.allTextComps.Count; i++)
        {
            GameManager.instance.allTextComps[i].fontSize -= 2;
        }
    }

    public void ToggleObject(GameObject _object, bool toggle)
    {
        _object.SetActive(toggle);
    }

    public void FillLessonUI()
    {
        foreach (LessonData lesson in GameManager.instance.lessons)
        {
            Button lessonButton = GameObject.Instantiate(GameManager.instance.lessonButtonPrefab,
                  GameManager.instance.lessonButtonPrefab.gameObject.transform.position,
                  Quaternion.identity, GetPanelByName("LessonSelectionPanel").transform);

            lessonButton.GetComponentInChildren<TextMeshProUGUI>().text = lesson.lessonName;
            lessonButton.onClick.AddListener(() => SetupLesson(GameManager.GetManager<LessonManager>().GetLesson(lesson.lessonName)));
        }
        Debug.Log("Filled in all Lessons"); 
        lessonsFilled = true;
    }

    
    public void SetupLesson(LessonData lesson)
    {
        CloseAllPanels();
        GameManager.GetManager<LessonManager>().SetLesson(lesson.lessonName);
        ToggleObject(GetPanelByName("InLessonPanel"), true);
    }


    public void ClosePanelButtons()
    {
        CloseAllPanels();
    }


    public void CreateProfileButton()
    {
        ToggleObject(GetPanelByName("ProfilesPanel"), false);
    }

    public void SelectLessonProfileButton()
    {
        ToggleObject(GetPanelByName("LessonSelectionPanel"), true);
        if (!lessonsFilled)
        {
            FillLessonUI();
        }
    }
}

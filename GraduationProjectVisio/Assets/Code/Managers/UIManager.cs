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
    private Transform lastTypingStep;

    private List<TypingStepUI> stepUIs = new List<TypingStepUI>();

    /// <summary>
    /// Displays the combination text on the screen
    /// </summary>
    /// <param name="text"></param>
    /// <param name="textElement"></param>
    /// <param name="color"></param>
    /// <param name="combinationKeys"></param>
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
    /// <summary>
    /// String replacer that replaces any string to whatever is needed and defaults to ""
    /// </summary>
    /// <param name="_string"></param>
    /// <param name="subStringToRemove"></param>
    /// <param name="replaceWith"></param>
    /// <returns></returns>
    public string ReplaceString(string _string, string subStringToRemove, string replaceWith = "")
    {
        return replacedString = _string.Replace(subStringToRemove, replaceWith);
    }

    /// <summary>
    /// Displays simple text in a text component
    /// </summary>
    /// <param name="text"></param>
    /// <param name="textElement"></param>
    /// <param name="color"></param>
    public void DisplayText(string text, TextMeshProUGUI textElement, Color color)
    {
        textElement.text = text;
        textElement.color = color;
    }


    /// <summary>
    /// Gets a panel by name, returning a gameobject
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
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


    /// <summary>
    /// Enabled or disables the magnifying glass
    /// </summary>
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

    /// <summary>
    /// Closes all open panels
    /// </summary>
    public void CloseAllPanels()
    {
        for (int i = 0; i < GameManager.instance.panels.Count; i++)
        {
            GameManager.instance.panels[i].SetActive(false);
        }
        ToggleVisualKeyBoard(true);
        foreach (var pair in GameManager.GetManager<VisualKeyboardManager>().keyMap)
        {
            pair.Value.SetDefault();
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
        ToggleVisualKeyBoard(false);
    }


    public void SetupLesson(LessonData lesson)
    {
        ToggleVisualKeyBoard(true);
        CloseAllPanels();
        GameManager.GetManager<LessonManager>().SetLesson(lesson.lessonName);
        ToggleObject(GetPanelByName("InLessonPanel"), true);
    }

    public void ToggleVisualKeyBoard(bool toggle)
    {
        foreach (var item in GameManager.instance.visualKeyboard)
        {
            ToggleObject(item, toggle);
        }
    }

    public void ClosePanelButtons()
    {
        CloseAllPanels();
    }

    /// <summary>
    /// Exetutes when the create profile button is pressed
    /// </summary>
    public void CreateProfileButton()
    {
        OnCreateProfileClicked();
        ToggleObject(GetPanelByName("ProfilesPanel"), false);
    }

    /// <summary>
    /// Executes when the select lesson button is pressed
    /// </summary>
    public void SelectLessonProfileButton()
    {
        ToggleObject(GetPanelByName("LessonSelectionPanel"), true);
        if (!lessonsFilled)
        {
            FillLessonUI();
        }
    }


    #region Profile
    /// <summary>
    /// Refreshed the profiles from disk and shows them in the UI
    /// </summary>
    public void RefreshProfileUI()
    {
        foreach (Transform item in GameManager.instance.profilesContainer)
        {
            GameManager.instance.CustomDestroyGameObject(item.gameObject);
        }

        List<string> profileIds = ProfileUtility.GetAllProfiles();

        foreach (var item in profileIds)
        {
            PlayerProfile profile = ProfileSaveSystem.LoadProfile(item);

            if (profile == null)
            {
                continue;
            }

            var profileItem = GameObject.Instantiate(GameManager.instance.profileItem, GameManager.instance.profilesContainer);

            //Setup naming and information
            profileItem.GetComponent<ProfileItemUI>().Setup(profile, OnProfileClicked);
        }
    }

    /// <summary>
    /// Opens the profile panel
    /// </summary>
    public void OpenProfilePanel()
    {
        Debug.Log("OpenProfilePanel");
        CloseAllPanels();
        ToggleVisualKeyBoard(false);
        ToggleObject(GetPanelByName("ProfilesPanel"), true);
    }

    /// <summary>
    /// When a profile is clicked load that profile from disk,
    /// </summary>
    /// <param name="profileId"></param>
    public void OnProfileClicked(string profileId)
    {
        Debug.Log("Loading Profile" + profileId);
        GameManager.GetManager<SaveLoadManager>().LoadProfile(profileId);
        GameManager.instance.profileName.text = GameManager.GetManager<SaveLoadManager>().currentProfile.profileName;
        CloseAllPanels();
        ToggleObject(GetPanelByName("ProfilesPanel"), true);
    }

    public void OnCreateProfileClicked()
    {
        string playerName = GameManager.instance.nameInputField.text;

        if (string.IsNullOrEmpty(playerName))
        {
            Debug.LogWarning("Enter a name first");
            return;
        }

        GameManager.GetManager<SaveLoadManager>().CreateNewProfile(playerName);

        RefreshProfileUI();
    }
    #endregion


    #region Custom Lessons
    public void StartCreatingLesson()
    {
        CloseAllPanels();
        ToggleObject(GetPanelByName("LessonCreatorPanel"), true);
        ToggleVisualKeyBoard(false);
        PrepareLesson();
    }

    public void PrepareLesson()
    {
        Debug.Log("Start creating lesson");

        // Clear lesson name
        GameManager.instance.lessonNameInput.text = "";

        // Clear old steps
        foreach (var step in stepUIs)
        {
            GameObject.Destroy(step.gameObject);
        }
        stepUIs.Clear();

        // Add a default step
        AddStep();
    }

    /// <summary>
    /// Adds a step when clicked on the add step button
    /// </summary>
    public void AddStep()
    {
        var step = GameObject.Instantiate(GameManager.instance.stepPrefab, GameManager.instance.stepsContainer);
        if (lastTypingStep != null)
        {
            ToggleObject(lastTypingStep.gameObject, false);
            lastTypingStep = step.transform;
        }
        else
        {
            lastTypingStep = step.transform;
        }
        stepUIs.Add(step);
        for (int i = 0; i < stepUIs.Count; i++)
        {
            Debug.Log("Value of List Step UI: " + stepUIs[i].instructionInput.text);
        }
    }

    public void RemoveStep(TypingStepUI step)
    {
        stepUIs.Remove(step);
        GameObject.Destroy(step.gameObject);
    }

    public void SaveLesson()
    {
        LessonSaveData lesson = new LessonSaveData();

        lesson.id = System.Guid.NewGuid().ToString();
        lesson.lessonName = GameManager.instance.lessonNameInput.text;

        lesson.steps = new List<TypingStep>();

        foreach (var stepUI in stepUIs)
        {
            lesson.steps.Add(stepUI.GetData());
        }

        LessonFileSystem.SaveLesson(lesson);

        Debug.Log("Lesson saved with " + lesson.steps.Count + " steps!");
    }
    #endregion
}

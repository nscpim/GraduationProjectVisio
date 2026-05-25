using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;

public class TypingStepUI : MonoBehaviour
{
    public TMP_InputField instructionInput;
    public TMP_Dropdown targetKeyDropdown;
    public TMP_InputField wrongTextInput;



    public Transform requiredKeysContainer;

    private List<KeyCode> availableKeys = new List<KeyCode>();
    private List<KeyCode> requiredKeys = new List<KeyCode>();

    void Start()
    {
        SetupDropdown();
        SetupRequiredKeyToggles();

    }
    /// <summary>
    /// Fills the dropdown menu with usable keys
    /// </summary>
    void SetupDropdown()
    {
        targetKeyDropdown.ClearOptions();

        List<string> options = new List<string>();

        foreach (KeyCode key in Enum.GetValues(typeof(KeyCode)))
        {
            if (isValidKey(key))
            {
                availableKeys.Add(key);
                options.Add(key.ToString());
            }
        }

        targetKeyDropdown.AddOptions(options);
    }

    /// <summary>
    /// Gets the current selected key in the drpdown
    /// </summary>
    /// <returns></returns>
    public KeyCode GetSelectedKey()
    {
        return availableKeys[targetKeyDropdown.value];
    }

    /// <summary>
    /// Filters unwanted keys.
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    bool isValidKey(KeyCode key)
    {
        string keyString = key.ToString();
        // if (key >= KeyCode.F13 && key <= KeyCode.F24)
        // {
        //    return false;
        // }
        if (keyString.Contains("Joy"))
        {
            return false;
        }
        return true;
    }

    /// <summary>
    /// Adds the additional keys to the requiredkeys list
    /// </summary>
    void SetupRequiredKeyToggles()
    {
        requiredKeys.Clear();
        foreach (Transform child in requiredKeysContainer)
        {
            requiredKeys.Add(child.GetComponent<RequiredKeyItemUI>().GetKey());
        }
    }




    /// <summary>
    /// Gets the data of the lesson creator combing the target key and required keys
    /// </summary>
    /// <returns></returns>
    public TypingStep GetData()
    {
        TypingStep step = new TypingStep();

        step.instructionText = instructionInput.text;


        step.targetKey = availableKeys[targetKeyDropdown.value];

        step.requiredKeys = new List<KeyCode>();

        SetupRequiredKeyToggles();

        for (int i = 0; i < requiredKeys.Count; i++)
        {
            step.requiredKeys.Add(requiredKeys[i]);
        }

        step.instructionIfWrong = wrongTextInput.text;

        return step;
    }
}

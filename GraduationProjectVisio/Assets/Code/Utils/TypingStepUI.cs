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

    public KeyCode GetSelectedKey()
    {
        return availableKeys[targetKeyDropdown.value];
    }

    bool isValidKey(KeyCode key)
    {
        string keyString = key.ToString();
        if (key >= KeyCode.F13 && key <= KeyCode.F24)
        {
            return false;
        }
        if (keyString.Contains("Joy"))
        {
            return false;
        }
        return true;
    }

    void SetupRequiredKeyToggles()
    {
        requiredKeys.Clear();
        foreach (Transform child in requiredKeysContainer)
        {
           requiredKeys.Add(child.GetComponent<RequiredKeyItemUI>().GetKey());
        }
    }

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

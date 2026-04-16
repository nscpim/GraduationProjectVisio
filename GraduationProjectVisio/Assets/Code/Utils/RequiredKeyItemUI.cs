using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RequiredKeyItemUI : MonoBehaviour
{
    public TMP_Dropdown keyDropdown;

    void Awake()
    {
        PopulateDropdown();
    }

    void PopulateDropdown()
    {
        keyDropdown.ClearOptions();

        List<string> options = new List<string>();

        foreach (KeyCode key in Enum.GetValues(typeof(KeyCode)))
        {
            options.Add(key.ToString());
        }

        keyDropdown.AddOptions(options);
    }

    public KeyCode GetKey()
    {
        return (KeyCode)keyDropdown.value;
    }
}

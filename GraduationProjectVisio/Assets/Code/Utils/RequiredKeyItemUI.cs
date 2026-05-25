using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RequiredKeyItemUI : MonoBehaviour
{
    public TMP_Dropdown keyDropdown;
    public List<KeyCode> availableKeys = new List<KeyCode>();
    List<string> options = new List<string>();
    public 

    void Awake()
    {
        PopulateDropdown();
    }

    /// <summary>
    /// Populates the dropdown menus with usable keys
    /// </summary>
    void PopulateDropdown()
    {
        keyDropdown.ClearOptions();


        foreach (KeyCode key in Enum.GetValues(typeof(KeyCode)))
        {
            if (isValidKey(key))
            {
                availableKeys.Add(key);
                options.Add(key.ToString());
            }
        }

        keyDropdown.AddOptions(options);
    }

    /// <summary>
    /// Filters keys that are unwanted or not being used
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    bool isValidKey(KeyCode key)
    {
        string keyString = key.ToString();
        //if (key >= KeyCode.F13 && key <= KeyCode.F24)
        //{
        //    return false;
        //}
        if (keyString.Contains("Joy"))
        {
            return false;
        }
        return true;
    }

    /// <summary>
    /// Gets the current key of the dropdown menu
    /// </summary>
    /// <returns></returns>
    public KeyCode GetKey()
    {
        return availableKeys[keyDropdown.value];
    }
}

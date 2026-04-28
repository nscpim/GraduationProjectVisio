using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class VisualKeyboardManager : Manager
{
    public List<KeyUI> keys = new List<KeyUI>();

    public Dictionary<KeyCode, KeyUI> keyMap;

    public override void Start()
    {
        keyMap = new Dictionary<KeyCode, KeyUI>();

        foreach (var key in keys)
        {
            keyMap[key.key] = key;
        }
    }
}

using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TypingStepSaveData
{
    public string instructionText;

    public int targetKey;

    public List<int> requiredKeys;

    public string instructionIfWrong;
}

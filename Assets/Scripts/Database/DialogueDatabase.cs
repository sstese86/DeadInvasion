using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Sirenix.OdinInspector;

[System.Serializable]
public struct Dialogue
{
    public string name;

    [TextArea(3,10)]
    public string[] sentences;
}

[CreateAssetMenu(fileName = "DialogueDatabase", menuName = "NaviEnt/DialogueDatabase", order = 3)]
public class DialogueDatabase : SerializedScriptableObject
{
    public Dictionary<string, Dialogue> data;
}

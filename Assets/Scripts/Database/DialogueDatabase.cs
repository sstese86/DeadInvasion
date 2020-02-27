﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Dialogue
{
    public string name;

    [TextArea(3,10)]
    public string[] sentences;
}

public class DialogueDatabase : ScriptableObject
{

}

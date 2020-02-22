using System.Collections;
using System.Collections.Generic;
using System;


[Serializable]
public class Entity
{
    string _name = string.Empty;
    string _description = string.Empty;

    public string Name => _name;
    public string Description => _description;
}

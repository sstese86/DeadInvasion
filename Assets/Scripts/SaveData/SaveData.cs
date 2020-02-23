using System.Collections;
using System.Collections.Generic;
using System;

public abstract class SaveData<T> where T:SaveData<T>
{
    public static T Instance { get; }
    public string hashValue;

    public SaveData()
    {
        hashValue = String.Empty;
    }
}


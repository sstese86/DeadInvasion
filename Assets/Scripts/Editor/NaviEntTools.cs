using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.U2D;
using UnityEditor;

using NaviEnt.Data;

#region ITEM_DATA
[Serializable]
public class ItemDataExternal
{
    // enum - 0 Simple, 1 Consumable, 2 Equipable
    public int itemType;

    public string name;
    public string description;

    public string iconPath;
    public string objPath;
    
    public int notAllowMultiple;
    
    public int value;

    public bool GetBoolNotAllowMultiple()
    {
        if (notAllowMultiple == 0)
            return false;
        else return true;
    }

    public Sprite GetIconSprite()
    {
        Sprite[] sprites;
        string path = string.Empty;
        string[] stringArray = iconPath.Split('_');
        
        int length = stringArray.Length - 1;
        for (int i = 0; i< length; i++)
        {
            path += stringArray[i] + "_";
        }
        path = path.Remove(path.Length - 1, 1);

        int subIndex = int.Parse(stringArray[stringArray.Length - 1]);
        sprites = Resources.LoadAll<Sprite>(path);
        return sprites[subIndex];
    }

    public GameObject GetGameObject()
    {
        GameObject obj;
        obj = Resources.Load<GameObject>(objPath);
        return obj;
    }

}

[Serializable]
public class JsonItemDataFromHoudini
{
    public List<ItemDataExternal> data = new List<ItemDataExternal>();
}
#endregion

public class NaviEntTools : EditorWindow
{
    ItemDatabase _itemDatabase = null;
    string path = "";
    //JsonItemDataFromHoudini tempJsonItem;

    [MenuItem("Tools/NavientTools")]
    public static void ShowWindow()
    {
        GetWindow<NaviEntTools>("NaviEntTools");
    }

    private void OnGUI()
    {
        InitAttributes();

        GUILayout.Label("NaviEnt Tools", EditorStyles.boldLabel);


        path = EditorGUILayout.TextField("Path", path);

        if (GUILayout.Button("Set Path"))
        {
            path = EditorUtility.OpenFilePanel("Open Data File", "", "*");
            path = EditorGUILayout.TextField("FilePath", path);
        }

            if (GUILayout.Button("Load Data File"))
        {
            string jsonString = File.ReadAllText(path);
            JsonItemDataFromHoudini database = JsonUtility.FromJson<JsonItemDataFromHoudini>(jsonString);
            _itemDatabase.data.Clear();
            
            foreach (ItemDataExternal item in database.data)
            {
                ItemData newItem = new ItemData();
                                
                newItem.itemType = (ItemType)item.itemType;
                newItem.name = item.name;
                newItem.description = item.description;
                newItem.icon = item.GetIconSprite();
                newItem.value = item.value;
                newItem.notAllowMultiple = item.GetBoolNotAllowMultiple();
                newItem.obj = item.GetGameObject();
                _itemDatabase.data.Add(item.name, newItem);
            }

        }

/*
        if (GUILayout.Button("Save Data File"))
        {
            string jsonData = TestJsonData();
            Debug.Log(jsonData);
        }
*/


    }

    void InitAttributes()
    {
        _itemDatabase = Resources.Load<ItemDatabase>("ScriptableObject/ItemDatabase");

    }

    string TestJsonData()
    {
        ItemDataExternal one = new ItemDataExternal();
        ItemDataExternal two = new ItemDataExternal();
        JsonItemDataFromHoudini jsonData = new JsonItemDataFromHoudini();

        jsonData.data.Add(one);
        jsonData.data.Add(two);
        string data = JsonUtility.ToJson(jsonData);
        return data;
    }


}

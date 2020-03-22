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

    public int isEquipable;
    public int state_maxHealth;
    public int state_damage;
    public int state_strength;
    public int state_agility;
    public int state_defance;
    public float state_moveSpeed;
    public float state_jumpSpeed;


    public bool GetBoolFromIntValue(int value)
    {
        if (value == 0)
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
    string _dataRoot = "D:/ZZ_SourceControls/Unity3D/DeadInvasion/.localAssets/Data/";
    string _itemDataPath = "ItemDatabase";
    //JsonItemDataFromHoudini tempJsonItem;

    [MenuItem("Tools/NavientTools")]
    public static void ShowWindow()
    {
        GetWindow<NaviEntTools>("NaviEntTools");
    }

    private void OnGUI()
    {
        InitAttributes();

        LoadItemDatabase();

    }

    private void LoadItemDatabase()
    {
        GUILayout.Space(10);
        GUILayout.Label("Item Database", EditorStyles.helpBox);

        _itemDataPath = EditorGUILayout.TextField("Path", _itemDataPath);

        if (GUILayout.Button("Load ItemDatabase"))
        {
            string jsonString = File.ReadAllText(_dataRoot + _itemDataPath + ".json");
            JsonItemDataFromHoudini database = JsonUtility.FromJson<JsonItemDataFromHoudini>(jsonString);
            _itemDatabase.data.Clear();

            foreach (ItemDataExternal item in database.data)
            {
                ItemData newItem = new ItemData();

                newItem.itemType = (ItemType)item.itemType;
                newItem.name = item.name;
                newItem.description = item.description;
                newItem.value = item.value;
                newItem.notAllowMultiple = item.GetBoolFromIntValue(item.notAllowMultiple);
                newItem.icon = item.GetIconSprite();
                newItem.obj = item.GetGameObject();

                newItem.isEquipable = item.GetBoolFromIntValue(item.isEquipable);
                newItem.equipableState.maxHealth = item.state_maxHealth;
                newItem.equipableState.damage = item.state_damage;
                newItem.equipableState.strength = item.state_strength;
                newItem.equipableState.agility = item.state_agility;
                newItem.equipableState.defance = item.state_defance;
                newItem.equipableState.moveSpeed = item.state_moveSpeed;
                newItem.equipableState.jumpSpeed = item.state_jumpSpeed;

                _itemDatabase.data.Add(item.name, newItem);
            }
            EditorUtility.SetDirty(_itemDatabase);
        }
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

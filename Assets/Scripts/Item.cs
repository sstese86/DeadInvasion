using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;


[Serializable]
public class Item
{
    public string _itemName;
    public string _itemDescription;
    public Sprite _itemIcon;
}

[CreateAssetMenu(fileName = "ItemDatabase",menuName = "NaviEnt/ItemDatabase", order = 0)]
public class ItemDatabase : ScriptableObject
{
    public List<Item> ItemList;
}

using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Sirenix.OdinInspector;

namespace NaviEnt.Data
{
    
    public enum ItemType
    {
        Simple,
        Consumable,
        Equipable,
    }

    [Serializable]
    public struct ItemData
    {
        [HorizontalGroup("Preview")]
        [PreviewField(32)]
        [LabelWidth(32)]
        public Sprite icon;

       
        [PreviewField(32)]
        [LabelWidth(32)]
        public GameObject obj;

        [VerticalGroup("Base Info")]
        [EnumToggleButtons]
        public ItemType itemType;
        [VerticalGroup("Base Info")]
        [LabelWidth(100)]
        public string name;

        [VerticalGroup("Base Info")]
        [LabelWidth(300)]
        [TextArea(2, 6)]
        public string description;

        [HorizontalGroup("amount")]
        public bool notAllowMultiple;

        [HorizontalGroup("amount")]
        public int value;

    }

    [CreateAssetMenu(fileName = "ItemDatabase", menuName = "NaviEnt/ItemDatabase", order = 0)]
    public class ItemDatabase : SerializedScriptableObject
    {
        [TableList()]
        public Dictionary<string, ItemData> data;
    }
}

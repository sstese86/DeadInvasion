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
    public struct Item
    {
        [EnumToggleButtons]
        public ItemType itemType;

        [HorizontalGroup("Base Info")]
        [PreviewField(32)]
        [LabelWidth(32)]
        public Sprite icon;

        [HorizontalGroup("Base Info")]
        [PreviewField(32)]
        [LabelWidth(32)]
        public GameObject obj;

        [HorizontalGroup("Base Info")]
        [LabelWidth(300)]
        [TextArea(2, 6)]
        public string description;

        [HorizontalGroup("amount")]
        public bool isNotConsumable;
        [HorizontalGroup("amount"), DisableIf("isNotConsumable")]
        public int amount;
    }

    [CreateAssetMenu(fileName = "ItemDatabase", menuName = "NaviEnt/ItemDatabase", order = 0)]
    public class ItemDatabase : SerializedScriptableObject
    {
        [TableList()]
        public Dictionary<string, Item> itemDatabase;
    }
}

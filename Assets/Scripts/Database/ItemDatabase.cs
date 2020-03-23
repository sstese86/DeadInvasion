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
    }

    public enum EquipType
    {
        Head,
        Body,
        Weapon,
        Misc,
    }


    [Serializable]
    public struct ItemData
    {
        [Space]
        [Space]
        [HorizontalGroup("Preview")]
        [PreviewField(32)]
        [LabelWidth(32)]
        public Sprite icon;
        [HorizontalGroup("Preview")]
        [PreviewField(32)]
        [LabelWidth(32)]
        public GameObject obj;

        
        [EnumToggleButtons]
        public ItemType itemType;
        [HorizontalGroup("Base Info")]
        [VerticalGroup("Base Info/Left")]
        [LabelWidth(100)]
        public string name;
        [VerticalGroup("Base Info/Right")]
        public string key;

        [LabelWidth(300)]
        [TextArea(2, 6)]
        public string description;

        [HorizontalGroup("amount")]
        public bool notAllowMultiple;

        [HorizontalGroup("amount")]
        public int value;

        [TitleGroup("Equipment")]
        public bool isEquipable;

        [ShowIf("isEquipable")]
        [EnumToggleButtons]
        public EquipType equipType;
        
        [ShowIf("isEquipable")]
        public CharacterState equipableState;

        //[ShowIf("isWeapon")]
        [ShowIf("equipType",EquipType.Weapon)]
        public WeaponState weaponState;

    }

    [CreateAssetMenu(fileName = "ItemDatabase", menuName = "NaviEnt/ItemDatabase", order = 0)]
    public class ItemDatabase : SerializedScriptableObject
    {
        [TableList()]
        public Dictionary<string, ItemData> data;
    }
}

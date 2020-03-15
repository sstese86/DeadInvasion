using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace NaviEnt.Data
{
    [Serializable]
    public struct RewardItem
    {
        [LabelWidth(50)]
        [HorizontalGroup("Item")]
        public string key;
        [LabelWidth(80)]
        [HorizontalGroup("Item")]
        public int minAmount;
        [LabelWidth(80)]
        [HorizontalGroup("Item")]
        public int maxAmount;

    }

    [Serializable]
    public struct MissionData
    {
        public string missionName;

        [PreviewField(64,ObjectFieldAlignment.Right)]
        [HideLabel]
        [HorizontalGroup("Info",64)]
        public Sprite icon;
        
        [TextArea(2,5)]
        [HorizontalGroup("Info")]
        public string description;
        public int fightingPower;
        public List<RewardItem> rewardItemList;
    }

    [CreateAssetMenu(fileName = "MissionDatabase", menuName = "NaviEnt/MissionDatabase", order = 1)]
    public class MissionDatabase : SerializedScriptableObject
    {
        public Dictionary<string, MissionData> data;
    }
}
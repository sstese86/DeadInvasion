using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public enum MissionDifficulty
{
    Easy,
    Normal,
    Difficult
}

[Serializable]
public struct MissionData
{
    public int missionId;
    public string description;
    public MissionDifficulty difficulty;
    public int[] rewardItemId;
}

[CreateAssetMenu(fileName = "MissionDatabase",menuName = "NaviEnt/MissionDatabase",order = 1)]
public class MissionDatabase : ScriptableObject
{
    public List<MissionData> MissionData;
}

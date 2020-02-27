using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

using NaviEnt.Data;

public class PlayerExperienceInfo : PlayerResourceInfo
{
    [SerializeField]
    TextMeshProUGUI _text = null;

    public override void UpdatePlayerData()
    {
        _text.text = DataManager.Instance.Experience.ToString();
    }
}

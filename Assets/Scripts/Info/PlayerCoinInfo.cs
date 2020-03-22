using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

using NaviEnt.Data;

public class PlayerCoinInfo : PlayerResourceInfo
{
    [SerializeField]
    TextMeshProUGUI _text = null;
    
    public override void UpdatePlayerData()
    {
        _text.text = PlayerDataManager.Instance.Coin.ToString();
    }

}

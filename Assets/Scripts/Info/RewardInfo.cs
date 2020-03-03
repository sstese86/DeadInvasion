using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

using NaviEnt.Data;

public class RewardInfo : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _info = null;
    [SerializeField] Image _icon = null;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void UpdateInfo(ItemData item)
    {
        _info.text = item.description;
        _icon.sprite = item.icon;
    }
}

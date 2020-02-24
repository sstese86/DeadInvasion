using NaviEnt.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResourceButton : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI _text = null;

    // Start is called before the first frame update
    void Start()
    {
        GameEventManager.onPlayerDataChanged += UpdatePlayerData;

        UpdatePlayerData();
    }

    private void UpdatePlayerData()
    {
        _text.text = DataManager.Instance.Gold.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

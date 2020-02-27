﻿using NaviEnt.Game;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerResourceInfo : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameEventManager.onPlayerDataChanged += UpdatePlayerData;
        UpdatePlayerData();
    }

    private void OnDestroy()
    {
        GameEventManager.onPlayerDataChanged -= UpdatePlayerData;
    }
    public abstract void UpdatePlayerData();


    // Update is called once per frame
    void Update()
    {
        
    }
}
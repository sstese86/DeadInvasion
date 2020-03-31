using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaviEnt.Game;

public class MissionInit : MonoBehaviour
{
    // Start is called before the first frame update

    private void Awake()
    {
        BattleManager.Instance.InitBattleGame();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

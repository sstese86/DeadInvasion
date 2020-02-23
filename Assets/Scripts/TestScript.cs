using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaviEnt.Data;



public class TestScript : MonoBehaviour
{
    PlayerData _testData = null;
    BattleData _testData2 = null;
    SystemData _testData3 = null;
    // Start is called before the first frame update
    void Start()
    {
        _testData = new PlayerData();
        _testData2 = new BattleData();
        _testData3 = new SystemData();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TestMethod()
    {
        DataSaver.Instance._jsonSaver.Save<PlayerData>(_testData);
        DataSaver.Instance._jsonSaver.Save<BattleData>(_testData2);
        DataSaver.Instance._jsonSaver.Save<SystemData>(_testData3);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

using NaviEnt.Level;
using NaviEnt.Data;

namespace NaviEnt.UI
{
    public class MissionEnterMenu : Menu<MissionEnterMenu>
    {
        [SerializeField] TextMeshProUGUI _missionIdText = null;
        [SerializeField] TextMeshProUGUI _difficultyText = null;
        [SerializeField] GameObject _rewardLayoutGroup = null;

        [SerializeField] GameObject _rewardInfoPrefab = null;
        [SerializeField] MissionDatabase _missionDatabase = null;
        [SerializeField] ItemDatabase _itemDatabase = null;

        int _missionIndex = 0;
        // Start is called before the first frame update
        private void Start()
        {

        }

        //protected override void Awake()
        //{
        //    base.Awake();
        //}

        public void UpdateInfo(int missionIndex)
        {
            _missionIndex = missionIndex - 1;

            if (_missionIndex > _missionDatabase.MissionData.Count)
            { 
                Debug.Log("MissionEnterMenu: Error! missionIndex is invalid check the MissionDatabase.");
                return;
            }
            _missionIdText.text = _missionDatabase.MissionData[_missionIndex].missionId.ToString();
            _difficultyText.text = _missionDatabase.MissionData[_missionIndex].difficulty.ToString();

            foreach(Transform child in _rewardLayoutGroup.transform)
            {
                Destroy(child.gameObject);
            }
            
            foreach(string itemName in _missionDatabase.MissionData[_missionIndex].rewardItemName)
            {
               RewardInfo info = GameObject.Instantiate(_rewardInfoPrefab, _rewardLayoutGroup.transform).GetComponent<RewardInfo>();
                Item tempItem;
                if(_itemDatabase.itemDatabase.TryGetValue(itemName, out tempItem))
                {
                    info.UpdateInfo(tempItem);
                }
                else
                {
                    Debug.Log("MissionEnterMenu: Error! there is no Item" + itemName +" in ItemDatabase.");
                }
                
            }

        }

        public void EnterMission()
        {
            LevelManager.LoadGame(_missionIndex);
        }

    }
}
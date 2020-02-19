using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
        // Start is called before the first frame update
        private void Start()
        {
            UpdateInfo(1);
        }


        public void UpdateInfo(int missionIndex)
        {
            _missionIdText.text = _missionDatabase.MissionData[missionIndex].missionId.ToString();
            _difficultyText.text = _missionDatabase.MissionData[missionIndex].difficulty.ToString();

            foreach(Transform child in _rewardLayoutGroup.transform)
            {
                Destroy(child.gameObject);
            }
            
            foreach(int item in _missionDatabase.MissionData[missionIndex].rewardItemId)
            {
               RewardInfo info = GameObject.Instantiate(_rewardInfoPrefab, _rewardLayoutGroup.transform).GetComponent<RewardInfo>();
                info.UpdateInfo(_itemDatabase.ItemList[item]);
            }

        }

    }
}
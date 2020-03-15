using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

using NaviEnt.Level;
using NaviEnt.Data;
using NaviEnt.Game;

namespace NaviEnt.UI
{
    [System.Serializable]
    public enum MissionDifficulty
    {
        Easy,
        Normal,
        Difficult
    }

    public class MissionEnterMenu : Menu<MissionEnterMenu>
    {
        [SerializeField] TextMeshProUGUI _missionNameText = null;
        [SerializeField] TextMeshProUGUI _difficultyText = null;
        [SerializeField] GameObject _rewardLayoutGroup = null;

        [SerializeField] GameObject _rewardInfoPrefab = null;
        [SerializeField] MissionDatabase _missionDatabase = null;
        [SerializeField] ItemDatabase _itemDatabase = null;

        string _key = string.Empty;

        // Start is called before the first frame update
        private void Start()
        {

        }

        //protected override void Awake()
        //{
        //    base.Awake();
        //}

        public void UpdateInfo(string key)
        {
            _key = key;

            if (!_missionDatabase.data.ContainsKey(_key))
            { 
                Debug.Log("MissionEnterMenu: Error!" + _key + " mission is invalid check the MissionDatabase.");
                return;
            }
            _missionNameText.text = _missionDatabase.data[_key].missionName.ToString();
            _difficultyText.text = CalculateDifficultyLevel(_missionDatabase.data[_key].fightingPower);

            foreach(Transform child in _rewardLayoutGroup.transform)
            {
                Destroy(child.gameObject);
            }
            
            foreach(RewardItem item in _missionDatabase.data[_key].rewardItemList)
            {
               RewardInfo info = GameObject.Instantiate(_rewardInfoPrefab, _rewardLayoutGroup.transform).GetComponent<RewardInfo>();
                ItemData tempItem;
                if(_itemDatabase.data.TryGetValue(item.key, out tempItem))
                {
                    info.UpdateInfo(tempItem);
                }
                else
                {
                    Debug.Log("MissionEnterMenu: Error! there is no Item" + item.key + " in ItemDatabase.");
                }
                
            }

        }
        string CalculateDifficultyLevel(int missionFightingPower)
        {
            MissionDifficulty difficulty;
            float playerPower = GameManager.Instance.PlayerFightingPower;

            if((float)missionFightingPower < (playerPower * 0.5f))
            {
                difficulty = MissionDifficulty.Easy;
            }
            else if((float)missionFightingPower > (playerPower * 1.5f))
            {
                difficulty = MissionDifficulty.Difficult;
            }
            else
            {
                difficulty = MissionDifficulty.Normal;
            }
                return difficulty.ToString();
        }

        public void ButtonEnterMission()
        {
            LevelManager.Instance.EnterToMission(_key);
            MenuClose();
        }

    }
}
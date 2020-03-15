using NaviEnt.Game;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

using NaviEnt;

namespace NaviEnt.Data
{
    public class DataManager : SerializedMonoBehaviour
    {
        public static DataManager Instance { get; private set; }

        static PlayerData _playerData;
        static SystemData _systemData;

        #region PLAYERDATA

        public string PlayerName
        {
            get => _playerData.playerName;
            set => _playerData.playerName = value;
        }

        public int Coin
        {
            get => _playerData.playerItem["Coin"];
            set => _playerData.playerItem["Coin"] = value;
        }

        public int Gas
        {
            get => _playerData.playerItem["Gas"];
            set => _playerData.playerItem["Gas"] = value;
        }

        public int Experience
        {
            get => _playerData.playerItem["Experience"];
            set => _playerData.playerItem["Experience"] = value;
        }

        public int GetPlayerItemAmount(string key)
        {
            if (!_playerData.playerItem.ContainsKey(key))
            {
                Debug.Log("DataManager_playerItem:: there is no item key: " + key);
                return 0;
            }
            return _playerData.playerItem[key];
        }

        public void SetPlayerItemAmount(string key, int amount)
        {
            if (!_playerData.playerItem.ContainsKey(key))
            {
                Debug.Log("DataManager_playerItem:: there is no item key: " + key);
                return;
            }
            _playerData.playerItem[key] = amount;
            UpdatePlayerData();
        }

        public void SetPlayerQuest(Quest quest)
        {

            List<int> questInfo = new List<int>();
            if(_playerData.playerActiveQuest== null)
            {
                Debug.Log("DataManager:: ERROR! playerActiveQuestIs Null");
                return;
            }

            foreach(QuestGoal goal in quest.questGoals)
            {
                // From List.first are for QuestGolesProgress.
                questInfo.Add(goal.currentAmount);
            }
            if(quest.isActive)
            {
                _playerData.playerActiveQuest[quest.Key] = questInfo;
            }
            else
            {
                _playerData.playerFinishedQuestKey.Add(quest.Key);
            }          
        }

        public Dictionary<string,List<int>> GetActiveQuests()
        {
            return  _playerData.playerActiveQuest;
        }

        public List<string> GetFinishedQuests()
        {
            return _playerData.playerFinishedQuestKey;
        }

        #endregion

        #region SETTINGS


        public float MasterVolume
        {
            get => _systemData.masterVolume;
            set => _systemData.masterVolume = value;
        }

        public float SfxVolume
        {
            get => _systemData.sfxVolume;
            set => _systemData.sfxVolume = value;
        }

        public float MusicVolume
        {
            get => _systemData.musicVolume;
            set => _systemData.musicVolume = value;
        }

        public GraphicQuality GraphicQuality
        {
            get => _systemData.graphicQuality;
            set => _systemData.graphicQuality = value;
        }

        #endregion

        private void Awake()
        {
            if (Instance != null) Destroy(gameObject);
            else Instance = this;
        }

        private void Start()
        {
            GameEventManager.onSavePlayerData += SavePlayerData;
            LoadPlayerData();

        }

        private void OnDestroy()
        {
            GameEventManager.onSavePlayerData -= SavePlayerData;
        }

        private void initData()
        {
            _playerData = new PlayerData();
            //initilize default values.
            _playerData.playerName = "Player";
            _playerData.playerActiveQuest = new Dictionary<string, List<int>>();
            _playerData.playerFinishedQuestKey = new List<string>();

            _playerData.playerItem = new Dictionary<string, int>();
            _playerData.playerItem.Add("Coin", 0);
            _playerData.playerItem.Add("Gas", 0);
            _playerData.playerItem.Add("Experience", 0);

            _systemData = new SystemData();
            //initilize default values.
            _systemData.isFirstTime = true;
            _systemData.masterVolume = 1f;
            _systemData.sfxVolume = 0.75f;
            _systemData.musicVolume = 0.55f;
            _systemData.graphicQuality = GraphicQuality.Normal;
        }


        public void UpdatePlayerData()
        {
            GameEventManager.Instance.OnPlayerDataChanged();
        }

        public void SavePlayerData()
        {
            DataSaver.Instance.Save<PlayerData>(_playerData);
        }

        public void LoadPlayerData()
        {
            if(DataSaver.Instance.FileExists<PlayerData>(_playerData))
            { 
                _playerData = DataSaver.Instance.Load<PlayerData>(_playerData);
                UpdatePlayerData();
            }
            else
            {
                initData();
                SavePlayerData();
                Debug.Log("Savefile dosn't exist. Default savefile created.");
            }
        }

        public void SaveSettings()
        {
            DataSaver.Instance.Save<SystemData>(_systemData);
        }

    }
}
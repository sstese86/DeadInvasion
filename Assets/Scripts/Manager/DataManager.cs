using NaviEnt.Game;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NaviEnt.Data
{
    public class DataManager : MonoBehaviour
    {
        public static DataManager Instance { get; private set; }

        PlayerData _playerData;
        SystemData _systemData;

        #region PLAYERDATA

        public string PlayerName
        {
            get => _playerData.playerName;
            set => _playerData.playerName = value;
        }

        public int Coin
        {
            get => _playerData.coin;
            set => _playerData.coin = value;
        }

        public int Gas
        {
            get => _playerData.gas;
            set => _playerData.gas = value;
        }

        public int Experience
        {
            get => _playerData.experience;
            set => _playerData.experience = value;
        }

        public void SetPlayerQuest(Quest quest)
        {

            List<int> questInfo = new List<int>();

            // List.Zero int is for QuestType.
            questInfo.Add((int)quest.Type);
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

        public Dictionary<string, List<int>> GetActiveQuests()
        {
            return _playerData.playerActiveQuest;
        }
        //public Dictionary<string, PlayerQuestData> LoadPlayerQuest()
        //{
        //    return _playerData.playerActiveQuest;
        //}

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

            _playerData = new PlayerData();
            _systemData = new SystemData();

        }

        private void Start()
        {
            //SavePlayerData();
            LoadPlayerData();
            
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
            _playerData = DataSaver.Instance.Load<PlayerData>(_playerData);
            UpdatePlayerData();
        }

        public void SaveSettings()
        {
            DataSaver.Instance.Save<SystemData>(_systemData);
        }

    }
}
using NaviEnt.Game;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

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
            if(_playerData.playerActiveQuest== null)
            {
                Debug.Log("DataManager: playerActiveQuestIs Null");
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

            initData();

        }

        private void Start()
        {
            //TODO Need to prepare first time play.
            LoadPlayerData();
        }

        private void initData()
        {
            _playerData = new PlayerData();
            //initilize default values.
            _playerData.playerName = "Player";
            _playerData.coin = 0;
            _playerData.gas = 0;
            _playerData.experience = 0;
            _playerData.playerActiveQuest = new Dictionary<string, List<int>>();
            _playerData.playerFinishedQuestKey = new List<string>();

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
            _playerData = DataSaver.Instance.Load<PlayerData>(_playerData);
            UpdatePlayerData();
        }

        public void SaveSettings()
        {
            DataSaver.Instance.Save<SystemData>(_systemData);
        }

    }
}
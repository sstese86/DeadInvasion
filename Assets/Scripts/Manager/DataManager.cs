using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NaviEnt.Data
{
    public class DataManager : MonoBehaviour
    {
        PlayerData _playerData = null;
        SystemData _systemData = null;        
        
        #region PLAYERDATA
        public string PlayerName
        {
            get => _playerData.playerName;
            set => _playerData.playerName = value;
        }

        public int Gold
        {
            get => _playerData.gold;
            set => _playerData.gold = value;
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
            _playerData = new PlayerData();
            _systemData = new SystemData();

        }

    }
}
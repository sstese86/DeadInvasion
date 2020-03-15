using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using NaviEnt;

namespace NaviEnt.Level
{
    public class LevelManager : MonoBehaviour
    {
        int _mainMenuIndex = 1;
        int _battleUIIndex = 2;
        int _alwaysOnTopUI = 3;

        static LevelManager _instance;
        public static LevelManager Instance => _instance;

        private void Awake()
        {
            if (_instance != null) Destroy(gameObject);
            else _instance = this;
        }

        public void EnterToMission(int index)
        {
            SceneManager.LoadSceneAsync(index);
            SceneManager.LoadSceneAsync(_battleUIIndex,LoadSceneMode.Additive);
            SceneManager.LoadSceneAsync(_alwaysOnTopUI, LoadSceneMode.Additive);
        }
        
        public void EnterToMission(string key)
        {
            SceneManager.LoadSceneAsync(key);
            SceneManager.LoadSceneAsync(_battleUIIndex, LoadSceneMode.Additive);
            SceneManager.LoadSceneAsync(_alwaysOnTopUI, LoadSceneMode.Additive);
        }

        public void EnterToNextMission()
        {
            GameEventManager.Instance.OnSavePlayerData();
            int currentIndex = SceneManager.GetActiveScene().buildIndex + 1;

            //need to save battledata.
            EnterToMission(currentIndex);
        }

        public void RestartCurrentMission()
        {
            int currentIndex = SceneManager.GetActiveScene().buildIndex;

            //need to save battledata.
            EnterToMission(currentIndex);
        }


        public void EnterToPreviousMission()
        {
            GameEventManager.Instance.OnSavePlayerData();
            int currentIndex = SceneManager.GetActiveScene().buildIndex - 1;

            //need to save battledata.
            EnterToMission(currentIndex);
        }



        public void MoveToMainMenu()
        {
            GameEventManager.Instance.OnSavePlayerData();
            SceneManager.LoadSceneAsync(_mainMenuIndex);
            SceneManager.LoadSceneAsync(_alwaysOnTopUI, LoadSceneMode.Additive);
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace NaviEnt.Level
{
    public class LevelManager : MonoBehaviour
    {
        static int mainMenuIndex = 1;
        static int gameScene = 2;

        public static int missionId = 0;

        static LevelManager _instance;
        public static LevelManager Instance => _instance;

        private void Awake()
        {
            if (_instance != null) Destroy(gameObject);
            else _instance = this;
        }

        public static void LoadGameScene(int missionId)
        {
            LevelManager.missionId = missionId;
            SceneManager.LoadSceneAsync(gameScene);
        }
    }
}
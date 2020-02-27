using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaviEnt.Data;
using Sirenix.OdinInspector;
using System;

namespace NaviEnt.Game
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        DataManager dataManager;
        GameEventManager gameEventManager;
        QuestDatabase _questDatabase;

        List<Quest> ActiveQuest;
        List<Quest> FinishedQuest;

        private void Awake()
        {
            if (Instance != null) Destroy(gameObject);
            else Instance = this;
        }

        // Start is called before the first frame update
        void Start()
        {
            Initialize();
        }

        private void Initialize()
        {
            dataManager = DataManager.Instance;
            gameEventManager = GameEventManager.Instance;
            _questDatabase = Resources.Load<QuestDatabase>("ScriptableObject/QuestDatabase");

            //LoadQuestData();

        }

        //TODO Deal with Quest Dictionary. 3Type of QuestDictionary is very bad idea. try to make it one.
        private void LoadQuestData()
        {
            
            Dictionary<string, List<int>> loadedQuests = dataManager.GetActiveQuests();
            
            Dictionary<string, List<int>> loadedMainQuests = new Dictionary<string, List<int>>();
            Dictionary<string, List<int>> loadedSubQuests = new Dictionary<string, List<int>>();
            Dictionary<string, List<int>> loadedDayilyQuests = new Dictionary<string, List<int>>();

            //Sorting Quests.
            foreach (KeyValuePair<string, List<int>> entry in loadedQuests)
            {
                int questType = entry.Value[0];
                if(questType==0) loadedMainQuests[entry.Key] = entry.Value;
                else if(questType == 1) loadedSubQuests[entry.Key] = entry.Value;
                else loadedDayilyQuests[entry.Key] = entry.Value;
            }




        }

        public void Reward(int coin, int experience)
        {
            dataManager.Coin += coin;
            dataManager.Experience += experience;
            gameEventManager.OnPlayerDataChanged();
        }

        [Button(ButtonSizes.Medium)]
        private void SavePlayerData()
        {
            dataManager.SavePlayerData();
        }


    }
}
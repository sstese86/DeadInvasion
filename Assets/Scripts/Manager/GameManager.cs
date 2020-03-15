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

        [HideInInspector]
        public int totalMenuOpened = 0;
        public bool IsAnyMenuOpened { get; private set; }

        public int PlayerFightingPower { get; private set; }

        DataManager _dataManager;
        GameEventManager _gameEventManager;

        QuestDatabase _questDatabase;
        ItemDatabase _itemDatabase;

        IEntity _selectedEntity;

        List<Quest> _activeQuest;
        List<Quest> _finishedQuest;

        public float gravity = 0.5f;

        private void Awake()
        {
            if (Instance != null) Destroy(gameObject);
            else Instance = this;
            Initialize();

        }

        // Start is called before the first frame update
        void Start()
        {
            GameEventManager.onUIStateUpdate += CheckOpenedMenus;
        }

        private void OnDestroy()
        {
            GameEventManager.onUIStateUpdate -= CheckOpenedMenus;
        }

        private void Initialize()
        {
            _dataManager = DataManager.Instance;
            _gameEventManager = GameEventManager.Instance;

            _questDatabase = Resources.Load<QuestDatabase>("ScriptableObject/QuestDatabase");
            _itemDatabase = Resources.Load<ItemDatabase>("ScriptableObject/ItemDatabase");

            _activeQuest = new List<Quest>();
            _finishedQuest = new List<Quest>();
            LoadQuestData();

        }

        //TODO Deal with Quest Dictionary. 3Type of QuestDictionary is very bad idea. try to make it one.
        private void LoadQuestData()
        {

            // Load activeQuests from player data.
            Dictionary<string, List<int>> loadedActiveQuests = _dataManager.GetActiveQuests();
            if (loadedActiveQuests == null) return;

            _activeQuest.Clear();
            foreach (KeyValuePair<string, List<int>> entry in loadedActiveQuests)
            {
                Quest quest;
                List<QuestGoal> questGoals = new List<QuestGoal>();
                
                if(!_questDatabase.data.TryGetValue(entry.Key, out quest)) return;
                quest.Key = entry.Key;

                int iter = 0;
                foreach(QuestGoal goal in quest.questGoals)
                {
                    QuestGoal tempQuestGoal = goal;
                    tempQuestGoal.CurrentAmount = entry.Value[iter];
                    questGoals.Add(tempQuestGoal);
                    iter++;
                }

                quest.questGoals = questGoals;
                _activeQuest.Add(quest);                
            }

            // Load finished Quests from player data.            
            List<string> loadedFinishedQuests = _dataManager.GetFinishedQuests();
            if (loadedFinishedQuests == null) return;

            _finishedQuest.Clear();

            foreach(string key in loadedFinishedQuests)
            {
                Quest quest;
                _questDatabase.data.TryGetValue(key, out quest);
                _finishedQuest.Add(quest);
            }

        }

        // It's a bad name. give a new name
        public void AddNewQuest(Quest newQuest)
        {
            newQuest.isActive = true;
            _activeQuest.Add(newQuest);

            _dataManager.SetPlayerQuest(newQuest);

            //TODO Make a reflect to Quest UI
        }

        public void ItemInstantiate(string key, int amount, Transform trans)
        {
            GameObject obj = Instantiate(_itemDatabase.data[key].obj, trans) ;
            Item item = obj.GetComponent<Item>();
            item.ItemData = _itemDatabase.data[key];

            item.Amount = amount;
        }

        public ItemData GetItemData(string key)
        {
            return _itemDatabase.data[key];
        }

        public void AddPlayerItemAmount(string key, int amount)
        {


            int currentAmount = _dataManager.GetPlayerItemAmount(key);
            currentAmount += amount;
            _dataManager.SetPlayerItemAmount(key, currentAmount);
        }

        void CheckOpenedMenus()
        {
            if (totalMenuOpened == 0)
                IsAnyMenuOpened = false;
            else IsAnyMenuOpened = true;
        }

        #region TEST

        public void Reward(int coin, int experience)
        {
            _dataManager.Coin += coin;
            _dataManager.Experience += experience;
            _gameEventManager.OnPlayerDataChanged();
        }

        [Button(ButtonSizes.Medium)]
        private void SavePlayerData()
        {
            _dataManager.SavePlayerData();
        }

        #endregion

    }
}
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

        PlayerDataManager _playerDataManager;
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
            _playerDataManager = PlayerDataManager.Instance;
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
            Dictionary<string, List<int>> loadedActiveQuests = _playerDataManager.GetActiveQuests();
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
            List<string> loadedFinishedQuests = _playerDataManager.GetFinishedQuests();
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

            _playerDataManager.SetPlayerQuest(newQuest);

            //TODO Make a reflect to Quest UI
        }

        public GameObject PickupItemInstantiate(string key, int amount, Transform trans)
        {
            GameObject obj = Instantiate(_itemDatabase.data[key].obj, trans) as GameObject ;

            PickupItem item = obj.GetComponent<PickupItem>();

            item.Key = key;
            item.Amount = amount;

            item.InitializePickupItem();
            item.EntityInfo = _itemDatabase.data[key].description;
            item.EntityName = _itemDatabase.data[key].name;

            obj.name = _itemDatabase.data[key].name;
            return obj;
        }

        public ItemData GetItemData(string key)
        {
            return _itemDatabase.data[key];
        }

        public void AddPlayerItemAmount(string key, int amount)
        {
            int currentAmount = 0;

            ItemData item = _itemDatabase.data[key];
            if(item.notAllowMultiple != true)
            {
                currentAmount = _playerDataManager.GetPlayerItemAmount(key);
                currentAmount += amount;
            }
            else
            {
                currentAmount = 1;
            }
            _playerDataManager.SetPlayerItemAmount(key, currentAmount);
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
            _playerDataManager.Coin += coin;
            _playerDataManager.Experience += experience;
            _gameEventManager.OnPlayerDataChanged();
        }

        [Button(ButtonSizes.Medium)]
        private void SavePlayerData()
        {
            _playerDataManager.SavePlayerData();
        }

        #endregion

    }
}
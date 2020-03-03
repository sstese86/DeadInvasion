using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Sirenix.OdinInspector;
using NaviEnt.Game;

namespace NaviEnt.Data
{
    public enum GoalType
    {
        Kill,
        GatheringItem
    }

    [System.Serializable]
    public struct QuestGoal
    {
        public GoalType goalType;

        public string name;
        [TextArea(2,4)]
        public string description;

        [HorizontalGroup]
        [PreviewField(42)]
        [HideLabel]
        public Sprite icon;

        [HorizontalGroup]
        public int requiredAmount, currentAmount;

        public int CurrentAmount
        {
            get => currentAmount;
            set => currentAmount = value;
        }

        public bool IsGoalReached()
        {

            if (currentAmount >= requiredAmount)
            {
                OnGoalComplete();
                return true;
            }
            return false;
        }

        public void EnemyKilled()
        {
            if (goalType == GoalType.Kill)
            {
                // need to check the type of the quest enemy target;
                currentAmount++;
            }
        }

        public void ItemCollected()
        {
            if (goalType == GoalType.GatheringItem)
            {
                // need to check the type of the quest item that need to gather.;
                currentAmount++;
            }
        }

        void OnGoalComplete()
        {
            Debug.Log(name + " has Complete");
        }
    }

    public enum QuestType
    {
        MainQuest,
        SubQuest,
        DayilyQuest,
    }

    [System.Serializable]
    public struct Quest
    {
        string _key;
        public string Key
        {
           get => _key;
           set => _key = value;
        }

        public QuestType questType;
        public bool isActive;
        public string title;
        [TextArea(2, 4)]
        public string description;
        public int experienceReward;
        public int coinReward;

        public List<QuestGoal> questGoals;

        public void UpdateQuest()
        {
            int totalQuestGoals = questGoals.Count;
            int completedQuestGoals = 0;

            foreach (QuestGoal goal in questGoals)
            {
                if (goal.IsGoalReached())
                    completedQuestGoals++;
            }

            if (totalQuestGoals == completedQuestGoals)
            {
                OnQuestComplete();
            }

        }

        private void OnQuestComplete()
        {
            Debug.Log("The Quest " + title + "has completed");
            isActive = false;
            GameManager.Instance.Reward(coinReward, experienceReward);

        }
    }


    [CreateAssetMenu(fileName = "QuestDatabase", menuName = "NaviEnt/QuestDatabase")]
    public class QuestDatabase : SerializedScriptableObject
    {
        public Dictionary<string, Quest> data;
    }
}
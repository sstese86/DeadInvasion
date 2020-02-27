using System.Collections;
using System.Collections.Generic;
using System;

namespace NaviEnt.Data
{

    public struct PlayerItemData
    {

    }


    [Serializable]
    public struct PlayerData
    {
        public string playerName;        
        
        public int coin;
        public int gas;
        public int experience;

        //List<int> for QuestGoal's currentAmount.
        public Dictionary<string, List<int>> playerActiveQuest;
        public List<string> playerFinishedQuestKey;
        public Dictionary<string, PlayerItemData> playerItem;
        
        //Will be inplement later.
        public Dictionary<int, string> inventory;
        

        
        public PlayerData(string playerName = "Player")
        {
            this.playerName = playerName;
            this.coin = 0;
            this.gas = 0;
            this.experience = 0;
            this.playerActiveQuest = new Dictionary<string, List<int>> {};
            this.playerFinishedQuestKey = new List<string>();
            this.playerItem = new Dictionary<string, PlayerItemData> {};
            this.inventory = new Dictionary<int, string> {};
        }        

    }

}
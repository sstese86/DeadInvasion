using System.Collections;
using System.Collections.Generic;
using System;

namespace NaviEnt.Data
{
    [Serializable]
    public struct PlayerData
    {
        public string playerName;        
        
        public int gold;
        public int gas;
        public int experience;

        public Dictionary<string, bool> quest;
        public Dictionary<int, string> inventory;
        

        
        public PlayerData(string playerName = "Player")
        {
            this.playerName = playerName;
            this.gold = 0;
            this.gas = 0;
            this.experience = 0;
            this.quest = new Dictionary<string, bool>{};
            this.inventory = new Dictionary<int, string> {};
        }        

    }

}
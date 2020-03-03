using System.Collections;
using System.Collections.Generic;
using System;

namespace NaviEnt.Data
{


    [Serializable]
    public struct PlayerData
    {
        public string playerName;        
        
        public int coin;
        public int gas;
        public int experience;

        public Dictionary<string, List<int>> playerActiveQuest;
        public List<string> playerFinishedQuestKey;

        //public Dictionary<int, string> inventory;
        //playerItem = new Dictionary<string, PlayerItemData> {};

    }

}
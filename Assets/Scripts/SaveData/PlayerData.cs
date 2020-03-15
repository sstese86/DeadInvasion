using System.Collections;
using System.Collections.Generic;
using System;

namespace NaviEnt.Data
{

    [Serializable]
    public struct PlayerData
    {
        public string playerName;        
        
        public Dictionary<string, List<int>> playerActiveQuest;
        
        // Just need keys because there will be no detailed infomation then just done sign.
        public List<string> playerFinishedQuestKey;

        // Possessed item Key , Amount
        public Dictionary<string, int> playerItem;

        //public Dictionary<int, string> inventory;
    }

}
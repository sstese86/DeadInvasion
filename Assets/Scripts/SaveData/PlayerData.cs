using System.Collections;
using System.Collections.Generic;
using System;

namespace NaviEnt.Data
{

    [Serializable]
    public class PlayerData : SaveData<PlayerData>
    {

        readonly string defaultPlayerName = "Player";

        public string playerName;

        public int gold;
        public int gas;
        public int experience;
        

        public Dictionary<int, string> dictionary;
        public List<int> listInt;

        public PlayerData()
        {
            playerName = defaultPlayerName;

            gold = 0;
            gas = 0;
            experience = 0;
            listInt = new List<int> { 1, 1, 2, 3, };
            dictionary = new Dictionary<int, string> { { 1,"teset"},{32,"haha" },{12,"test!!"} };

        }


    }
}
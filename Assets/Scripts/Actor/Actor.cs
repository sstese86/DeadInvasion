using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public enum Team
{
    Player,
    Enemy_0,
    Enemy_1,
    Enemy_2,
    Prop,
    Alliance_0,
    Alliance_1,
    Alliance_2,
}

namespace NaviEnt
{
    public class Actor : MonoBehaviour
    {

        [SerializeField]
        string _name = string.Empty;
        [SerializeField]
        string _disc = string.Empty;

        [SerializeField]
        Team _team = Team.Player;

        public string ActorName { get => _name; }
        public string ActorDiscription { get => _disc; }
        public Team ActorTeam { get => _team; }
        public int CurrentHealth { get; protected set; }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NaviEnt
{
    [System.Serializable]
    public struct CharacterState
    {
        public int maxHealth;
        public int attackDamage;

        public int strength;
        public int agility;
        public int defance;

        public float moveSpeed;
        public float jumpSpeed;
    }
}


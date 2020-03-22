using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace NaviEnt
{
    [System.Serializable]
    public struct CharacterState
    {
        [HorizontalGroup("Life")]
        public int maxHealth;
        [HorizontalGroup("Life")]
        public int damage;

        [HorizontalGroup("BaseStat")]
        [LabelWidth(70)]
        public int strength;
        [HorizontalGroup("BaseStat")]
        [LabelWidth(70)]
        public int agility;
        [HorizontalGroup("BaseStat")]
        [LabelWidth(70)]
        public int defance;

        [HorizontalGroup("Movement")]
        public float moveSpeed;
        [HorizontalGroup("Movement")]
        public float jumpSpeed;
    }

    [System.Serializable]
    public struct WeaponState
    {
        [HorizontalGroup("Ammo")]
        public bool needAmmo;
        [HorizontalGroup("Ammo")]
        public int maxAmmo;
        public float fireRate;
    }

}


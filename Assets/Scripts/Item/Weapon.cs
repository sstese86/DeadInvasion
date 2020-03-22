using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum WeaponType
{
    None,
    Melee_OneHand,
    Melee_TwoHand,
    Range_HandGun,
    Range_Rifle,
    Range_Sniper,
    Range_Roket,
}

namespace NaviEnt.Game
{
    // This Weapon class is for Graphic representation and adjust modifier. the actual state and data is stored at database.
    public class Weapon : Item<Weapon>, IEntity
    {
        [SerializeField]
        WeaponType _weaponType = WeaponType.None;


        public string EntityName { get; set; }
        public string EntityInfo { get; set; }




        public void UpdateEntityInfo()
        {
            // No need to. unless later weapon has fixing feature.
        }

    }
}
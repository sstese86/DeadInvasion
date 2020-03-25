using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaviEnt;
using NaviEnt.Game;
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

[System.Serializable]
public struct WeaponAttackSetup
{
    public int attackAnimIndex;
    public HitCollider hitCollider;
    public NaviEntEffect hitEffect;

    //public int GetRandomAttackAnimIndex()
    //{
    //    int result = 0;
    //    result = Mathf.RoundToInt(Random.Range(0f, (float)attackAnimIndex.Count));
    //    return result;
    //}
}

namespace NaviEnt.Game
{
    // This Weapon class is for Graphic representation and adjust modifier. the actual state and data is stored at database.
    public class Weapon : Item<Weapon>
    {

        [SerializeField]
        ItemSoundClip _itemSoundClip = null;
        [Space]

        [SerializeField]
        WeaponType _weaponType = WeaponType.None;        

        [SerializeField]
        List<WeaponAttackSetup> _weaponAttackSetup = new List<WeaponAttackSetup>();

        public int WeaponTypeIndex { get => (int)_weaponType; }        
        public ItemSoundClip ItemSoundClip { get => _itemSoundClip; }
        public List<WeaponAttackSetup> WeaponAttackSetup { get => _weaponAttackSetup; }

        public float GetAttackAnimIndex()
        {
            int result = 0;
            result = Mathf.RoundToInt(Random.Range(0f, (float)_weaponAttackSetup.Count-1));
            return (float)result;
        }


        
        public void UpdateEntityInfo()
        {
            // No need to. unless later weapon has fixing feature.
        }

        public void Fire()
        {

        }

        public void Reload()
        {

        }
    }
}
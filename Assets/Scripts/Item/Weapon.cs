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

[System.Serializable]
public struct AttackAnimSetup
{
    public List<int> attackAnimIndex;

    public GameObject hitCollider;
    public GameObject hitEffect;

    public int GetRandomAttackAnimIndex()
    {
        int result = 0;
        result = Mathf.RoundToInt(Random.Range(0f, (float)attackAnimIndex.Count));
        return result;
    }
}

namespace NaviEnt.Game
{
    // This Weapon class is for Graphic representation and adjust modifier. the actual state and data is stored at database.
    public class Weapon : Item<Weapon>, IEntity
    {

        [SerializeField]
        ItemSoundClip _itemSoundClip = null;
        [Space]

        [SerializeField]
        WeaponType _weaponType = WeaponType.None;
        

        [SerializeField]
        List<int> _defaultAttackAnimIndex = new List<int>();
        [SerializeField]
        int _criticalAttackAnimIndex = 0;

        [SerializeField]
        HitCollider _defaultHitCollider = null;
        [SerializeField]
        HitCollider _criticalHitCollider = null;

        public string EntityName { get; set; }
        public string EntityInfo { get; set; }

        public int WeaponTypeIndex { get => (int)_weaponType; }
        public HitCollider HitCollider { get => _defaultHitCollider; }
        public HitCollider CreticalHitCollider { get => _criticalHitCollider; }

        public ItemSoundClip ItemSoundClip { get => _itemSoundClip; }

        public float GetAttackAnimIndex()
        {
            int result = 0;
            result = Mathf.RoundToInt(Random.Range(0f, (float)_defaultAttackAnimIndex.Count-1));
            return (float)result;
        }
        public float GetCreticalAttackAnimIndex()
        {
            return (float)_criticalAttackAnimIndex;
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
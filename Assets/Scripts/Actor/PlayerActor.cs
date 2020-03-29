using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;


namespace NaviEnt.Game
{
    //TODO Rename to PlayerActor

    public class PlayerActor : Actor, IDamageable
    {
        [SerializeField]
        FeedbackHandler _feedbackHandler = null;
        [SerializeField]
        EquipmentHandler _equipmentHandler = null;
        [SerializeField]
        PlayerActorSoundClip _actorSoundClip = null;        

        WeaponState _weaponState = new WeaponState();

        public ItemSoundClip GetWeaponSoundClip { get => _equipmentHandler.CurrentWeapon?.ItemSoundClip; }
        public PlayerActorSoundClip GetActorSoundClip { get => _actorSoundClip; }

        public Weapon GetCurrentWeapon { get => _equipmentHandler.CurrentWeapon; }
        public float GetWeaponRange { get => _weaponState.range; }
        public float GetWeaponFireRate { get => _weaponState.fireRate; }

        //Animation Infomation
        public int GetWeaponType { get => _equipmentHandler.GetWeaponIndex(); }
        public float GetAttackAnimIndex { get => _equipmentHandler.GetAttackAnimIndex(); }


        public override void OnEnable()
        {
            base.OnEnable();

            UpdateEquipmentState();

            if (_equipmentHandler)
                _equipmentHandler.onActorEquipmentChanged += UpdateEquipmentState;
        }

        private void OnDisable()
        {
            if (_equipmentHandler)
                _equipmentHandler.onActorEquipmentChanged -= UpdateEquipmentState;
        }
        // Use this for initialization
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        public HitCollider GetHitCollider(int animIndex)
        {
            return GetCurrentWeapon.WeaponAttackSetup[animIndex].hitCollider;
        }

        public NaviEntEffect GetHitEffect(int animIndex)
        {
            return GetCurrentWeapon.WeaponAttackSetup[animIndex].hitEffect;
        }

        void UpdateEquipmentState()
        {
            _modifiedState = _equipmentHandler.UpdateModifiedCharacterState(_baseState);
            _weaponState = _equipmentHandler.WeaponState;
            Damage = ModifiedState.damage + _weaponState.damage;
            UpdateHealthInfo();            
        }

        protected override void TakeDamageFeedback()
        {
            base.TakeDamageFeedback();
            _actorSoundClip?.PlaySoundHit();
            _feedbackHandler?.HitFeedback();
        }

        public override void Heal(int amount)
        {
            base.Heal(amount);
        }

        protected override void DeadFeedback()
        {
            base.DeadFeedback();
            _actorSoundClip?.PlaySoundDead();            
        }

        protected override void UpdateHealthInfoFeedback()
        {
            base.UpdateHealthInfoFeedback();
        }


    }
}
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
        ActorSoundClip _actorSoundClip = null;        

        WeaponState _weaponState = new WeaponState();

        HUDHealthBar _healthbar = null;

        public ItemSoundClip WeaponSoundClip { get => _equipmentHandler.CurrentWeapon?.ItemSoundClip; }
        public ActorSoundClip ActorSoundClip { get => _actorSoundClip; }

        public Weapon CurrentWeapon { get => _equipmentHandler.CurrentWeapon; }
        public float WeaponRange { get => _weaponState.range; }
        public float WeaponFireRate { get => _weaponState.fireRate; }

        //Animation Infomation
        public int WeaponType { get => _equipmentHandler.GetWeaponIndex(); }
        public float AttackAnimIndex { get => _equipmentHandler.GetAttackAnimIndex(); }


        private void OnEnable()
        {
            UpdateEquipmentState();

            CurrentHealth = ModifiedState.maxHealth;
            
            EntityName = ActorName;
            EntityInfo = ModifiedState.maxHealth.ToString() + " / " + CurrentHealth.ToString();
            EntityValue = 1f;

            _healthbar = GetComponentInChildren<HUDHealthBar>();
            _healthbar.InitHealthBar();

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
            return CurrentWeapon.WeaponAttackSetup[animIndex].hitCollider;
        }

        public NaviEntEffect GetHitEffect(int animIndex)
        {
            return CurrentWeapon.WeaponAttackSetup[animIndex].hitEffect;
        }

        void UpdateEquipmentState()
        {
            _modifiedState = _equipmentHandler.UpdateModifiedCharacterState(_baseState);
            _weaponState = _equipmentHandler.WeaponState;
            Damage = ModifiedState.damage + _weaponState.damage;
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
            _healthbar?.gameObject.SetActive(false);
        }

        protected override void UpdateHealthInfoFeedback()
        {
            base.UpdateHealthInfoFeedback();
            _healthbar.UpdateHealthSlider(HealthRatio);
        }




    }
}
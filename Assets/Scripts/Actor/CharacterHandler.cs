using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;


namespace NaviEnt.Game
{
    public class CharacterHandler : Actor, IDamageable, IEntity
    {
        [Space]
        
        [SerializeField]
        FeedbackHandler _feedbackHandler = null;
        [SerializeField]
        EquipmentHandler _equipmentHandler = null;
        [SerializeField]
        ActorSoundClip _actorSoundClip = null;


        [Space]
        [SerializeField]
        CharacterState _baseState = new CharacterState();
        CharacterState _modifiedState = new CharacterState();

        WeaponState _weaponState = new WeaponState();

        HUDHealthBar _healthbar = null;

        public CharacterState ModifiedState { get => _modifiedState; }
        
        public ItemSoundClip WeaponSoundClip { get => _equipmentHandler.CurrentWeapon?.ItemSoundClip; }
        public ActorSoundClip ActorSoundClip { get => _actorSoundClip; }
        public Weapon CurrentWeapon { get => _equipmentHandler.CurrentWeapon; }
        public float WeaponRange { get => _weaponState.range; }

        public int Damage { get; private set; }
        public bool isDead { get; private set; }

        
        //Animation Infomation
        public int WeaponType { get => _equipmentHandler.GetWeaponIndex(); }
        public float AttackAnimIndex { get => _equipmentHandler.GetAttackAnimIndex(); }

        
        public string EntityName { get; set; }
        public string EntityInfo { get; set; }



        private void OnEnable()
        {
            UpdateEquipmentState();

            CurrentHealth = ModifiedState.maxHealth;
            
            EntityName = ActorName;
            EntityInfo = CurrentHealth.ToString();

            _healthbar = GetComponentInChildren<HUDHealthBar>();
            _healthbar?.gameObject.SetActive(true);
            if(_equipmentHandler)
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

        void UpdateEquipmentState()
        {
            _modifiedState = _equipmentHandler.UpdateModifiedCharacterState(_baseState);
            _weaponState = _equipmentHandler.WeaponState;
            Damage = ModifiedState.damage + _weaponState.damage;
        }

        public bool TakeDamage(Team team, int amount)
        {
            if(ActorTeam != team)
            {
                _actorSoundClip?.PlaySoundHit();
                CurrentHealth -= amount;
                IsDead();   
                UpdateCurrentHealthInfo();

                _feedbackHandler?.HitFeedback();
                return true;
            }
            return false;
        }
        public void Heal(int amount)
        {
            CurrentHealth += amount;
            UpdateCurrentHealthInfo();
        }

        bool IsDead()
        {
            if (CurrentHealth < 1)
            {
                isDead = true;
                _actorSoundClip?.PlaySoundDead();
                GetComponent<Collider>().enabled = false;
                _healthbar?.gameObject.SetActive(false);
                return true;
            }
            return false;
        }
        void UpdateCurrentHealthInfo()
        {
            CurrentHealth = Mathf.Clamp(CurrentHealth, 0, _baseState.maxHealth);
            float value = (float)CurrentHealth / (float)_baseState.maxHealth;
            _healthbar.UpdateHealthSlider(value);
            EntityInfo = CurrentHealth.ToString();
        }

        public Team GetTeam()
        {
            return ActorTeam;
        }

        public HitCollider GetHitCollider(int animIndex)
        {
            return CurrentWeapon.WeaponAttackSetup[animIndex].hitCollider;
        }

        public NaviEntEffect GetHitEffect(int animIndex)
        {
            return CurrentWeapon.WeaponAttackSetup[animIndex].hitEffect;
        }


        public void UpdateEntityInfo()
        {
            EntityName = ActorName;
            EntityInfo = _baseState.maxHealth.ToString() + " / " + CurrentHealth.ToString();
            GameEventManager.Instance.OnSelectedEntityChangedCallback(GetComponent<IEntity>());
        }
    }
}
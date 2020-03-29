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
    public class Actor : MonoBehaviour, IEntity, IDamageable
    {

        [SerializeField]
        string _name = string.Empty;
        [SerializeField]
        string _disc = string.Empty;

        [SerializeField]
        Team _team = Team.Player;



        [Space]
        [SerializeField]
        protected CharacterState _baseState = new CharacterState();

        protected CharacterState _modifiedState = new CharacterState();

        public Team DamageableTeam { get => _team; }
        public string ActorName { get => _name; }
        public string ActorDiscription { get => _disc; }
        public int CurrentHealth { get; protected set; }
        public float HealthRatio { get; protected set; }
        public CharacterState ModifiedState { get => _modifiedState; }

        public string EntityName { get; set; }
        public string EntityInfo { get; set; }
        public float EntityValue { get; set; }

        public int Damage { get; protected set; }
        public bool isDead { get; protected set; }

        public virtual void UpdateEntityInfo()
        {
            EntityInfo = ModifiedState.maxHealth.ToString() + " / " + CurrentHealth.ToString();
            EntityValue = HealthRatio;

            if (DamageableTeam != Team.Player)
                GameEventManager.Instance.OnSelectedEntityChangedCallback(GetComponent<IEntity>(), transform);
        }

        void UpdateHealthInfo()
        {
            CurrentHealth = Mathf.Clamp(CurrentHealth, 0, _modifiedState.maxHealth);
            HealthRatio = (float)CurrentHealth / (float)_modifiedState.maxHealth;
            UpdateHealthInfoFeedback();
        }

        public virtual bool TakeDamage(Team team, int amount)
        {
            if (DamageableTeam != team)
            {
                CurrentHealth -= amount;
                TakeDamageFeedback();
                IsDead();
                return true;
            }
            return false;
        }

        public virtual void Heal(int amount)
        {
            CurrentHealth += amount;
            UpdateHealthInfo();
        }

        protected bool IsDead()
        {
            if (CurrentHealth < 1)
            {
                isDead = true;
                GetComponent<Collider>().enabled = false;
                DeadFeedback();
                return true;
            }
            return false;
        }

        protected virtual void TakeDamageFeedback() { UpdateHealthInfo(); }
        protected virtual void UpdateHealthInfoFeedback() 
        {
            UpdateEntityInfo();
        }
        protected virtual void DeadFeedback()
        {
            EntityInfo = EntityName + " is Dead";
            if (DamageableTeam != Team.Player)
                GameEventManager.Instance.OnSelectedEntityChangedCallback(GetComponent<IEntity>(), transform);
        }
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
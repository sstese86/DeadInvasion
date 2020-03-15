using UnityEngine;
using System.Collections;
using NaviEnt.Game;

namespace NaviEnt
{
    public class CharacterHandler : Actor, IDamageable, IEntity
    {
        [SerializeField]
        FeedbackHandler _feedbackHandler = null;

        [SerializeField]
        CharacterState _state = new CharacterState();

        HUDHealthBar _healthbar = null;
        public CharacterState State { get => _state; }
        
        public string EntityName { get; set; }
        public string EntityInfo { get; set; }

        // Use this for initialization
        void Start()
        {
            CurrentHealth = _state.maxHealth;
            _healthbar = GetComponentInChildren<HUDHealthBar>();
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void TakeDamage(Team team, int amount)
        {
            if(ActorTeam != team)
            { 
                CurrentHealth -= amount;
                IsDead();   
                UpdateCurrentHealthInfo();

                _feedbackHandler.HitFeedback();
            }
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
                DeadCallback();
                return true;
            }
            return false;
        }
        void UpdateCurrentHealthInfo()
        {
            CurrentHealth = Mathf.Clamp(CurrentHealth, 0, _state.maxHealth);
            float value = (float)CurrentHealth / (float)_state.maxHealth;
            _healthbar.UpdateHealthSlider(value);
        }

        void DeadCallback()
        {
            GameEventManager.Instance.OnPlayerDead();
            Debug.Log(gameObject.name + " is dead.");
        }

        public Team GetTeam()
        {
            return ActorTeam;
        }

        public void UpdateEntityInfo()
        {
            EntityName = ActorName;
            EntityInfo = _state.maxHealth.ToString() + " / " + CurrentHealth.ToString();
            GameEventManager.Instance.OnSelectedEntityChangedCallback(GetComponent<IEntity>());
        }
    }
}